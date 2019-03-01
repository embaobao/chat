using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace chart3520
{
    class DataWrite
    {
     
       /// <summary>
       /// 数据库登录
       /// </summary>
       /// <param name="na">账户名</param>
       /// <param name="p">密码</param>
       /// <returns>成功为true 反之false</returns>
       public static bool dblogin(string na, string p)
        {
            string name = na;
            string pw = p;
            int n = 0;
            try
            {
                string sqlcoms = string.Format(@"select COUNT(*) from userstb where uname='{0}' and upw='{1}'", name, pw);
                n = Convert.ToInt32(SqlDbHelper.ExecuteScalar(sqlcoms));
            }
            catch (Exception)
            {
                return false;
            }
            if (n > 0)
            {
                return true;
            }
            return false;
        }

     /// <summary>
     /// 数据库注册
     /// </summary>
     /// <param name="na">用户名</param>
     /// <param name="p">用户密码</param>
     /// <returns>成功true 反之 false</returns>
     public static bool dbregister(string na, string p)
        {
            string name = na;
            string pw = p;
            try
            {
                string sqlcom = string.Format("insert into userstb(uname,upw) values('{0}','{1}')", name, pw);
                SqlDbHelper.ExecuteNonQuery(sqlcom);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 多次写入注册法，现与数据库注册相同（已弃用）
        /// </summary>
        /// <param name="na">用户名</param>
        /// <param name="p">用户密码</param>
        /// <returns></returns>
        public static bool dbregisters(string na, string p)
        {
            string name = na;
            string pw = p;
            try
            {
                string sqlcom = string.Format("insert into userstb(uname,upw) values('{0}','{1}')", name, pw);
                SqlDbHelper.ExecuteNonQuery(sqlcom);
                //string sqlid = string.Format("select id from userstb where uname='{0}' and upw='{1}'", name, pw);
                //int id = Convert.ToInt32(SqlDbHelper.ExecuteScalar(sqlid));
                //string sqladdft = string.Format("insert into ftb(id,friend) values('{0}','{1}')", id, "");
                //SqlDbHelper.ExecuteNonQuery(sqladdft);
                //string sqladdimg = string.Format("insert into ums(name,uimg,umess) values('{0}','{1}','{2}')", id, "","");
                //  SqlDbHelper.ExecuteNonQuery(sqladdimg);
                //已优化，由数据库处理
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 用户名巳经注册？
        /// </summary>
        /// <param name="na">用户名</param>
        /// <returns>是 true 反之false</returns>
        public static bool Isregister(string na)
        {
            string name = na;
            int n=0;
            try
            {
                string sqlcoms = string.Format(@"select COUNT(*) from userstb where uname='{0}'", name);
                n = Convert.ToInt32(SqlDbHelper.ExecuteScalar(sqlcoms));
            }
            catch (Exception)
            {
                return true;
                throw;
            }
            if (n > 0)
            {
                return true;
            }
            return false;
        }


     /// <summary>
     ///数据库增加用户好友
     /// </summary>
     /// <param name="na">操作用户</param>
     /// <param name="addname">添加的用户名</param>
     /// <returns></returns>
     public static bool dbaddfrined(string na, string addname)
        {
            try
            {

               string userfl = getflist(na)+ addname + ";";
                string sqladdft = string.Format(" update userfm set uflist = '{0}' where uname = '{1}'", userfl,na);
                SqlDbHelper.ExecuteNonQuery(sqladdft);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    /// <summary>
    /// 更新数据库用户签名
    /// </summary>
    /// <param name="na">用户名</param>
    /// <param name="txt">跟新文字</param>
    /// <returns>返回是更新 true 成功 反之false 失败</returns>
    public static bool dbuptxt(string na, string txt)
        {
            string name = na;
            try
            {
                string sqladdft = string.Format(" update userinfo  set utxt='{0}' where uname = '{1}'", txt, na);
                SqlDbHelper.ExecuteNonQuery(sqladdft);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
      /// <summary>
      /// 更新数据库用户本地图片头像位置
      /// </summary>
      /// <param name="na"></param>
      /// <param name="txt"></param>
      /// <returns></returns>
      public static bool dbuplocimg(string na, string txt)
        {
          
            try
            {
                string sqladdft = string.Format(" update userinfo set ulocimgpath='{0}' where uname = '{1}'", txt, na);
                SqlDbHelper.ExecuteNonQuery(sqladdft);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 弃用方法，数据库更新为name为唯一标识符
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object getuid(string name)
        {
            string sqlid = string.Format("select * from userstb where uname='{0}'", name);
            int id = Convert.ToInt32(SqlDbHelper.ExecuteScalar(sqlid));
            return id;
        }


     /// <summary>
     /// 得到用户列表
     /// </summary>
     /// <param name="name">用户名</param>
     /// <returns>获得用户列别字符串</returns>
     public static string getflist(string name)
    {
            string sqlf = string.Format("select uflist from userfm where uname='{0}'", name);
            string userfl = "";
            SqlDataReader reader = SqlDbHelper.ExecuteReader(sqlf, new SqlConnection(SqlDbHelper.connectionString));

            if (reader != null)
            {
                if (reader.Read())
                {
                   userfl = reader.GetString(0);
                }
            }
            return userfl;
    }




      /// <summary>
      /// 获得用户本地的头像存储地址
      /// </summary>
      /// <param name="name">用户名</param>
      /// <returns>地址path 类型 string</returns>
      public static string getlocimgpath(string name)
        {
            string sqlf = string.Format("select ulocimgpath from userinfo where uname='{0}'", name);
            string imgpath= "";
            SqlDataReader reader = SqlDbHelper.ExecuteReader(sqlf, new SqlConnection(SqlDbHelper.connectionString));

            if (reader != null)
            {
                if (reader.Read())
                {
                  imgpath= reader.GetString(0);
                }
            }
          


            return imgpath;
        }

      /// <summary>
      /// 获得用户的数据库个性签名 
      /// </summary>
      /// <param name="name">用户名</param>
      /// <returns>返回签字字符</returns>
      public static object gettxt(string  name)
        {
            string sqlf = string.Format("select utxt from userinfo where uname='{0}'", name);
            string txt = "";
            SqlDataReader reader = SqlDbHelper.ExecuteReader(sqlf, new SqlConnection(SqlDbHelper.connectionString));

            if (reader != null)
            {
                if (reader.Read())
                {
                   txt = reader.GetString(0);
                }
            }



            return txt;
        }



        /// <summary>
        /// 弃用方法得到用户列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static string Getf(string name)
        //   {
        //       //int id = (int)getid(name);

        //       //string userfl = getf(id).ToString();
        //       return userfl;
        //   }

        /// <summary>
        /// 这个字符串是一个数字吗？
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是 true，不是 false</returns>
        public static bool isnum(string str)
        {

            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }
     /// <summary>
     /// 查找用户名
     /// </summary>
     /// <param name="str">输入用户关键字</param>
     /// <returns>返回匹配用户名string</returns>
     public static object getnames(string str)
        {
           
            string users = "";

            try
            {
                string sqlf = string.Format("select uname from userstb where uname like '%{0}%'", str);
       
                SqlDataReader reader = SqlDbHelper.ExecuteReader(sqlf, new SqlConnection(SqlDbHelper.connectionString));

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        users = reader.GetString(0);
                    }
                }
            }
            catch (Exception)
            {

              //  throw;
            }
            

            return users;

        }


   /// <summary>
   /// 是否是好友？
   /// </summary>
   /// <param name="name">操作用户名</param>
   /// <param name="havaf">好友名</param>
   /// <returns>true 是，false 不是</returns>
   public static bool isF(string name,string havaf)
        {
          string fl=getflist(name).ToString();
            string[] fs = fl.Split(';');
            foreach (string f in fs)
            {
                if (f == havaf)
                {
                    return true;
                } 
            }

            return false;
        }
    }
}
