namespace uChat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.userinputTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.colorBox = new System.Windows.Forms.Panel();
            this.aboutPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Controls.Add(this.userinputTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chatRichTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.colorBox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.aboutPanel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.54737F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.452627F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 370);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // userinputTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.userinputTextBox, 2);
            this.userinputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userinputTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userinputTextBox.Location = new System.Drawing.Point(3, 346);
            this.userinputTextBox.MaxLength = 140;
            this.userinputTextBox.Name = "userinputTextBox";
            this.userinputTextBox.Size = new System.Drawing.Size(374, 21);
            this.userinputTextBox.TabIndex = 0;
            this.userinputTextBox.Tag = "";
            this.userinputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userinputTextBox_KeyDown);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.statusLabel.Location = new System.Drawing.Point(3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(184, 20);
            this.statusLabel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.statusLabel, "Connection status");
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.chatRichTextBox, 3);
            this.chatRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatRichTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatRichTextBox.Location = new System.Drawing.Point(3, 20);
            this.chatRichTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.ReadOnly = true;
            this.chatRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.chatRichTextBox.Size = new System.Drawing.Size(401, 313);
            this.chatRichTextBox.TabIndex = 2;
            this.chatRichTextBox.Text = "";
            // 
            // colorBox
            // 
            this.colorBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorBox.Location = new System.Drawing.Point(384, 347);
            this.colorBox.Margin = new System.Windows.Forms.Padding(4);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(19, 19);
            this.colorBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.colorBox, "Change text color");
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // aboutPanel
            // 
            this.aboutPanel.BackgroundImage = global::uChat.Properties.Resources.about;
            this.aboutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutPanel.Location = new System.Drawing.Point(380, 0);
            this.aboutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(16, 16);
            this.aboutPanel.TabIndex = 4;
            this.aboutPanel.Tag = "";
            this.toolTip1.SetToolTip(this.aboutPanel, "About");
            this.aboutPanel.Click += new System.EventHandler(this.aboutPanel_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::uChat.Properties.Resources.settings;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(360, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 0, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(16, 16);
            this.panel1.TabIndex = 5;
            this.toolTip1.SetToolTip(this.panel1, "Settings");
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 370);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "uChat v0.1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox userinputTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel colorBox;
        private System.Windows.Forms.Panel aboutPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;

    }
}

