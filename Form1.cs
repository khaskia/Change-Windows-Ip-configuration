using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace change_IP {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            txtNetWork.Text = Properties.Settings.Default.NetworkName;
            textBox1.Text = Properties.Settings.Default.IpAddress;
            textBox2.Text = Properties.Settings.Default.GetWay;


        }

        Process p = new Process();
        private string device;

        public void executeCommands(string command) {


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            MessageBox.Show(cmd.StandardOutput.ReadToEnd());

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // string ipAddress = textBox1.Text.Split('.')[3];
            string[] arr = textBox2.Text.Split('.');
            arr[3] = "1";
            textBox2.Text = string.Join(".", arr);
            string getway = string.Join(".", arr);
            string ipaddress = textBox1.Text;
            string networkname = txtNetWork.Text;




            executeCommands("netsh interface ipv4 set address name=\"" + networkname + "\" source=static address=" + ipaddress + " mask=255.255.255.0 gateway=" + getway);

            Properties.Settings.Default.NetworkName = networkname;
            Properties.Settings.Default.IpAddress = ipaddress;
             Properties.Settings.Default.GetWay = getway;
            MessageBox.Show("Static is Done");

        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
           executeCommands("netsh interface ipv4 set address name=\"Wi-Fi\" source=dhcp");
           MessageBox.Show("Dynamic is Done");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://192.168.3.1");
        }
    }
}
