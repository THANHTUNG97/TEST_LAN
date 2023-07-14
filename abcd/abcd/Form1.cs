using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using EasyModbus;
using System.Diagnostics.Eventing.Reader;
using SimpleTCP.Server;
using System.Net.NetworkInformation;
using SimpleTCP;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace abcd
{
    public partial class Form1 : Form
    {
        public byte[] Txt1 = new byte[50];
        public byte[] Onl1 = new byte[50];
        public byte[] Rxt1 = new byte[512];
        public byte[] byte_sen1 = new byte[10];
        //public UdpClient ClientSoket = new UdpClient();
        public string ServerIPAddress1 = "192.168.1.1";// Properties.Settings.Default.IPaddress.ToString();
        public int PortNumber = Convert.ToInt16(Properties.Settings.Default.PortNumber);
        public ModbusClient ModClient1 = new ModbusClient();
        bool connected1 = true;
        public byte[] Txt2 = new byte[50];
        public byte[] Rxt2 = new byte[512];
        public byte[] byte_sen2 = new byte[10];
        public int sum;
        //public UdpClient ClientSoket = new UdpClient();
        public string ServerIPAddress2 = "192.168.1.52";// Properties.Settings.Default.IPaddress.ToString();
        public ModbusClient ModClient2 = new ModbusClient();
        bool connected2 = true;
        public bool nhan;
        public Form1()
        {
            InitializeComponent();
        }


        private void Send_Click(object sender, EventArgs e)
        {

            //NetworkStream stream = clien2.GetStream();
            //nhan = true;
            byte_sen1[0] = (byte)'@';
            byte_sen1[1] = 0;
            for (int i = 0; i < 8; i++)
            {
                byte_sen1[1] = ((byte)((byte_sen1[1] * 2) + Txt1[i]));
            }
            byte_sen1[2] = 0;
            for (int i = 0; i < 8; i++)
            {
                byte_sen1[2] = ((byte)(byte_sen1[2] * 2 + Txt1[i + 8]));
            }
            byte_sen1[3] = 0;
            for (int i = 0; i < 8; i++)
            {
                byte_sen1[3] = ((byte)(byte_sen1[3] * 2 + Txt1[i + 16]));
            }
            byte_sen1[4] = 0;
            byte_sen1[5] = 0;
            byte_sen1[6] = 0;
            byte_sen1[7] = 0;
            sum = (byte_sen1[1] + byte_sen1[2] + byte_sen1[3] + byte_sen1[4] + byte_sen1[5] + byte_sen1[6] + byte_sen1[7]) / 60;
            byte_sen1[8] = (byte)sum;
            byte_sen1[9] = (byte)'#';

            TcpClient client = new TcpClient();

            try
            {
                // Connect to the server
                client.Connect(ServerIPAddress1, PortNumber);

                // Get the network stream for sending and receiving data
                NetworkStream stream = client.GetStream();

                // Send data to the server

                stream.Write(byte_sen1, 0, byte_sen1.Length);
                //Console.WriteLine("Data sent to the server: " + sendData);

                // Receive data from the server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                // string receivedData = buffer[0].ToString()+"."+ buffer[1].ToString() + "." + buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString() + "." +
                //  buffer[6].ToString() + "." + buffer[7].ToString() + "." + buffer[8].ToString() + "." + buffer[9].ToString()  ;
                lstRegvalues.Items.Clear();
                for (int i = 0; i < bytesRead; i++)
                {
                    lstRegvalues.Items.Add(buffer[i].ToString());
                    Rxt1[i] = buffer[i];
                }

                //string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                //MessageBox.Show($"Data received from the server: {bytesRead} bytes " + receivedData);

                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the TCP client
                client.Close();
            }


        }

        private void tb1ON_Click(object sender, EventArgs e)
        {
            Txt1[0] = 1;
            tb1ON.Visible = false;
            tb1OFF.Visible = true;
        }

        private void tb1OFF_Click(object sender, EventArgs e)
        {
            Txt1[0] = 0;
            tb1ON.Visible = true;
            tb1OFF.Visible = false;
        }

        private void tb2ON_Click(object sender, EventArgs e)
        {
            Txt1[1] = 1;
            tb2ON.Visible = false;
            tb2OFF.Visible = true;
        }

        private void tb2OFF_Click(object sender, EventArgs e)
        {
            Txt1[1] = 0;
            tb2ON.Visible = true;
            tb2OFF.Visible = false;
        }

        private void tb3ON_Click(object sender, EventArgs e)
        {
            Txt1[2] = 1;
            tb3ON.Visible = false;
            tb3OFF.Visible = true;
        }

        private void tb3OFF_Click(object sender, EventArgs e)
        {
            Txt1[2] = 0;
            tb3ON.Visible = true;
            tb3OFF.Visible = false;
        }

        private void tb4ON_Click(object sender, EventArgs e)
        {
            Txt1[3] = 1;
            tb4ON.Visible = false;
            tb4OFF.Visible = true;
        }

        private void tb4OFF_Click(object sender, EventArgs e)
        {
            Txt1[3] = 0;
            tb4ON.Visible = true;
            tb4OFF.Visible = false;
        }

        private void tb5ON_Click(object sender, EventArgs e)
        {
            Txt1[4] = 1;
            tb5ON.Visible = false;
            tb5OFF.Visible = true;
        }
        private void tb5OFF_Click(object sender, EventArgs e)
        {
            Txt1[4] = 0;
            tb5ON.Visible = true;
            tb5OFF.Visible = false;
        }
        private void tb6ON_Click(object sender, EventArgs e)
        {
            Txt1[5] = 1;
            tb6ON.Visible = false;
            tb6OFF.Visible = true;
        }

        private void tb6OFF_Click(object sender, EventArgs e)
        {
            Txt1[5] = 0;
            tb6ON.Visible = true;
            tb6OFF.Visible = false;
        }

        private void tb7ON_Click(object sender, EventArgs e)
        {
            Txt1[6] = 1;
            tb7ON.Visible = false;
            tb7OFF.Visible = true;
        }

        private void tb7OFF_Click(object sender, EventArgs e)
        {
            Txt1[6] = 0;
            tb7ON.Visible = true;
            tb7OFF.Visible = false;
        }

        private void tb8ON_Click(object sender, EventArgs e)
        {
            Txt1[7] = 1;
            tb8ON.Visible = false;
            tb8OFF.Visible = true;
        }

        private void tb8OFF_Click(object sender, EventArgs e)
        {
            Txt1[7] = 0;
            tb8ON.Visible = true;
            tb8OFF.Visible = false;
        }

        void Form1_Load(object sender, EventArgs e)
        {

            Text1.Text = "Disconnected!";
            Text2.Text = "Disconnected!";
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

           
        }



        private void IP1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            ServerIPAddress1 = IP1.Text.ToString() + "." + IP2.Text.ToString() + "." + IP3.Text.ToString() + "." + IP4.Text.ToString();
            ModClient1.IPAddress = ServerIPAddress1;
            ModClient1.Port = PortNumber;
            try
            {
                ModClient1.Connect();
                if (ModClient1.Connected)
                {
                    Text1.Text = "Connected!";
                    //toolStripStatusConnect.Text = "Connected!";
                    //groupBox1.Enabled = true;
                    groupBox1.Show();
                    IPsetup.Enabled = false;
                    byte_sen1[0] = (byte)'@';
                    byte_sen1[1] = 0;
                    byte_sen1[2] = 0;
                    byte_sen1[3] = 0;
                    byte_sen1[4] = 0;
                    byte_sen1[5] = 0;
                    byte_sen1[6] = 0;
                    byte_sen1[7] = 0;
                    sum = (byte_sen1[1] + byte_sen1[2] + byte_sen1[3] + byte_sen1[4] + byte_sen1[5] + byte_sen1[6] + byte_sen1[7]) / 60;
                    byte_sen1[8] = (byte)sum;
                    byte_sen1[9] = (byte)'#';

                    TcpClient client = new TcpClient();

                    try
                    {
                        // Connect to the server
                        client.Connect(ServerIPAddress1, PortNumber);

                        // Get the network stream for sending and receiving data
                        NetworkStream stream = client.GetStream();

                        // Send data to the server

                        stream.Write(byte_sen1, 0, byte_sen1.Length);
                        //Console.WriteLine("Data sent to the server: " + sendData);

                        // Receive data from the server
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        // string receivedData = buffer[0].ToString()+"."+ buffer[1].ToString() + "." + buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString() + "." +
                        //  buffer[6].ToString() + "." + buffer[7].ToString() + "." + buffer[8].ToString() + "." + buffer[9].ToString()  ;
                        lstRegvalues.Items.Clear();
                        for (int i = 0; i < bytesRead; i++)
                        {
                            lstRegvalues.Items.Add(buffer[i].ToString());
                            Rxt1[i] = buffer[i];
                        }

                        //string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        //MessageBox.Show($"Data received from the server: {bytesRead} bytes " + receivedData);

                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        // Close the TCP client
                        client.Close();
                    }
                }

           
                //for (int i = 0; i < 6; i++)
             
                if (Txt1[0] == 1)
                {
                    tb1ON.Visible = false;
                    tb1OFF.Visible = true;
                }
                else
                {
                    tb1ON.Visible = true;
                    tb1OFF.Visible = false;
                }
                if (Txt1[1] == 1)
                {
                    tb2ON.Visible = false;
                    tb2OFF.Visible = true;
                }
                else
                {
                    tb2ON.Visible = true;
                    tb2OFF.Visible = false;
                }
                if (Txt1[2] == 1)
                {
                    tb3ON.Visible = false;
                    tb3OFF.Visible = true;
                }
                else
                {
                    tb3ON.Visible = true;
                    tb3OFF.Visible = false;
                }
                if (Txt1[3] == 1)
                {
                    tb4ON.Visible = false;
                    tb4OFF.Visible = true;
                }
                else
                {
                    tb4ON.Visible = true;
                    tb4OFF.Visible = false;
                }
                if (Txt1[4] == 1)
                {
                    tb5ON.Visible = false;
                    tb5OFF.Visible = true;
                }
                else
                {
                    tb5ON.Visible = true;
                    tb5OFF.Visible = false;
                }
                if (Txt1[5] == 1)
                {
                    tb6ON.Visible = false;
                    tb6OFF.Visible = true;
                }
                else
                {
                    tb6ON.Visible = true;
                    tb6OFF.Visible = false;
                }
                if (Txt1[6] == 1)
                {
                    tb7ON.Visible = false;
                    tb7OFF.Visible = true;
                }
                else
                {
                    tb7ON.Visible = true;
                    tb7OFF.Visible = false;
                }
                if (Txt1[7] == 1)
                {
                    tb8ON.Visible = false;
                    tb8OFF.Visible = true;
                }
                else
                {
                    tb8ON.Visible = true;
                    tb8OFF.Visible = false;
                }

            }
            catch (Exception ex)
            {

                Text1.Text = "Error Connection!";
                //toolStripStatusConnect.Text = "Error Connection!";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Save2_Click(object sender, EventArgs e)
        {
            ServerIPAddress2 = IP12.Text.ToString() + "." + IP22.Text.ToString() + "." + IP32.Text.ToString() + "." + IP42.Text.ToString();
            ModClient2.IPAddress = ServerIPAddress2;
            ModClient2.Port = PortNumber;
            try
            {
                ModClient2.Connect();
                if (ModClient2.Connected)
                {
                    Text2.Text = "Connected!";

                    //toolStripStatusConnect.Text = "Connected!";
                    groupBox2.Show();
                    IPsetup2.Enabled = false;
                    byte_sen2[0] = (byte)'@';
                    byte_sen2[1] = 0;
                    byte_sen2[2] = 0;
                    byte_sen2[3] = 0;
                    byte_sen2[4] = 0;
                    byte_sen2[5] = 0;
                    byte_sen2[6] = 0;
                    byte_sen2[7] = 16;
                    sum = (byte_sen2[1] + byte_sen2[2] + byte_sen2[3] + byte_sen2[4] + byte_sen2[5] + byte_sen2[6] + byte_sen2[7]) / 60;
                    byte_sen2[8] = (byte)sum;
                    byte_sen2[9] = (byte)'#';
                    TcpClient client = new TcpClient();

                    try
                    {
                        // Connect to the server
                        client.Connect(ServerIPAddress2, PortNumber);

                        // Get the network stream for sending and receiving data
                        NetworkStream stream = client.GetStream();

                        // Send data to the server

                        stream.Write(byte_sen2, 0, byte_sen2.Length);
                        //Console.WriteLine("Data sent to the server: " + sendData);

                        // Receive data from the server
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        // string receivedData = buffer[0].ToString()+"."+ buffer[1].ToString() + "." + buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString() + "." +
                        //  buffer[6].ToString() + "." + buffer[7].ToString() + "." + buffer[8].ToString() + "." + buffer[9].ToString()  ;
                        lstRegvalues.Items.Clear();
                        for (int i = 0; i < bytesRead; i++)
                        {
                            lstRegvalues.Items.Add(buffer[i].ToString());
                            Rxt2[i] = buffer[i];
                        }

                        //string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        //MessageBox.Show($"Data received from the server: {bytesRead} bytes " + receivedData);
                        for (int i = 0; i < 8; i++)
                        {
                            Txt2[7-i] = (byte)(Rxt2[1] % 2);
                            Rxt2[1] = (byte)(Rxt2[1] / 2);
                        }
                        client.Close();
                        if (Txt2[0] == 1)
                        {
                            tb1ON2.Visible = false;
                            tb1OFF.Visible = true;
                        }
                        else
                        {
                            tb1ON2.Visible = true;
                            tb1OFF2.Visible = false;
                        }
                        if (Txt2[1] == 1)
                        {
                            tb2ON2.Visible = false;
                            tb2OFF2.Visible = true;
                        }
                        else
                        {
                            tb2ON2.Visible = true;
                            tb2OFF2.Visible = false;
                        }
                        if (Txt2[2] == 1)
                        {
                            tb3ON2.Visible = false;
                            tb3OFF2.Visible = true;
                        }
                        else
                        {
                            tb3ON2.Visible = true;
                            tb3OFF2.Visible = false;
                        }
                        if (Txt2[3] == 1)
                        {
                            tb4ON2.Visible = false;
                            tb4OFF2.Visible = true;
                        }
                        else
                        {
                            tb4ON2.Visible = true;
                            tb4OFF2.Visible = false;
                        }
                        if (Txt2[4] == 1)
                        {
                            tb5ON2.Visible = false;
                            tb5OFF2.Visible = true;
                        }
                        else
                        {
                            tb5ON2.Visible = true;
                            tb5OFF2.Visible = false;
                        }
                        if (Txt2[5] == 1)
                        {
                            tb6ON2.Visible = false;
                            tb6OFF2.Visible = true;
                        }
                        else
                        {
                            tb6ON2.Visible = true;
                            tb6OFF2.Visible = false;
                        }
                        if (Txt2[6] == 1)
                        {
                            tb7ON2.Visible = false;
                            tb7OFF2.Visible = true;
                        }
                        else
                        {
                            tb7ON2.Visible = true;
                            tb7OFF2.Visible = false;
                        }
                        if (Txt2[7] == 1)
                        {
                            tb8ON2.Visible = false;
                            tb8OFF2.Visible = true;
                        }
                        else
                        {
                            tb8ON2.Visible = true;
                            tb8OFF2.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        // Close the TCP client
                        client.Close();
                    }
                }
               
             

            }
            catch (Exception ex)
            {
                Text2.Text = "Error Connection!";
                // toolStripStatusConnect.Text = "Error Connection!";
            }
        }

        private void Send2_Click(object sender, EventArgs e)
        {
            //NetworkStream stream = clien2.GetStream();
            //nhan = true;
            byte_sen2[0] = (byte)'@';
            byte_sen2[1] = 0;
            for (int i = 0; i < 8; i++)
            {
              byte_sen2[1] = ((byte)((byte_sen2[1] * 2) + Txt2[i]));
            }
            byte_sen2[2] = 0;
            for (int i = 0; i < 8; i++)
            {
              byte_sen2[2] = ((byte)(byte_sen2[2] * 2 + Txt2[i + 8]));
            }
            byte_sen2[3] = 0;
            for (int i = 0; i < 8; i++)
            {
              byte_sen2[3] = ((byte)(byte_sen2[3] * 2 + Txt2[i + 16]));
            }
            byte_sen2[4] = 0;
            byte_sen2[5] = 0;
            byte_sen2[6] = 0;
            byte_sen2[7] = 0;
            sum = (byte_sen2[1] + byte_sen2[2] + byte_sen2[3] + byte_sen2[4] + byte_sen2[5] + byte_sen2[6] + byte_sen2[7]) / 60;
            byte_sen2[8] = (byte)sum;
            byte_sen2[9] = (byte)'#';

            TcpClient client = new TcpClient();

            try
            {
                // Connect to the server
                client.Connect(ServerIPAddress2, PortNumber);

                // Get the network stream for sending and receiving data
                NetworkStream stream = client.GetStream();

                // Send data to the server
             
                stream.Write(byte_sen2, 0, byte_sen2.Length);
                //Console.WriteLine("Data sent to the server: " + sendData);

                // Receive data from the server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                // string receivedData = buffer[0].ToString()+"."+ buffer[1].ToString() + "." + buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString() + "." +
                //  buffer[6].ToString() + "." + buffer[7].ToString() + "." + buffer[8].ToString() + "." + buffer[9].ToString()  ;
                lstRegvalues.Items.Clear();
                for(int i=0;i< bytesRead; i++) 
                {
                    lstRegvalues.Items.Add(buffer[i].ToString());
                    Rxt2[i] = buffer[i];
                } 
                    
                //string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                //MessageBox.Show($"Data received from the server: {bytesRead} bytes " + receivedData);

                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the TCP client
                client.Close();
            }
        }

        private void Disconnect1_Click(object sender, EventArgs e)
        {
            try
            {
                Text1.Text = "Disconnected!";
                //toolStripStatusConnect.Text = "Disconnected!";
                // groupBox1.Enabled = false;
                groupBox1.Hide();

                IPsetup.Enabled = true;
                connected1 = false;
                ModClient1.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Failed to connect to the sever", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Text1_Click(object sender, EventArgs e)
        {

        }

        private void tb1OFF2_Click(object sender, EventArgs e)
        {
            Txt2[0] = 0;
            tb1ON2.Visible = true;
            tb1OFF2.Visible = false;
        }

        private void tb1ON2_Click(object sender, EventArgs e)
        {
            Txt2[0] = 1;
            tb1ON2.Visible = false;
            tb1OFF2.Visible = true;
        }

        private void tb2OFF2_Click(object sender, EventArgs e)
        {
            Txt2[1] = 0;
            tb2ON2.Visible = true;
            tb2OFF2.Visible = false;
        }

        private void tb2ON2_Click(object sender, EventArgs e)
        {
            Txt2[1] = 1;
            tb2ON2.Visible = false;
            tb2OFF2.Visible = true;
        }

        private void tb3OFF2_Click(object sender, EventArgs e)
        {
            Txt2[2] = 0;
            tb3ON2.Visible = true;
            tb3OFF2.Visible = false;
        }

        private void tb3ON2_Click(object sender, EventArgs e)
        {
            Txt2[2] = 1;
            tb3ON2.Visible = false;
            tb3OFF2.Visible = true;
        }

        private void tb4OFF2_Click(object sender, EventArgs e)
        {
            Txt2[3] = 0;
            tb4ON2.Visible = true;
            tb4OFF2.Visible = false;
        }

        private void tb4ON2_Click(object sender, EventArgs e)
        {
            Txt2[3] = 1;
            tb4ON2.Visible = false;
            tb4OFF2.Visible = true;
        }

        private void tb5OFF2_Click(object sender, EventArgs e)
        {
            Txt2[4] = 0;
            tb5ON2.Visible = true;
            tb5OFF2.Visible = false;
        }

        private void tb5ON2_Click(object sender, EventArgs e)
        {
            Txt2[4] = 1;
            tb5ON2.Visible = false;
            tb5OFF2.Visible = true;
        }

        private void tb6OFF2_Click(object sender, EventArgs e)
        {
            Txt2[5] = 0;
            tb6ON2.Visible = true;
            tb6OFF2.Visible = false;
        }

        private void tb6ON2_Click(object sender, EventArgs e)
        {
            Txt2[5] = 1;
            tb6ON2.Visible = false;
            tb6OFF2.Visible = true;
        }

        private void tb7OFF2_Click(object sender, EventArgs e)
        {
            Txt2[6] = 0;
            tb7ON2.Visible = true;
            tb7OFF2.Visible = false;
        }

        private void tb7ON2_Click(object sender, EventArgs e)
        {
            Txt2[6] = 1;
            tb7ON2.Visible = false;
            tb7OFF2.Visible = true;
        }

        private void tb8OFF2_Click(object sender, EventArgs e)
        {
            Txt2[7] = 0;
            tb8ON2.Visible = true;
            tb8OFF2.Visible = false;
        }

        private void tb8ON2_Click(object sender, EventArgs e)
        {
            Txt2[7] = 1;
            tb8ON2.Visible = false;
            tb8OFF2.Visible = true;
        }

        private void Disconnect2_Click(object sender, EventArgs e)
        {
            try
            {
                Text2.Text = "Disconnected!";
                //toolStripStatusConnect.Text = "Disconnected!";
                // groupBox1.Enabled = false;
                groupBox2.Hide();

                IPsetup2.Enabled = true;
                connected2 = false;
                ModClient2.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Failed to connect to the sever", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {
          
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void IPsetup2_Enter(object sender, EventArgs e)
        {



        }
        

    }
}
