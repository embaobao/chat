using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace chart3520
{
    class Client
    {

        private string ServerIp = null;

        public string ServerIp1
        {
            get { return ServerIp; }
            set { ServerIp = value; }
        }
        public IPAddress LocIp = null;
        private User userToServer = null;

       public User UserToServer
        {
            get { return userToServer; }
            set { userToServer = value; }
        }
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string pw = string.Empty;

        public string Pw
        {
            get { return pw; }
            set { pw = value; }
        }
        private string putstring = string.Empty;

        public string Putstring
        {
            get { return putstring; }
            set { putstring = value; }
        }
        private string instrin = string.Empty;

        public string Instrin
        {
            get { return instrin; }
            set { instrin = value; }
        }
        public Client(string ServerIp, string name,string pw)
        {
            this.ServerIp1 = ServerIp;
            this.name = name;
            this.pw = pw;
            IPAddress[] addrip = Dns.GetHostAddresses(string.Empty);
            LocIp = addrip[0];
            
        }

        //public string Name { get => name; set => name = value; }
        //public string Pw { get => pw; set => pw = value; }
        //public string Putstring { get => putstring; set => putstring = value; }
        //public string Instrin { get => instrin; set => instrin = value; }
        //internal User UserToServer { get => userToServer; set => userToServer = value; }
        //public string ServerIp1 { get => ServerIp; set => ServerIp = value; }

        public bool ConServer()
        {
           
            try
            {
                TcpClient ServerConT = new TcpClient(ServerIp1,51888);
                UserToServer = new User(ServerConT);
            }
            catch (Exception)
            {
                return false;
               
            }
            return true;
        }

        public void ReceiveData()
        { 

         while (true)
            {
                string receivestring = null;
                try
                {
                    receivestring = UserToServer.br.ReadString();
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
                                   MessageBox.Show ("成功登录服务器！账户正确");
                                }
                                else
                                {
                                    MessageBox.Show("登录服务器失败！账户错误");
                                }
                                break;
                            case (int)Message.messtypes.Logout:
                                if (!string.IsNullOrEmpty(ms.Txt))
                                {
                                    MessageBox.Show(ms.Txt);

                                }
                                Application.Exit();

                                break;
                            case (int)Message.messtypes.Mess:
                                MessageBox.Show(ms.Txt);
                                break;
                            default:
                               MessageBox.Show("未知格式消息：" + receivestring);
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
        public void Sendstring(string str)
        {
            try
            {
                UserToServer.bw.Write(str);
                UserToServer.bw.Flush();
              
            }
            catch (Exception)
            {

                return;
            }

        }


        public void SendMessLogin()
        {
            try
            {
                UserToServer.bw.Write(Message.CretemessToServerLogin(this.name,this.pw));
                UserToServer.bw.Flush();

            }
            catch (Exception)
            {

                return;
            }

        }
        public void SendMessRegister()
        {
            try
            {
                UserToServer.bw.Write(Message.CretemessToServerRegister(this.name,this.pw));
                UserToServer.bw.Flush();

            }
            catch (Exception)
            {

                return;
            }

        }





    }
}
