using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace UDP_test
{
    public partial class FrmCheck : Form
    {
        public FrmCheck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UdpClient client = new UdpClient();
            try
            {
                client.Connect("212.247.59.3", 514);
                MessageBox.Show("Port open");
            }
            catch (Exception)
            {
                MessageBox.Show("Port closed");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("212.247.59.3", 443);
                MessageBox.Show("Port open");
            }
            catch (Exception)
            {
                MessageBox.Show("Port closed");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool test = p("212.247.59.1", 514);
            if (test)
            {
                MessageBox.Show("Port open");
            }
            else
            {
                MessageBox.Show("Port closed");
            }
        }

        public static bool p(string url, int port)
        {
            int timeout = 1000;
            var result = false;
            using (var client = new TcpClient())
            {
                try
                {
                    client.ReceiveTimeout = timeout * 1000;
                    client.SendTimeout = timeout * 1000;
                    var asyncResult = client.BeginConnect(url, port, null, null);
                    var waitHandle = asyncResult.AsyncWaitHandle;
                    try
                    {
                        if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(timeout), false))
                        {
                            // wait handle didn't came back in time
                            client.Close();
                        }
                        else
                        {
                            // The result was positiv
                            result = client.Connected;
                        }
                        // ensure the ending-call
                        client.EndConnect(asyncResult);
                    }
                    finally
                    {
                        // Ensure to close the wait handle.
                        waitHandle.Close();
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient("212.247.59.1", 80);
                MessageBox.Show("Port open");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Port closed");
            }
        }

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutCheck f = new AboutCheck();
            f.ShowDialog(this);
        }

        private void FrmCheck_Load(object sender, EventArgs e)
        {

        }

        private void testaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chechHTTPS();
        }

        public void TCP_Check()
        {
            TcpClient tcpClient = new TcpClient();
            //TCP 80
            try
            {
                tcpClient.Connect("212.247.59.3", 80);
                MessageBox.Show("Port open");
            }
            catch (Exception)
            {
                MessageBox.Show("Port closed");
            }
            //TCP 443
            try
            {
                tcpClient.Connect("212.247.59.3", 443);
                MessageBox.Show("Port open");
            }
            catch (Exception)
            {
                MessageBox.Show("Port closed");
            }
        }

        private void chechHTTPS()
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 80);
                MessageBox.Show("Port open");
            }
            catch (Exception)
            {
                MessageBox.Show("Port closed");
            }

        }
    }
}
