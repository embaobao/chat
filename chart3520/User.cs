using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace chart3520
{
    class User
    {
        private string id = "0";

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string pw = "";

        public string Pw
        {
            get { return pw; }
            set { pw = value; }
        }
        private string name = "客户端";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        //public string Id { get => id; set => id = value; }
        //public string Pw { get => pw; set => pw = value; }
        //public string Name { get => name; set => name = value; }

        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream netstream = client.GetStream();
            br = new BinaryReader(netstream);
            bw = new BinaryWriter(netstream);
        }
    /// <summary>
    /// 初始化用户
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pw"></param>
    /// <param name="userclient"></param>
    public User(string name, string pw, TcpClient userclient)
        {
            this.Name = name;
            this.Pw = pw;
            this.client = userclient;
        
        }
        public void Setname(string name)
        {
            this.Name = name;
        }
        public void setTcpclient(TcpClient t)
        {
            this.client = t;
        
        }


    }
}
