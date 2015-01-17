using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using NationalInstruments.VisaNS;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace NationalInstruments.Examples.SimpleReadWrite
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private MessageBasedSession mbSession;
        private string lastResourceString = null;
        private System.Windows.Forms.TextBox writeTextBox;
        private System.Windows.Forms.TextBox readTextBox;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Button openSessionButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button closeSessionButton;
        private System.Windows.Forms.Label stringToWriteLabel;
        private System.Windows.Forms.Label stringToReadLabel;
        private Button btnAuto;
        private Button btnSquare;
        private Button btnRamp;
        private Button btnNoise;
        private Button btnPulse;
        private Button button1;
        private TextBox boxIP;
        private RichTextBox boxRemote;
        private CheckBox boxTeste;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            SetupControlState(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mbSession != null)
                {
                    mbSession.Dispose();
                }
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.queryButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.openSessionButton = new System.Windows.Forms.Button();
            this.writeTextBox = new System.Windows.Forms.TextBox();
            this.readTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.closeSessionButton = new System.Windows.Forms.Button();
            this.stringToWriteLabel = new System.Windows.Forms.Label();
            this.stringToReadLabel = new System.Windows.Forms.Label();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnSquare = new System.Windows.Forms.Button();
            this.btnRamp = new System.Windows.Forms.Button();
            this.btnNoise = new System.Windows.Forms.Button();
            this.btnPulse = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.boxIP = new System.Windows.Forms.TextBox();
            this.boxRemote = new System.Windows.Forms.RichTextBox();
            this.boxTeste = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // queryButton
            // 
            this.queryButton.Location = new System.Drawing.Point(5, 83);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(74, 23);
            this.queryButton.TabIndex = 3;
            this.queryButton.Text = "Query";
            this.queryButton.Click += new System.EventHandler(this.query_Click);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(79, 83);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(74, 23);
            this.writeButton.TabIndex = 4;
            this.writeButton.Text = "Write";
            this.writeButton.Click += new System.EventHandler(this.write_Click);
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(153, 83);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(74, 23);
            this.readButton.TabIndex = 5;
            this.readButton.Text = "Read";
            this.readButton.Click += new System.EventHandler(this.read_Click);
            // 
            // openSessionButton
            // 
            this.openSessionButton.Location = new System.Drawing.Point(5, 5);
            this.openSessionButton.Name = "openSessionButton";
            this.openSessionButton.Size = new System.Drawing.Size(92, 22);
            this.openSessionButton.TabIndex = 0;
            this.openSessionButton.Text = "Open Session";
            this.openSessionButton.Click += new System.EventHandler(this.openSession_Click);
            // 
            // writeTextBox
            // 
            this.writeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.writeTextBox.Location = new System.Drawing.Point(5, 54);
            this.writeTextBox.Name = "writeTextBox";
            this.writeTextBox.Size = new System.Drawing.Size(633, 20);
            this.writeTextBox.TabIndex = 2;
            this.writeTextBox.Text = "*IDN?\\n";
            // 
            // readTextBox
            // 
            this.readTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readTextBox.Location = new System.Drawing.Point(5, 136);
            this.readTextBox.Multiline = true;
            this.readTextBox.Name = "readTextBox";
            this.readTextBox.ReadOnly = true;
            this.readTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.readTextBox.Size = new System.Drawing.Size(365, 231);
            this.readTextBox.TabIndex = 6;
            this.readTextBox.TabStop = false;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(6, 369);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(633, 24);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clear_Click);
            // 
            // closeSessionButton
            // 
            this.closeSessionButton.Location = new System.Drawing.Point(97, 5);
            this.closeSessionButton.Name = "closeSessionButton";
            this.closeSessionButton.Size = new System.Drawing.Size(92, 22);
            this.closeSessionButton.TabIndex = 1;
            this.closeSessionButton.Text = "Close Session";
            this.closeSessionButton.Click += new System.EventHandler(this.closeSession_Click);
            // 
            // stringToWriteLabel
            // 
            this.stringToWriteLabel.Location = new System.Drawing.Point(5, 34);
            this.stringToWriteLabel.Name = "stringToWriteLabel";
            this.stringToWriteLabel.Size = new System.Drawing.Size(165, 17);
            this.stringToWriteLabel.TabIndex = 8;
            this.stringToWriteLabel.Text = "String para enviar:";
            // 
            // stringToReadLabel
            // 
            this.stringToReadLabel.Location = new System.Drawing.Point(5, 117);
            this.stringToReadLabel.Name = "stringToReadLabel";
            this.stringToReadLabel.Size = new System.Drawing.Size(101, 15);
            this.stringToReadLabel.TabIndex = 9;
            this.stringToReadLabel.Text = "Read:";
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(233, 83);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(75, 23);
            this.btnAuto.TabIndex = 10;
            this.btnAuto.Text = "AUTO";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // btnSquare
            // 
            this.btnSquare.Location = new System.Drawing.Point(564, 134);
            this.btnSquare.Name = "btnSquare";
            this.btnSquare.Size = new System.Drawing.Size(75, 23);
            this.btnSquare.TabIndex = 11;
            this.btnSquare.Text = "Square";
            this.btnSquare.UseVisualStyleBackColor = true;
            this.btnSquare.Click += new System.EventHandler(this.btnSquare_Click);
            // 
            // btnRamp
            // 
            this.btnRamp.Location = new System.Drawing.Point(564, 163);
            this.btnRamp.Name = "btnRamp";
            this.btnRamp.Size = new System.Drawing.Size(75, 23);
            this.btnRamp.TabIndex = 12;
            this.btnRamp.Text = "Ramp";
            this.btnRamp.UseVisualStyleBackColor = true;
            this.btnRamp.Click += new System.EventHandler(this.btnRamp_Click);
            // 
            // btnNoise
            // 
            this.btnNoise.Location = new System.Drawing.Point(564, 192);
            this.btnNoise.Name = "btnNoise";
            this.btnNoise.Size = new System.Drawing.Size(75, 23);
            this.btnNoise.TabIndex = 13;
            this.btnNoise.Text = "Noise";
            this.btnNoise.UseVisualStyleBackColor = true;
            this.btnNoise.Click += new System.EventHandler(this.btnNoise_Click);
            // 
            // btnPulse
            // 
            this.btnPulse.Location = new System.Drawing.Point(564, 221);
            this.btnPulse.Name = "btnPulse";
            this.btnPulse.Size = new System.Drawing.Size(75, 23);
            this.btnPulse.TabIndex = 14;
            this.btnPulse.Text = "Pulse";
            this.btnPulse.UseVisualStyleBackColor = true;
            this.btnPulse.Click += new System.EventHandler(this.btnPulse_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(564, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Start Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // boxIP
            // 
            this.boxIP.Location = new System.Drawing.Point(458, 86);
            this.boxIP.Name = "boxIP";
            this.boxIP.Size = new System.Drawing.Size(100, 20);
            this.boxIP.TabIndex = 16;
            this.boxIP.Text = "127.0.0.1";
            // 
            // boxRemote
            // 
            this.boxRemote.Location = new System.Drawing.Point(376, 136);
            this.boxRemote.Name = "boxRemote";
            this.boxRemote.ReadOnly = true;
            this.boxRemote.Size = new System.Drawing.Size(182, 227);
            this.boxRemote.TabIndex = 17;
            this.boxRemote.Text = "";
            // 
            // boxTeste
            // 
            this.boxTeste.AutoSize = true;
            this.boxTeste.Location = new System.Drawing.Point(558, 111);
            this.boxTeste.Name = "boxTeste";
            this.boxTeste.Size = new System.Drawing.Size(53, 17);
            this.boxTeste.TabIndex = 18;
            this.boxTeste.Text = "Teste";
            this.boxTeste.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(645, 401);
            this.Controls.Add(this.boxTeste);
            this.Controls.Add(this.boxRemote);
            this.Controls.Add(this.boxIP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPulse);
            this.Controls.Add(this.btnNoise);
            this.Controls.Add(this.btnRamp);
            this.Controls.Add(this.btnSquare);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.stringToReadLabel);
            this.Controls.Add(this.stringToWriteLabel);
            this.Controls.Add(this.closeSessionButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.readTextBox);
            this.Controls.Add(this.writeTextBox);
            this.Controls.Add(this.openSessionButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.queryButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(295, 316);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eLAB";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
        string path = @"log.txt";
        public void receive()
        {
            // Create a TCP/IP listener.

            boxRemote.Text = "eLab Server Running...\n";

            var localAddress = IPAddress.Parse(boxIP.Text);
            var tcpListener = new TcpListener(localAddress, 5005);

            // Start listening for connections.
            tcpListener.Start();
            new Thread(() =>
              {

                  while (true)
                  {

                      // Program is suspended while waiting for an incoming connection.
                      var tcpClient = tcpListener.AcceptSocket();//.AcceptTcpClient();

                      
                      // An incoming connection needs to be processed.
                      
                      Thread thread = new Thread(() => ClientSession(tcpClient))
                      {
                          IsBackground = true
                      };
                      thread.Start();
                      
                      //ClientSession(tcpClient);
                  }
              }).Start();
        }

        private bool TryReadExact(Stream stream, byte[] buffer, int offset, int count, Socket tcpClient)
        {
            int bytesRead;

            while (tcpClient.Connected && count > 0 && ((bytesRead = stream.Read(buffer, offset, count)) > 0))
            {
                offset += bytesRead;
                count -= bytesRead;
            }

            return count == 0;
        }


        public void updateLog(string texto)
        {
            boxRemote.Text += texto + "\n";
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private void ClientSession(Socket tcpClient)
        {

            //this.Invoke((MethodInvoker)delegate { this.boxRemote.Text += "connected" + "\n"; });
            const int totalByteBuffer = 4096;
            byte[] buffer = new byte[256];
            

            using (var networkStream = new NetworkStream(tcpClient))//tcpClient.GetStream())
            using (var bufferedStream = new BufferedStream(networkStream, totalByteBuffer))
                while (true)
                {
                    try
                    {
                        
                            try
                            {
                                
                                    // Create a file to write to.

                                

                                // Receive header - byte length.
                                if (tcpClient.Connected)
                                {
                                    if (!TryReadExact(bufferedStream, buffer, 0, 1, tcpClient))
                                    {
                                        break;
                                    }
                                }
                                else break;
                                byte messageLen = buffer[0];
                                //this.Invoke((MethodInvoker)delegate { this.boxRemote.Text += messageLen + "\n"; });
                                // Receive the ASCII bytes.
                                if (tcpClient.Connected)
                                if (!TryReadExact(bufferedStream, buffer, 1, messageLen,tcpClient))
                                {
                                    break;
                                }
                                //this.Invoke((MethodInvoker)delegate { this.boxRemote.Text += "recv" + "\n"; });
                                
                                var message = Encoding.ASCII.GetString(buffer, 1, messageLen);
                                //MessageBox.Show("Client", "Message received: " + message);
                                string textToWrite = ReplaceCommonEscapeSequences(message);
                                //this.Invoke(() => { this.boxRemote += "hi"; });
                                //string msg = this.boxRemote.Text;
                                this.Invoke((MethodInvoker)delegate { this.boxRemote.Text += "MSG: " + message + "\n"; });
                                if (textToWrite.StartsWith("r_"))
                                {
                                    string responseString = mbSession.ReadString();
                                    if (tcpClient.Connected)  tcpClient.Send(System.Text.Encoding.ASCII.GetBytes(responseString));
                                    this.Invoke((MethodInvoker)delegate { writeLog(path, new string[] { "MSG: READ:" + textToWrite }); });
                                    //sw.WriteLine(textToWrite);

                                }
                                else if (textToWrite.StartsWith("q_"))
                                {
                                    //mbSession.q(textToWrite);
                                    string responseString = mbSession.Query(textToWrite.Replace("q_", ""));
                                    if (tcpClient.Connected)
                                        tcpClient.Send(System.Text.Encoding.ASCII.GetBytes(responseString));
                                    this.Invoke((MethodInvoker)delegate { writeLog(path, new string[] { "MSG: " + textToWrite }); });
                                    //sw.WriteLine(textToWrite);

                                }
                                else
                                {
                                    //mbSession.Write(textToWrite);
                                    if(!boxTeste.Checked)
                                        mbSession.Write(textToWrite);

                                    if (tcpClient.Connected)
                                        tcpClient.Send(System.Text.Encoding.ASCII.GetBytes("OK"));

                                    
                                    //this.Invoke((MethodInvoker)delegate { File.AppendAllText(path, "MSG: " + textToWrite + "\n"); });
                                    this.Invoke((MethodInvoker)delegate { writeLog(path, new string[] { "MSG: " + textToWrite }); });
                                    //sw.WriteLine(textToWrite);

                                }
                            }
                            catch(Exception e)
                            {
                                this.Invoke((MethodInvoker)delegate { writeLog(path, new string[] { "ERR: " + e.Message }); });
                                //this.Invoke((MethodInvoker)delegate { File.AppendAllText(path, "ERR: " + e.Message+"\n"); });
                                //sw.WriteLine(e.Message);

                                try
                                {
                                    if(tcpClient.Connected)
                                        tcpClient.Send(System.Text.Encoding.ASCII.GetBytes(e.Message));
                                    
                                }
                                catch { break; }
                            }
                        }
                       
                    
                    catch { break; }
                    
                }
        }


        public void writeLog(string path, string[] content)
        {
            List<string> content2 = new List<string>();
            foreach (var item in content)
            {
                content2.Add(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " ~ " + item);
            }

            File.AppendAllLines(path, content2 );
            
        }

        private void openSession_Click(object sender, System.EventArgs e)
        {
            using (SelectResource sr = new SelectResource())
            {
                if (lastResourceString != null)
                {
                    sr.ResourceName = lastResourceString;
                }
                DialogResult result = sr.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    lastResourceString = sr.ResourceName;
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(sr.ResourceName);
                        SetupControlState(true);

                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Resource selected must be a message-based session");
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }

        }

        private void closeSession_Click(object sender, System.EventArgs e)
        {
            SetupControlState(false);
            mbSession.Dispose();
        }

        private void query_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string textToWrite = ReplaceCommonEscapeSequences(writeTextBox.Text);
                string responseString = mbSession.Query(textToWrite);
                readTextBox.Text = InsertCommonEscapeSequences(responseString);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void write_Click(object sender, System.EventArgs e)
        {
            try
            {
                string textToWrite = ReplaceCommonEscapeSequences(writeTextBox.Text);
                mbSession.Write(textToWrite);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void read_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string responseString = mbSession.ReadString();
                readTextBox.Text = InsertCommonEscapeSequences(responseString);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void clear_Click(object sender, System.EventArgs e)
        {
            readTextBox.Text = String.Empty;
        }

        private void SetupControlState(bool isSessionOpen)
        {
            openSessionButton.Enabled = !isSessionOpen;
            closeSessionButton.Enabled = isSessionOpen;
            queryButton.Enabled = isSessionOpen;
            writeButton.Enabled = isSessionOpen;
            readButton.Enabled = isSessionOpen;
            writeTextBox.Enabled = isSessionOpen;
            clearButton.Enabled = isSessionOpen;
            if (isSessionOpen)
            {
                readTextBox.Text = String.Empty;
                writeTextBox.Focus();
            }
        }

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            string textToWrite = ReplaceCommonEscapeSequences(":AUToscale");
            mbSession.Write(textToWrite);
        }

        private void btnRamp_Click(object sender, EventArgs e)
        {

            string textToWrite = ReplaceCommonEscapeSequences("APPLy:RAMP");
            mbSession.Write(textToWrite);
        }

        private void btnNoise_Click(object sender, EventArgs e)
        {
            string textToWrite = ReplaceCommonEscapeSequences("APPLy:NOISe");
            mbSession.Write(textToWrite);
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            string textToWrite = ReplaceCommonEscapeSequences("APPLy:SQUare");
            mbSession.Write(textToWrite);
        }

        private void btnPulse_Click(object sender, EventArgs e)
        {
            string textToWrite = ReplaceCommonEscapeSequences("APPLy:PULSe");
            mbSession.Write(textToWrite);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            receive();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("deu certo");
                    }
                }
            }
            catch { }
        }
    }
}
