using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace uChat
{
    public partial class Form1 : Form
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public const int MAX_CONNECT_ATTEMPTS = 5;
        public string USERNAME;
        FileStream fs;
        StreamReader sr;
        StreamWriter sw;

        public Form1()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            colorDialog1.Color = Color.Black;
            USERNAME = Environment.UserName;

            bool read=false;
            if(File.Exists("settings.ini"))
                read = true;

            fs = new FileStream("settings.ini", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sr = new StreamReader(fs);
            sw = new StreamWriter(fs);

            if (read)
            {
                try
                {
                    USERNAME = sr.ReadLine();
                    colorDialog1.Color = System.Drawing.ColorTranslator.FromHtml(sr.ReadLine());
                    Console.WriteLine("Settings loaded");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show("Unable to load settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


            colorBox.BackColor = colorDialog1.Color;
        }

        delegate void Invoker(string parameter);
        delegate void arrayInvoker(byte[] parameter);

        public void SetTitleSafe(string title)
        {
            if (statusLabel.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                statusLabel.BeginInvoke(new Invoker(SetTitleSafe), title);

                // we return immedeately
                return;
            }

            // From here on it is safe to access methods and properties on the GUI
            // For example:
            statusLabel.Text = title;
        }

        public void SetRichTextBox(byte[] data)
        {
            if (chatRichTextBox.InvokeRequired)
            {
                chatRichTextBox.BeginInvoke(new arrayInvoker(SetRichTextBox), data);

                return;
            }

            //raspakujemo niz bajtova nazad u niz stringova
            string[] msg = new string[3];
            msg = (string[])ByteArrayToObject(data);

            chatRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            chatRichTextBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml(msg[1]);
            chatRichTextBox.SelectedText = msg[0] + " says: " + msg[2] +
                                           Environment.NewLine;
            chatRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            chatRichTextBox.SelectionColor = Color.Gray;
            chatRichTextBox.SelectedText = DateTime.Now.ToShortTimeString() + Environment.NewLine;

            //Autoscroll down
            chatRichTextBox.SelectionStart = chatRichTextBox.Text.Length;
            chatRichTextBox.ScrollToCaret();

        }


        public void LoopConnect()
        {
            int attempts = 0;
            SetTitleSafe("Connecting to server...");
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 9999);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Connection attempt #: " + attempts.ToString());
                }

                if (attempts >= MAX_CONNECT_ATTEMPTS)
                {
                    MessageBox.Show(
                                    "Unable to connect to server.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                    SetTitleSafe("Failed to connect.");
                    return;
                }
            }
            Console.WriteLine("Connected.");
            SetTitleSafe("Connected.");
            Thread recieveThread = new Thread(RecieveLoop);
            recieveThread.IsBackground = true;
            recieveThread.Name = "recieveloop";
            recieveThread.Start();

            //Postavljamo username i boju
            SetUsername(USERNAME);
            SetUserColor(colorDialog1.Color);
        }

        public void RecieveLoop()
        {
            while (true)
            {

                try
                {
                    byte[] recieved_buffer = new byte[1024];
                    int n = _clientSocket.Receive(recieved_buffer);
                    byte[] data = new byte[n];
                    Array.Copy(recieved_buffer, data, n);
                    SetRichTextBox(data);
                }
                catch (SocketException)
                {
                    MessageBox.Show("Server disconnected.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                    SetTitleSafe("Disconnected.");
                    return;
                }

            }

        }

        private void userinputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string text = userinputTextBox.Text;
                try
                {
                    _clientSocket.Send(Encoding.ASCII.GetBytes(text));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Unable to send message.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1);
                    return;
                }
                Console.WriteLine("Sent: " + text);
                userinputTextBox.Clear();
            }
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorBox.BackColor = colorDialog1.Color;
            SetUserColor(colorDialog1.Color);
        }

        public void SetUsername(string name)
        {
            try
            {
                string text = "-setname#" + name;
                _clientSocket.Send(Encoding.ASCII.GetBytes(text));
                Console.WriteLine("Sent: " + text);
                USERNAME = name;
            }
            catch (SocketException)
            {
                Console.WriteLine("Error changing username.");
            }
        }

        public void SetUserColor(Color c)
        {
            try
            {
                string htmlColor = System.Drawing.ColorTranslator.ToHtml(c);
                string text = "-setcolor" + htmlColor;
                _clientSocket.Send(Encoding.ASCII.GetBytes(text));
                Console.WriteLine("Sent: " + text);
            }
            catch (SocketException)
            {
                Console.WriteLine("Error changing user color.");
            }
        }

        //-------------------funkcije za serijalizaciju/deserijalizaciju-----------------

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        private void aboutPanel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("uChat beta.\nBy Uros Jovanovic", "About");
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Form2 settings = new Form2();
            settings.UserName = USERNAME;
            DialogResult dr = settings.ShowDialog();
            if (dr == DialogResult.OK && USERNAME != settings.UserName)
            {
                USERNAME = settings.UserName;
                SetUsername(USERNAME);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Console.WriteLine(USERNAME);
                fs.Seek(0, SeekOrigin.Begin);
                sw.WriteLine(USERNAME);
                sw.WriteLine(System.Drawing.ColorTranslator.ToHtml(colorDialog1.Color));
                sw.Flush();
                sw.Close();
                sr.Close();
                fs.Close();
                Console.WriteLine("Settings saved");
            }
            catch (IOException)
            {
                MessageBox.Show("Unable to save settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

    }
}
