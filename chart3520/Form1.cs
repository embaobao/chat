using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace chart3520
{
    public partial class Form1 : Form
    {
        IPAddress[] addrip = Dns.GetHostAddresses(string.Empty);
        IPAddress LocIp = null;
        Client user = null;
        public  DirectoryInfo Teamdir = new DirectoryInfo(Application.StartupPath + @"\Skins");
        public string  imgpath = Application.StartupPath + @"\img\";
        private delegate void addlbdelegate(string str);
        bool islog = false;
        private Thread t = null;
        string ip = "";

        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  Teamname_Load();
            timer1.Enabled = true;
            timer1.Interval = 10;
            LocIp = addrip[0];
            ip = LocIp.ToString();
            FormLink.serverip = ip;
        }




        


        private void Teamname_Load()
        {
            comboBox1.Items.Clear();
            try
            {
                foreach (FileInfo teamfile in Teamdir.GetFiles())
                {
                    comboBox1.Items.Add(teamfile.Name);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("加载主题错误");
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != null || comboBox1.SelectedItem.ToString() != "")
            {
                try
                {
                    string teamname = comboBox1.SelectedItem.ToString();
                    skinEngine1.SkinFile = Application.StartupPath + @"\Skins\" + teamname;
                    //MessageBox.Show("改变主题中...");
                    //Thread.Sleep(1000);
                    
                    // new Sunisoft.IrisSkin.SkinEngine().SkinFile = Application.StartupPath + @"\Skins\" + teamname;


                }
                catch (Exception)
                {

                    MessageBox.Show("应用内存不足，主题载入失败!");
                    skinEngine1.SkinFile = Application.StartupPath + @"\Skins\Warm.ssk";
                }
                finally
                {
                    this.Refresh();
                }
                this.Refresh();
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
           
            string name = idtxt.Text.ToString().Trim();
            string pw = pwtxt.Text.ToString().Trim();
            if (name=="3520")
            {
                SetForm s = new SetForm();
                s.Show();
            }
            else if (name.Length < 3)
            {
                MessageBox.Show("名字太少了，可记不清了，长些吧");
            }
            else if (name.Length > 10)
            {
                MessageBox.Show("名字太多了，可记不住，短些吧");
            }
            else if (pw.Length > 10)
            {
                MessageBox.Show("密码太长了，可记不清了，少些吧");
            }
            else if (pw.Length < 3)
            {
                MessageBox.Show("密码太短了，可不安全昂，多些吧");
            }
            else
            {

                try
                {
                    user = new Client(ip,name, pw);
                    if (user.ConServer())
                    {
                        FormLink.client = user;
                        user.SendMessLogin();
                        t = new Thread(ReceiveData);
                        t.IsBackground = true;
                        t.Start(user.UserToServer);
                    }
                    else
                    {

                        MessageBox.Show("网络问题，无法连接服务器，请检查网络设置。");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("网络问题，无法连接服务器，请检查网络设置。");
                }

            }
            
           
        }

       /// <summary>
       /// 注册
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void registerbt_Click(object sender, EventArgs e)
        {
            string name = idtxt.Text.ToString().Trim();
            string pw = pwtxt.Text.ToString().Trim();
            if (name.Length < 3)
            {
                MessageBox.Show("名字太少了，可记不清了，长些吧");
            }
            else if (name.Length > 10)
            {
                MessageBox.Show("名字太多了，可记不住，短些吧");
            }
            else if (pw.Length > 10)
            {
                MessageBox.Show("密码太长了，可记不清了，少些吧");
            }
            else if (pw.Length < 3)
            {
                MessageBox.Show("密码太短了，可不安全昂，多些吧");
            }
            else
            {

                try
                {
                    user = new Client(ip, name, pw);
                    if (user.ConServer())
                    {
                        user.SendMessRegister();
                        t = new Thread(ReceiveData);
                        t.IsBackground = true;
                        t.Start(user.UserToServer);
                    }
                    else
                    {

                        MessageBox.Show("网络问题，无法连接服务器，请检查网络设置。");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("网络问题，无法连接服务器，请检查网络设置。");
                }



            }
        }



        public void ReceiveData(object obj)
        {
            string name = idtxt.Text.ToString().Trim();
            string pw = pwtxt.Text.ToString().Trim();
            User u = (User)obj;
            while (true)
            {
                string receivestring = null;
                try
                {
                    receivestring = u.br.ReadString();
                }
                catch
                {
                    break;
                }
                if (!string.IsNullOrEmpty(receivestring))
                {
                    try
                    {
                        Message ms = new Message(receivestring);
                        switch (ms.Type)
                        {
                            case (int)Message.messtypes.Login:

                                if (ms.Txt == true.ToString())
                                {
                                    FormLink.user = this.user.UserToServer;
                                    FormLink.client = this.user;
                                    FormLink.name = name;
                                    FormLink.pw = pw;
                                    this.user.Sendstring(Message.CretemessToServerLogout()); 
                                    islog = true;
                                    addlb("成功登录服务器！");
                                    if (t.IsAlive)
                                    {
                                        t.Abort();
                                    }
                                    else
                                    {

                                    }
                                    Thread.Sleep(1000);
                                 
                                }
                                else
                                {
                                    islog = false;
                                    addlb("登录服务器失败！");

                                }
                                break;
                            case (int)Message.messtypes.Logout:
                                if (!string.IsNullOrEmpty(ms.Txt))
                                {
                                  addlb(ms.Txt);

                                }
                                Application.Exit();

                                break;
                            case (int)Message.messtypes.Mess:
                                addlb(ms.Txt);
                                break;
                            case (int)Message.messtypes.Register:
                                addlb(ms.Txt);
                                if (t.IsAlive)
                                {
                                    t.Abort();
                                }
                                else
                                {

                                }
                                break;
                            default:
                                addlb("未知格式消息：" + receivestring);
                                break;


                        }





                    }
                    catch (Exception)
                    {

                        break;
                    }
                 



                }
            }

        }


        private void addlb(string str)
        {
            if (txtlb.InvokeRequired)
            {
                addlbdelegate a = new addlbdelegate(addlb);
                this.Invoke(a, str);
            }
            else
            {
                txtlb.Text = "";
                txtlb.Text = str;

            }

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
         
        }

        private void closebt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minboxbt_Click(object sender, EventArgs e)
        {
            if (this.Width>400 || this.Height > 500 || this.Visible == true)
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
               // this.WindowState = FormWindowState.Minimized;
                this.Size = new Size(0,0);
                this.Visible = false;
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Width <400 || this.Height < 500 ||this.Visible==false)
            {
            this.ShowInTaskbar = true;  //不显示在系统任务栏
            notifyIcon1.Visible = true;  //托盘图标可见
            this.Size = new Size(401,551);
            this.Visible =true;
            }   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FormLink.serverip))
            {
            }
            else
            {
                IPAddress[] ips = Dns.GetHostAddresses(FormLink.serverip);
                ip = ips[0].ToString();
                FormLink.serverip = ip;
            }


            if (islog == true)
                {
                    Thread.Sleep(1000);
                    new myform().Show();
                    timer1.Enabled = false;
                    this.Close();
                }  
        }


        private void idtxt_MouseEnter(object sender, EventArgs e)
        {
            idtip.SetToolTip(idtxt, @"请输入ID\Name(3-9)");
        }

        private void pwtxt_MouseEnter(object sender, EventArgs e)
        {
            idtip.ToolTipTitle = "PassWord";
            idtip.SetToolTip(pwtxt, @"请输入PassWord(6-18)");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           

        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            idtip.ToolTipTitle = "记住密码";
            idtip.SetToolTip(checkBox1, @"请在安全的环境中选中记住密码");
        }

        private void loginbt_Enter(object sender, EventArgs e)
        {
            idtip.ToolTipTitle = "LogIn/Register";
            idtip.SetToolTip(loginbt, @"如您已经申请请登录，或输入ID-PassWord点击注册！");
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]

        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }
    }
}
