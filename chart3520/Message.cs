using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace chart3520
{
   public class Message 
    {
      //  public static string connectionmess = string.Empty;
       public  string messtxt;
        private int type;
        public static readonly string splitestr = "%#%";
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string From;
        public string From1
        {
            get { return From; }
            set { From = value; }
        }
        private string To;
        public string To1
        {
            get { return To; }
            set { To = value; }
        }
        private string txt;
        public string Txt
        {
            get { return txt; }
            set { txt = value; }
        }
   /// <summary>
   /// 消息类型
   /// </summary>
   public  enum messtypes { Login = 0, Logout = 1, Mess = 2 ,Register=3,Friend=4,info=5};
     /// <summary>
     /// 消息解析
     /// </summary>
     /// <param name="str"></param>
     public Message(string str)
        {
            
            try
            {
              
                messtxt = str;
               // string[] mess = str.Split('#');
                string[] mess = Regex.Split(str, splitestr, RegexOptions.IgnoreCase);
                this.type = Convert.ToInt32(mess[0]);
                this.From = mess[1];
                this.To = mess[2];
                this.txt = mess[3];
            }
            catch (Exception)
            {

                MessageBox.Show("消息:"+str+"格式非法");
            }
            
        }

   

      /// <summary>
      /// 创建消息，包含真假
      /// </summary>
      /// <param name="MessType">消息类型</param>
      /// <param name="status">消息状态</param>
      /// <returns></returns>
      public static string CretemessStr(messtypes MessType,bool status)
       {
        string mess=string.Empty;
        mess = (int)MessType +splitestr+splitestr+splitestr+ status.ToString();
        return mess;
       }
      /// <summary>
      /// 
      /// 创建标准信息</summary>
      /// <param name="MessType">类型</param>
      /// <param name="from">发送者</param>
      /// <param name="to">接受者</param>
      /// <param name="txt">内容</param>
      /// <returns></returns>
      public static string CretemessStr(messtypes MessType,string from,string to,string txt)
       {
           string mess = string.Empty;
           mess = (int)MessType +splitestr+from+ splitestr+to+ splitestr+txt+ splitestr;
           return mess;
       }

        public static string CretemessTouser(string from, string to, string txt)
        {
            string mess = string.Empty;
            mess = (int)messtypes.Mess + splitestr + from + splitestr + to + splitestr + txt + splitestr;
            return mess;
        }

        public static string CretemessToServerINFO(string from, string to, string txt)
        {
            string mess = string.Empty;
            mess = (int)messtypes.info+ splitestr + from + splitestr + to + splitestr + txt + splitestr;
            return mess;
        }
        public static string CretemessToClientINFO(string from, string to, string txt)
        {
            string mess = string.Empty;
            mess = (int)messtypes.info + splitestr + from + splitestr + to + splitestr + txt + splitestr;
            return mess;
        }
        public static string CretemessToClient(string txt)
       {
           string mess = string.Empty;
           mess = (int)messtypes.Mess + splitestr + "server" + splitestr + "client" + splitestr + txt + splitestr;
           return mess;
       }
        /// <summary>
        /// 创建好友列表消息
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        public static string CreteFriendToClient(string name)
        {
            string mess = string.Empty;
            string friend = "";
            try
            {
                // DataWrite.getf(6).ToString();
                friend = DataWrite.getflist(name).ToString();
                mess = (int)messtypes.Friend + splitestr + "server" + splitestr + "client" + splitestr + friend + splitestr;
            }
            catch (Exception)
            {
                mess = (int)messtypes.Friend + splitestr + "server" + splitestr + "client" + splitestr + "ero;"+ splitestr;
                throw;
            }

            return mess;
        }


        public static string CretesSearchFriendToClient(string name,string txt)
        {
            string mess = string.Empty;
            string friend = "";
            try
            {
                // DataWrite.getf(6).ToString();
                string users = DataWrite.getnames(txt).ToString();
                if (users.Length > 0)
                {
                    friend = users;
                }
                else
                {
                    friend = "无匹配好友";
                }
                mess = (int)messtypes.Friend + splitestr + "search" + splitestr + name + splitestr + friend + splitestr;
            }
            catch (Exception)
            {
                mess = (int)messtypes.Friend + splitestr + "server" + splitestr + "client" + splitestr + "ero;" + splitestr;
                throw;
            }

            return mess;
        }
        public static string CretemessToServer(string txt)
       {
           string mess = string.Empty;
           mess = (int)messtypes.Mess + splitestr + "client" + splitestr + "server" + splitestr + txt + splitestr;
           return mess;
       }
       public static string CretemessToServerLogin(string name,string pw)
       {
           string mess = string.Empty;
           mess = (int)messtypes.Login + splitestr + "client" + splitestr + "server" + splitestr + "name"+name+";pw"+pw+ splitestr;
           return mess;
       }
        public static string CretemessToServerRegister(string name, string pw)
        {
            string mess = string.Empty;
            mess = (int)messtypes.Register+ splitestr + "client" + splitestr + "server" + splitestr + "name" + name + ";pw" + pw + splitestr;
            return mess;
        }
        public static string CretemessToServerLogout(string name, string pw)
       {
           string mess = string.Empty;
           mess = (int)messtypes.Logout+ splitestr + "client" + splitestr + "server" + splitestr + "name" + name + ";pw" + pw + splitestr;
           return mess;
       }
       public static string CretemessToServerLogout()
       {
           string mess = string.Empty;
           mess = (int)messtypes.Logout + splitestr + "client" + splitestr + "server" + splitestr+ splitestr;
           return mess;
       }

        public static string CretemessToServerF(string name)
        {
            string mess = string.Empty;
            mess = (int)messtypes.Friend + splitestr + name + splitestr + "server" + splitestr + splitestr;
            return mess;
        }
        public static string CretemessToServerF(string name,string contro,string conname)
        {
            string mess = string.Empty;
            mess = (int)messtypes.Friend + splitestr + name + splitestr + contro + splitestr +conname+ splitestr;
            return mess;
        }

        public static string CretemessToClientLogout()
       {
           string mess = string.Empty;
           mess = (int)messtypes.Logout + splitestr + "server" + splitestr + "client" + splitestr + splitestr;
           return mess;
       }
     /// <summary>
     /// 创建登出信息给客户端
     /// </summary>
     /// <param name="txt"></param>
     /// <returns></returns>
     public static string CretemessToClientLogout(string txt)
       {
           string mess = string.Empty;
           mess = (int)messtypes.Logout + splitestr + "server" + splitestr + "client" + splitestr +txt+ splitestr;
           return mess;
       }


     /// <summary>
     /// 得到信息中账户密码 
     /// </summary>
     /// <param name="str">登录信息</param>
     /// <returns>数组【0】账户，【1】密码</returns>
     public static string[] GetMessNa_Pw(string str)
       {
           string[] strs = new string[2]; 
           if (!string.IsNullOrEmpty(str))
           {
               strs = str.Split(';');
               strs[0] = strs[0].Replace("name", "");
               strs[1] = strs[1].Replace("pw", "");
           }
           else
           {
               strs[0] = "";
               strs[1] = "";
           }
           
       return strs;
       }

    }
  
}
