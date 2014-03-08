using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace adflybot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WinInetInterop.SetConnectionProxy("localhost:8888");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();

            button1.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox7.Enabled = false;
            textBox6.Enabled = false;
            richTextBox1.Enabled = false;
        }

        int p = 0;
        int proxNum;
        private int txtBxNum = 0;
        OpenFileDialog ofd = new OpenFileDialog();
            
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] ProxList = File.ReadAllLines(ofd.FileName.ToString());
                    
            string[] s = new string[5];            
            WinInetInterop.SetConnectionProxy(ProxList[p]);

            proxNum = Convert.ToInt32(textBox6.Text);

            if (p > proxNum)
                p = 0;

            s[0] = textBox1.Text;
            s[1] = textBox2.Text;
            s[2] = textBox3.Text;
            s[3] = textBox4.Text;
            s[4] = textBox7.Text;

            try
            {
                webBrowser1.Navigate(s[txtBxNum]);

                textBox5.Text = webBrowser1.Url.ToString();//s[txtBxNum].ToString();
                label2.Text = "Current IP: " + ProxList[p].ToString();

                timer2.Start();                
                timer2.Stop();

                txtBxNum++;

                if (txtBxNum > 4)
                    txtBxNum = 0;
            }
            catch { }

            p++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Document.GetElementById("skip_ad_button").InvokeMember("click");
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            button1.Enabled = true;
            button3.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox7.Enabled = true;
            textBox6.Enabled = true;
            richTextBox1.Enabled = true;
        }

        void proxDir()
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(File.OpenRead(ofd.FileName));
                richTextBox1.Text = sr.ReadToEnd();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            proxDir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instructions:\r\n1) Load the proxy text file and enter the amout of proxies (if not sure take an estimate, make sure it is lower than actual number).\r\n2) Put your 4 adfly links\r\n3) Press start\r\n \r\nIf you are having problems contact FJam at etechforum.com\r\nFor Updated IP lists visit etechforum.com", "Instructions");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            System.Net.WebClient Wc = new System.Net.WebClient();
            string hwid = Wc.DownloadString("http://etechforum.com/HWID.txt");
            if (hwid.Contains(cpuInfo))
            {
            }
            else
            {
                MessageBox.Show("Your HWID Doesn't exist in the database");
                Environment.Exit(-1);
            }
            Wc.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
