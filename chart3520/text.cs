using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace chart3520
{
    public partial class SetForm : Form
    {
        public SetForm()
        {
            InitializeComponent();
        }

        private void SetForm_Load(object sender, EventArgs e)
        {

            //string s = "12%#%34568%#%15%3";
            //string[] sArray = Regex.Split(s, "%#%", RegexOptions.IgnoreCase);
            //foreach (string i in sArray)
            //{
            //    MessageBox.Show(i.ToString() + "\r\n");
            //}



        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ip = iptxt.Text.Trim().ToString();
            int port = 51888;
            try
            {
                 port = Convert.ToInt32(porttxt.Text.Trim());
            }
            catch (Exception)
            {

                MessageBox.Show("端口号不正确");
            }
            FormLink.serverip = ip;
            FormLink.servesport = port;

            MessageBox.Show("保存成功！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            iptxt.Text = "192.168.31.131";
            porttxt.Text = "51888";
            MessageBox.Show("重置成功！");
        }
    }
}
