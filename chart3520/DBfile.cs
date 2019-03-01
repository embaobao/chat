using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace chart3520
{
    class DBfile
    {

        //
        /// <summary>
        /// 项目的启动位置
        /// </summary>
        public static string apppath = System.Windows.Forms.Application.StartupPath;
       /// <summary>
       /// 图像资源路径
       /// </summary>
       public static string apppimgath = System.Windows.Forms.Application.StartupPath+ @"\img\";
       /// <summary>
       /// 数据路径
       /// </summary>
       public static string DBPath = apppath + @"\DB\";
        public static string DBConfigPath = apppath + @"\DB\config.xml";
        public static string DBUserListPath = apppath + @"\DB\UserList.xml";
        public static void CreateUserListxml(string str)
        {
            if (!File.Exists(str))
            {
                XmlDocument userxml = new XmlDocument();
                XmlNode node = userxml.CreateXmlDeclaration("1.0", "utf-8", "");
                userxml.AppendChild(node);
                XmlElement root = userxml.CreateElement("UserList");
                userxml.AppendChild(root);
              //  CreateNode(userxml,root,);

            }
            else
            {


            }
           
        }

        public static void CNode(XmlDocument xmldoc,XmlElement parentnode,string name,string value,string attribute )
        {
          //  XmlElement u = xmldoc.CreateElement(name);

        }
        
        public static void CreateXmlFile(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Server");
            xmlDoc.AppendChild(root);
            CreateNode(xmlDoc, root, "ip", "");
            CreateNode(xmlDoc, root, "", "");
            CreateNode(xmlDoc, root, "", "");
            try
            {
                xmlDoc.Save(path);
            }
            catch (Exception e)
            {
                //显示错误信息  
          MessageBox.Show(e.Message);
            }
            //Console.ReadLine();  

        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        public static string xmlReader(string path,string node1,string node2)
        {
            
            XmlDataDocument doc = new XmlDataDocument();
            doc.Load(path);
            
            string txt = "";

       txt=doc.SelectSingleNode(node1).SelectSingleNode(node2).InnerText.ToString();

            return txt;
        }


        public static void UpdateNode(string Path, string NodeName, string NodeValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);
            XmlNode xn = doc.SelectSingleNode("//" + NodeName + "");
            xn.InnerText = NodeValue;
            doc.Save(Path);
        }
    }
}
