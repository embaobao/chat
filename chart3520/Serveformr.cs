using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace chart3520
{
    public partial class Serveformr : Form
    {
        private List<User> userlist = new List<User>();
        private IPAddress locaaddress;
        private int port = 51888;
        private TcpListener mylistener;
        private delegate void setservermessdelegate(string str);
        private delegate void setusermessdelegate(User user);
        private IPAddress ip = null;
        public Serveformr()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            listBox2.HorizontalScrollbar = true;
            listBox3.HorizontalScrollbar = true;
            IPAddress[] addrip = Dns.GetHostAddresses(string.Empty);
            locaaddress = addrip[0];
            button1.Enabled = true;
            button3.Enabled = false;
        }

        private void Serveformr_Load(object sender, EventArgs e)
        {
            tab1.SelectedIndex = 1;
           
            
            //button1.PerformClick();
            //button7.PerformClick();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FormLink.serverip))
            {
                ip = locaaddress;
            }
            else
            {
                IPAddress[] ips = Dns.GetHostAddresses(FormLink.serverip);
                ip = ips[0];
                port = FormLink.servesport;
            }
        //    MessageBox.Show(ip.ToString());
            try
            {
                mylistener = new TcpListener(ip, port);
                mylistener.Start();
                addservermess(string.Format("三五二零服务器启动-在{0}:{1}监听中！", ip, port));
            }
            catch (Exception)
            {
            }
           
            Thread mythread = new Thread(ConnecListen);
            mythread.Start();
            button1.Enabled = false;
            button3.Enabled = true;
        }




        private void ConnecListen()
        {
            TcpClient userclient = null;
            while (true)
            {

                try
                {
                    userclient = mylistener.AcceptTcpClient();
                }
                catch (Exception)
                {
                    break;
                }
                Thread t = new Thread(ReceiveDate);
                t.IsBackground = true;
                User user = new User(userclient);
                t.Start(user);
               // userlist.Add(user);
               // adduserlist(user);
                addserverlist(string.Format("[{0}进入]", userclient.Client.RemoteEndPoint));
                addserverlist(string.Format("当前连接数：{0}", userlist.Count));



            }



        }

        private void ReceiveDate(object use)
        {
            User user = (User)use;
            TcpClient client = user.client;
            bool normalexit = false;
            bool exitwhile = false;
            while (exitwhile == false)
            {
                string receivestring = null;
                try
                {
                    receivestring = user.br.ReadString();
                }
                catch (Exception)
                {

                    addserverlist("接受数据失败！");
                }
                if (receivestring == null)
                {
                    if (normalexit == false)
                    {
                        if (client.Connected == true)
                        {
                            addserverlist(string.Format("与{0}失去联系，已经终止接受该用户信息", client.Client.RemoteEndPoint));
                        }

                    }
                    break;
                }


                addserverlist(string.Format("来自[{0}]:{1}Ip:{2}", user.Name, receivestring, user.client.Client.RemoteEndPoint));
               // addserverlist("什么意思" + receivestring);

              Message ms = new Message(receivestring);
                switch (ms.Type)
                {
                    case (int)Message.messtypes.Login:
                       string name=Message.GetMessNa_Pw(ms.Txt)[0];
                        string pw=Message.GetMessNa_Pw(ms.Txt)[1];  
                        if(DataWrite.dblogin(name,pw))
                        {
                            SenderToClient(user, Message.CretemessStr(Message.messtypes.Login, true));
                            user.Name = Message.GetMessNa_Pw(ms.Txt)[0];
                            user.Pw = Message.GetMessNa_Pw(ms.Txt)[1];
                            SenderToClient(user, Message.CretemessToClient(string.Format(" 尊敬的{0}先生/女士，您好！迎来到三五二〇聊天室!", Message.GetMessNa_Pw(ms.Txt))));
                            SenderToClient(user, Message.CreteFriendToClient(user.Name));
                            addserverlist(Message.GetMessNa_Pw(ms.Txt)[0] + "登录服务器！");
                            SenderToClient(user,Message.CretemessToClientINFO("img",user.Name,DataWrite.getlocimgpath(user.Name).ToString()));
                            SenderToClient(user, Message.CretemessToClientINFO("txt", user.Name, DataWrite.gettxt(user.Name).ToString()));
                            userlist.Add(user);
                            adduserlist(user);
                        }
                            else
                        {
                            SenderToClient(user, Message.CretemessStr(Message.messtypes.Login,false));
                        }
                        
                        
                        break;
                    case (int)Message.messtypes.Logout:
                        normalexit = true;
                        exitwhile = true;
                        userlist.Remove(user);
                        removeuserlist(user);
                        addserverlist(string.Format("[{0}],退出", user.client.Client.RemoteEndPoint));
                        break;
                    case (int)Message.messtypes.Mess:
                        if (ms.From1.Contains("client") || ms.To1.Contains("server") || ms.From1.Contains("server") || ms.To1.Contains("client"))
                        {
                            SenderToClient(user, Message.CretemessToClient("您的消息已经送达！"));
                        }
                        else
                        {
                            string to = ms.To1;
                            string from = ms.From1;
                            senderToUser(to,ms.messtxt);
                        }
                        addserverlist(string.Format("用户{0}：向{1}发送消息为：{2}", ms.From1, ms.To1, ms.Txt));
                        break;
                    case (int)Message.messtypes.Friend:

                        if (!ms.From1.Contains("server")|| !ms.From1.Contains("client"))
                        {


                            if (ms.To1.Contains("search"))
                            {
                                SenderToClient(user,Message.CretesSearchFriendToClient(ms.From1,ms.Txt));
                              
                            }



                            if (ms.To1.Contains("add"))
                            {


                                string uname = ms.From1;
                                string addname = ms.Txt;
                                if (!DataWrite.Isregister(addname))
                                {
                                    clientshow(uname, "添加失败，查无此用户！");
                                }
                                else
                                {
                                    if (DataWrite.isF(uname, addname))
                                    {
                                        clientshow(uname, "您已经添加了这666小青年，不能再添加了");
                                    }
                                    else
                                    {
                                        DataWrite.dbaddfrined(uname,addname );
                                        clientshow(uname, "您已添加了这666小青年");
                                        SenderToClient(user, Message.CreteFriendToClient(uname));

                                    }
                                }


                            }
                           
                            

                        }
                       
                        break;
                    case (int)Message.messtypes.Register:
                        string[] txt = Message.GetMessNa_Pw(ms.Txt);
                        addserverlist(string.Format("用户向服务器注册：用户名：{0},密码：{1}",txt[0],txt[1]));
                        if (!DataWrite.Isregister(txt[0]))
                        {
                            if (DataWrite.dbregisters(txt[0], txt[1]))
                            {
                                addserverlist("用户向服务器注册成功！");
                                SenderToClient(user, Message.CretemessToClient("注册成功！"));
                            }
                            else
                            {
                                SenderToClient(user, Message.CretemessToClient("服务器开车去了,注册失败！"));
                            }
                        }
                        else
                        {
                            SenderToClient(user, Message.CretemessToClient("用户名存在，换一个名字吧？注册失败！"));
                        }
                        break;
                    case (int)Message.messtypes.info:
                        if (ms.To1.Contains("txt"))
                        {
                            DataWrite.dbuptxt(ms.From1, ms.Txt);
                            SenderToClient(user, Message.CretemessToClient("更新签名成功"));
                        }
                        if (ms.To1.Contains("img"))
                        {
                            DataWrite.dbuplocimg(ms.From1, ms.Txt);
                            SenderToClient(user, Message.CretemessToClient("更新头像成功"));
                        }

                        break;

                    default:
                        addserverlist("什么意思" + receivestring);
                    
                        break;


                }



            }
            userlist.Remove(user);
            removeuserlist(user);
            client.Close();
            addserverlist(string.Format("当前连接用户数：{0}", userlist.Count));
        }

        private void clientshow(string uname,string str)
        {
            senderToUser(uname, Message.CretemessToClient(str));
        }

        private void SenderToClient(User user, string str)
        {
            try
            {
                user.bw.Write(str);
                user.bw.Flush();
                addserverlist(string.Format("正在向{0}发送消息：{1},Ip:{2}", user.Name, str, user.client.Client.RemoteEndPoint));
            }
            catch (Exception)
            {
                addserverlist(string.Format("向{0}发送消息：{1}错误,Ip:{2}", user.Name, str, user.client.Client.RemoteEndPoint));
            }


        }




        private void addservermess(string str)
        {   
            listBox3.Items.Add(str);
            listBox3.SelectedIndex = listBox1.Items.Count - 1;
            this.listBox3.SelectedIndex = -1;
        }
        private void addusermess(User user)
        {

            listBox1.Items.Add(user.Name);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.ClearSelected();
        }
        private void removeuserfromlist(User user)
        {
            listBox1.Items.Remove(user.Name);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.ClearSelected();
        }
        private void addserverlist(string str)
        {
            try
            {
                setservermessdelegate d = new setservermessdelegate(addservermess);
                this.Invoke(d, str);
            }
            catch (Exception)
            {

                
            }
          
        }
        private void adduserlist(User user)
        {
            setusermessdelegate s = new setusermessdelegate(addusermess);
            this.Invoke(s, user);
        }
        private void removeuserlist(User user)
        {
            setusermessdelegate s = new setusermessdelegate(removeuserfromlist);
            this.Invoke(s, user);
        }
       /// <summary>
       /// 停止服务器
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void button3_Click(object sender, EventArgs e)
        {
            addserverlist(string.Format("目前连接用户数：{0}", userlist.Count));
            addserverlist("停止服务中,将退出每个用户");
            for (int i = 0; i < userlist.Count; i++)
            {

                SenderToClient(userlist[i], Message.CretemessToClientLogout( string.Format("尊敬的{0},由于管理员维护等原因关闭服务器，三五二〇服务器即将退出，请您稍后再登陆！谢谢您的谅解！",userlist[i].Name)));
                userlist[i].br.Close();
                userlist[i].bw.Close();
                userlist[i].client.Close();
            }
            mylistener.Stop();
            button1.Enabled = true;
            button3.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
         
            Form1 f = new Form1();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listBox1.SelectedItem!=null)
            {
                idtxt.Text = listBox1.SelectedItem.ToString();
            }


        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendbt_Click(object sender, EventArgs e)
        {
            string n = idtxt.Text.Trim().ToString();
            senderToUser(n,Message.CretemessToClient(messtxt.Text));
        }

        private void senderToUser(string name,string txt)
        {
            try
            {

                if (name.Length > 0)
                {
                    if (userlist.Count > 0)
                    {

                        for (int i = 0; i <userlist.Count; i++)
                        {

                            if (userlist[i].Name == name)
                            {
                                SenderToClient(userlist[i], txt);

                            }


                        }


                    }



                }
            }
            catch (Exception)
            {

                MessageBox.Show("发送失败！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetForm s = new SetForm();
            s.Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
