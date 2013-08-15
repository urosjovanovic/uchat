using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ServerApp
{
    class Program
    {
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static Dictionary<Socket, string[]> _clients = new Dictionary<Socket, string[]>();
        private static readonly int _BUFFER_SIZE = 2048;
        private static byte[] _buffer = new byte[_BUFFER_SIZE];

        static void Main(string[] args)
        {
            Console.Title = "uChat Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Console.WriteLine("---Server Online---");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients)
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (var client in _clients)
            {
                client.Key.Shutdown(SocketShutdown.Both);
                client.Key.Close();
            }

            _serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = null;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            //Dodajemo novog klijenta
            string[] tmp = new string[2];
            tmp[0] = "DefaultName";
            tmp[1] = "Black";
            _clients.Add(socket, tmp);
            socket.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            Console.WriteLine("Client connected");
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received = 0;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                current.Close(); // Dont shutdown because the socket may be disposed and its disconnected anyway
                _clients.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(_buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if (text.StartsWith("-setname#")) // Klijent postavlja ime
            {
                string name = text.Substring(9);
                Console.WriteLine("Client name: "+name);
                if (_clients.ContainsKey(current))
                {
                    string oldname = _clients[current][0];
                    _clients[current][0] = name;
                    if(oldname != "DefaultName")
                    SendToAll(oldname + " changed name to " + name, "Gray");
                }

                else
                {
                    Console.WriteLine("Can't change client name.");
                    return;
                }
            }
            else if (text.StartsWith("-setcolor")) // Klijent postavlja boju
            {
                string htmlColor = text.Substring(9);
                Console.WriteLine("Client color: " + htmlColor);
                if (_clients.ContainsKey(current))
                    _clients[current][1] = htmlColor;
                else
                {
                    Console.WriteLine("Can't change client color.");
                    return;
                }
            }
            else
            {
                foreach (var client in _clients)
                {
                    string[] chatMsg  = new string[3];
                    chatMsg[0] = _clients[current][0];
                    chatMsg[1] = _clients[current][1];
                    chatMsg[2] = text;

                    //Pakujemo niz stringova u niz bajtova za slanje preko mreze
                    client.Key.Send(ObjectToByteArray(chatMsg));
                }
            }

            current.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), current);
        }

        private static void SendToAll(string msg, string color)
        {
             foreach (var client in _clients)
                {
                    string[] chatMsg  = new string[3];
                    chatMsg[0] = "Server";
                    chatMsg[1] = color;
                    chatMsg[2] = msg;

                    //Pakujemo niz stringova u niz bajtova za slanje preko mreze
                    client.Key.Send(ObjectToByteArray(chatMsg));
                }
        }

        //-------------------funkcije za serijalizaciju/deserijalizaciju-----------------
        
        // Convert an object to a byte array
        private static byte[] ObjectToByteArray(Object obj)
        {
            if(obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        // Convert a byte array to an Object
        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object) binForm.Deserialize(memStream);
            return obj;
        }

    }
}
