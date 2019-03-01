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
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Collections;

namespace chart3520
{
    public partial class myform : Form
    {
        private delegate void addfriendlistdelegate(User user);
        private delegate void addmesslistdelegate(string str);
        private delegate void addtxtdelagete(string str);
        private delegate void addbtmessdelagete(string str,string contxt,bool isself);
        private User my = null;
        private Client c = null;
        IPAddress locip = null;
        string  ip = null;
        int port = 51888;
        private bool isexit = false;

        public myform()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            listBox2.HorizontalScrollbar = true;
            textBox1.Text = string.Empty;
        }



        private void myform_Load(object sender, EventArgs e)
        {
            SetDouble(this);
            txtpanel.WrapContents = false;
            toolTip1.SetToolTip(addfbt, "点击添加好友");
            // IPAddress[] addrip = Dns.GetHostAddresses(string.Empty);
            // locip = addrip[0];
            this.Text = "We-Chart-By:" + FormLink.name;
            ip = FormLink.serverip; //locip.ToString();
            if (ip!= null)
            {
                ConneServer(ip,51888);
            }
           
            
         
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        private void ConneServer(string ip,int port)
        {
            try
            {
                 TcpClient ServerConT = new TcpClient(ip, port);
              //  TcpClient ServerConT = FormLink.client.UserToServer.client;
                my = new User(ServerConT)
                {
                    Name = FormLink.name,
                    Pw = FormLink.pw
                };
                namelb.Text = my.Name;
            }
            catch (Exception)
            {
                Addmesslist("与服务器连接失败！");
                MessageBox.Show("连接失败");
                this.Visible = false;
                Form1 f = new Form1();
                f.Show();
                this.Close();
                return;
            }
            Sendstring(Message.CretemessToServerLogin(my.Name,my.Pw));
            Thread t = new Thread(ReceiveDate);
            t.IsBackground = true;
            t.Start();
        }



     /// <summary>
     /// 设置双缓冲
     /// </summary>
     /// <param name="cc">控件</param>
     public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        /// <summary>
        /// 发送消息
        /// 
        /// </summary>
        private void ReceiveDate()
        {

            while (isexit == false)
            {
                string receivestring = null;
                try
                {
                    receivestring = my.br.ReadString();
                }
                catch
                {

                    Addmesslist("接受数据丢失！");
                }
                if (receivestring == null)
                {
                    if (isexit == false)
                    {
                        MessageBox.Show("与联系人失去联系！");

                    }
                    break;

                }
                Message ms = new Message(receivestring);
                switch (ms.Type)
                {
                    case (int)Message.messtypes.Login:
                        if (ms.Txt == true.ToString())
                        {
                            Addmesslist("欢迎您"+my.Name+ "来到3520we-Chart，您已成功进入3520的世界！");
                        }
                        else
                        {
                            Addmesslist("连接服务器错误，请您再次登陆！");
                            Thread.Sleep(2000);
                            Form1 f = new Form1();
                            f.Show();
                            this.Visible = false;
                            try
                            {
                                this.Dispose();
                                this.Close();
                            }
                            catch (Exception)
                            {

                            }
                          
                        }
                        break;
                    case (int)Message.messtypes.Logout:
                        if (!string.IsNullOrEmpty(ms.Txt))
                        {
                            MessageBox.Show(ms.Txt);
                        }
                        try
                        {
                            this.Close();
                        }
                        catch (Exception)
                        {


                        }
                        finally
                        {
                            this.Dispose();
                        }
                       
                        break;
                    case (int)Message.messtypes.Mess:
                        if (!ms.From1.Contains("server"))
                        {
                            refname(ms.From1);
                            try
                            {
                                if (this.Controls.Contains(GetUserContxtPanel(ms.From1)))
                                {
                                    GetUserContxtPanel(ms.From1).BringToFront();
                                    Addnewbtmess(ms.From1,ms.From1+":"+ms.Txt, false);
                                }
                                else
                                {
                                    addfrindpanel(ms.From1);
                                    GetUserContxtPanel(ms.From1).BringToFront();
                                    Addnewbtmess(ms.From1, ms.From1 + ":" +ms.Txt, false);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            Addmesslist(string.Format("{0}:{1}", ms.From1, ms.Txt));
                        }
                        else
                        {
                           Addmesslist("3520We-Char提示您："+ms.Txt)  ;
                        }
                     // AddTXT(string.Format("{0}对你说:{1}", ms.From1, ms.Txt));
                        break;
                    case (int)Message.messtypes.Friend:
                        // clearmesslist("");
                        if (ms.From1.Contains("server"))
                        {
                            clearFlist("");
                          //  Addmesslist(string.Format("{0}好友列表:{1}", ms.From1, ms.Txt));
                            string[] friendlist = ms.Txt.Split(';');
                            foreach (string friend in friendlist)
                            {
                                if (!string.IsNullOrEmpty(friend) && !string.IsNullOrWhiteSpace(friend))
                                {
                                    Addfriend(friend);
                                }
                               
                            }
                        }
                        else
                        {
                            if (ms.From1.Contains("search"))
                            {
                                Setserachbttxt(ms.Txt);

                            }


                        }
                        
                        
                        // AddTXT(string.Format("{0}对你说:{1}", ms.From1, ms.Txt));
                        break;
                    case (int)Message.messtypes.info:
                        if (ms.From1.Contains("txt"))
                        {
                            setqianming(ms.Txt);
                        }
                        if (ms.From1.Contains("img"))
                        {

                            setimg(ms.Txt);
                        }
                        break;
                    default:
                       Addmesslist("收到未知消息："+ receivestring);
                        break;


                }
                

                    

                    //Addmesslist("收到消息：" + receivestring);
                
            }




        }

        private void AddTXT(string str)
        {
          
            Label txt = new Label();
            txt.Width = txtpanel.Width;
            txt.Text = str;
            this.txtpanel.Controls.Add(txt);
        }

        
         private void Sendstring(string str)
        {
            try
            {
                my.bw.Write(str);
                my.bw.Flush();
              //  Addmesslist(string.Format("发送：{0}中",str));
            }
            catch (Exception)
            {
               
                Addmesslist("发送失败！");
            }

        }
         private string Readstring(int datalenth)
         {
             string receivestring = null;
             try
             {
                 receivestring = my.br.ReadString();
             }
             catch (Exception)
             {

                 Addmesslist("接受数据失败！");
             }
             return receivestring;

         }


        private void Addmesslist(string str)
        {
            if (listBox2.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(Addmesslist);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {

                    
                }
           
            }
            else
            {
                listBox2.Items.Add(str);
                listBox2.SelectedIndex = listBox1.Items.Count - 1;
             

            }

        }
    /// <summary>
    /// 设置个性签名
    /// </summary>
    /// <param name="str">个性签名内容</param>
    private void setqianming(string str)
        {
          
            if ( listBox1.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(setqianming);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(str);
            }

        }

      /// <summary>
      /// 设置头像
      /// </summary>
      /// <param name="str">头像路径</param>
      private void setimg(string str)
        {
            
                
            if ( pictureBox1.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(setimg);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                try
                {
                    if (System.IO.File.Exists(str))
                    {
                        pictureBox1.Image = Image.FromFile(str);
                    }
                    else
                    {
                      //  MessageBox.Show("erro: null source");
                    }
                 
                }
                catch (Exception)
                {
                    
                   
                }

            }

        }
     /// <summary>
     /// 清除消息列表
     /// </summary>
     /// <param name="str"></param>

       private void clearFlist(string str)
        {
            if (userlistpanel.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(clearFlist);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                userlistpanel.Controls.Clear();

            }

        }

      /// <summary>
      /// 设置搜素结果按钮
      /// </summary>
      /// <param name="str">按钮内容</param>
      private void Setserachbttxt(string str)
        {
            if (usertxtbt.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(Setserachbttxt);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                usertxtbt.Text = str;

            }

        }
       /// <summary>
       /// 多线程设置好友姓名lb
       /// </summary>
       /// <param name="str">好友姓名</param>
       private void refname(string str)
        {
            if (flb.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(refname);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                flb.Text = "";
                flb.Text = str;

            }

        }


    /// <summary>
    /// 清空消息列表
    /// </summary>
    /// <param name="str"></param>
    private void clearmesslist(string str)
        {
            if (listBox2.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(clearmesslist);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {


                }

            }
            else
            {
                listBox2.Items.Clear();
                listBox2.SelectedIndex = listBox1.Items.Count - 1;
            }

        }
     /// <summary>
     /// 添加好友按钮
     /// </summary>
     /// <param name="str">name</param>
     private void Addfriend(string str)
        {
            if (userlistpanel.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(Addfriend);
                try
                {
                    this.Invoke(d, str);
                }

                catch (Exception)
                {
                }
            }
            else
            {
                
                Button bt1 = new Button();
                SetDouble(bt1);
                bt1.BackColor = System.Drawing.Color.Transparent;
                bt1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                bt1.FlatAppearance.BorderSize = 0;
                bt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                bt1.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                bt1.Image = System.Drawing.Image.FromFile(DBfile.apppimgath+ @"好友框加下划线.png");
                bt1.Name = str;
                bt1.Size = new System.Drawing.Size(97, 28);
                bt1.TabIndex = 2;
                bt1.Text =str;
                bt1.UseVisualStyleBackColor = false;
                userlistpanel.Controls.Add(bt1);
                addfrindpanel(str);
                bt1.Click += new System.EventHandler(this.bt1_Click);
                this.Refresh();
            }

        }

        private void addfrindpanel(string str)
        {
            if (this.InvokeRequired)
            {

                addmesslistdelegate d = new addmesslistdelegate(addfrindpanel);
                try
                {
                    this.Invoke(d, str);
                }
                catch (Exception)
                {
                }
            }
            else
            {
               FlowLayoutPanel txtpane = new FlowLayoutPanel();
               Button bt = new Button();
               
               txtpane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            |  System.Windows.Forms.AnchorStyles.Left)
            |  System.Windows.Forms.AnchorStyles.Right)));
               txtpane.AutoScroll = true;
               txtpane.BackColor = System.Drawing.Color.Transparent;
               txtpane.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
               txtpane.Font = new System.Drawing.Font("等线 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               txtpane.Location = new Point(166, 130);
       //        txtpane.BackgroundImage = Image.FromFile(DBfile.apppimgath+ "ub.jpg");
      //   txtpane.BackgroundImageLayout = ImageLayout.Stretch;
               txtpane.Name = str+"ptxt";
               txtpane.WrapContents = false;
               txtpane.Visible = false;
               txtpane.Size = new System.Drawing.Size(621, 396);
               bt.FlatAppearance.BorderSize = 0;
               bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
               bt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
               bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
               bt.Location = new System.Drawing.Point(0, 0);
               bt.Margin = new System.Windows.Forms.Padding(0);
               bt.Name = str+"bt";
               bt.Size = new System.Drawing.Size(620, 23);
               bt.TabIndex = 0;
               bt.UseVisualStyleBackColor = false;
               bt.Text = "您与"+str+"的更多记忆..";
               txtpane.TabStop = true;
                SetDouble(txtpane);
                SetDouble(bt);
                txtpane.Controls.Add(bt);
               this.Controls.Add(txtpane);
              // bt.Click += new System.EventHandler(this.bt1_Click);
              this.Refresh();
            }

        }
      /// <summary>
      /// 给好友按钮注册的事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void bt1_Click(object sender, EventArgs e)
        {
            flb.Text = null;
            Button b = (Button)sender;
            string name= b.Text.ToString().Trim();
            flb.Text = name;
            // listBox2.Items.Clear();
            foreach (Control con in this.Controls)
            {
                if (con.Name.Contains("ptxt"))
                {
                    FlowLayoutPanel coo = (FlowLayoutPanel)con;
                    if (coo.Name.Contains(name))
                    {
                        coo.Visible = true;
                        //MessageBox.Show(name);
                        //MessageBox.Show(coo.Name);
                        coo.HorizontalScroll.Value = txtpanel.HorizontalScroll.Maximum;
                        coo.VerticalScroll.Value = txtpanel.VerticalScroll.Maximum;
                        coo.BringToFront();
                    }
                    else
                    {
                        coo.Visible = false;  
                    }

                }
            }       
                     this.Refresh();
                
            
        }
       /// <summary>
       /// 得到所有控件
       /// </summary>
       /// <param name="control">控件名</param>
       public void GetAllControls(Control control)
        {
            foreach (Control con in control.Controls)
            {
                if (con.Controls.Count > 0)
                {
                    GetAllControls(con);
                }
            }
           // btn.BringToFront();//将控件放置所有控件最前端  
           // btn.SendToBack();//将控件放置所有控件最底端
        }

        private void Addflist(User user)
        {
            if (listBox1.InvokeRequired)
            {

                addfriendlistdelegate d = new addfriendlistdelegate(Addflist);
                this.Invoke(d, user);
            }
            else
            {
                listBox1.Items.Add(user.Name);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.ClearSelected();

            }

        }

       /// <summary>
       /// 发送信息 点击事件
       /// </summary>
       /// <param name="sender">发送者</param>
       /// <param name="e">发送事件源</param>
       private void button1_Click(object sender, EventArgs e)
        {
          
            string txt = textBox1.Text.ToString();
            string to = flb.Text.ToString().Trim();
            string ms = "";
            Addnewbtmess(flb.Text, txt, true);
            if (txt.Contains(Message.splitestr))
            {
                txt = txt.Replace(Message.splitestr, "");

                if (to.Length > 0)
                {
                    ms = Message.CretemessTouser(my.Name, to, txt);
                }
            }
            else
            {

                if (to.Length > 0)
                {
                    ms = Message.CretemessTouser(my.Name, to, txt);
                }

                
            }

            Sendstring(ms);
            Addmesslist(namelb.Text + ":" + txt);
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            textBox1.Text = "";

        }

      /// <summary>
      /// 添加一个消息按钮在对话框
      /// </summary>
      /// <param name="name">好友姓名</param>
      /// <param name="isself">是自己发送的吗？</param>
      private void Addnewbtmess(string name,string contxt,bool isself)
        {
            if (GetUserContxtPanel(name) != null)
            {
            }
            else
            {
               
                return;
            }
            if (GetUserContxtPanel(name).InvokeRequired)
            {

                addbtmessdelagete d = new addbtmessdelagete(Addnewbtmess);
                object[] par = new object[] { name,contxt, isself };
                this.Invoke(d, par);
            }
            else
            {
                Button button = new Button();
              
                if (isself) {
                    button = setmessbt(button, my.Name+":"+contxt, "msb.jpg", "", 10F, true);
                   // button = setmessbt(button);
                }
                else
                {
                    button = setmessbt(button, contxt, "msb.jpg", "微软雅黑", 10F,false);
                    button.Anchor = System.Windows.Forms.AnchorStyles.Left;
                  
                }
                
                        FlowLayoutPanel coo = GetUserContxtPanel(name);
                        if (coo.Name == name + "ptxt")
                        {
                            coo.Visible = true;
                            coo.Controls.Add(button);
                            coo.BringToFront();
                            coo.HorizontalScroll.Value = txtpanel.HorizontalScroll.Maximum;
                            coo.VerticalScroll.Value = txtpanel.VerticalScroll.Maximum;

                        }
                        else
                        {
                            coo.Visible = false;
                            coo.SendToBack();
                        }


                Refresh();
            }


          
        }

     /// <summary>
     /// 设置默认消息按钮
     /// </summary>
     /// <param name="button">需要设置的按钮</param>
     /// <returns>设置过的按钮</returns>
     private Button setmessbt(Button button)
        {
            
            button.BackColor = System.Drawing.Color.Transparent;
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            button.Image = System.Drawing.Image.FromFile(DBfile.apppimgath + @"好友框加下划线.png");
            button.Size = new System.Drawing.Size(15 * (textBox1.Text.Length + namelb.Text.Length), 28);
            button.UseVisualStyleBackColor = false;
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button.Text = namelb.Text + ":" + textBox1.Text.ToString();
            button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            return button;
        }
    /// <summary>
    /// 设置消息按钮样式
    /// </summary>
    /// <param name="button">所设置按钮</param>
    /// <param name="contxt">按钮文本</param>
    /// <param name="bgimgpath">按钮背景</param>
    /// <param name="fontstyle">字体样式</param>
    /// <param name="fontsize">字体大小</param>
    /// <returns>设置后的按钮</returns>
    private Button setmessbt(Button button,string contxt,string bgimgpath,string fontstyle , float fontsize,bool isself)
        {
            int row = 1;
            int rowout = contxt.Length % 30;
            if (contxt.Length > 30)
            {
                row = contxt.Length / 30;

                for (int i = 1; i <= row; i++)
                {
                    contxt = contxt.Insert(30 * i, "\r\n");
                }
                if (rowout > 0)
                {
                    row++;
                }

            }

            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Name = "btmess";
          //  button.Size = new System.Drawing.Size(300, 12*row);
            button.TabIndex = 1;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         //   button.UseVisualStyleBackColor = true;
            button.Text = contxt;
            button.AllowDrop = true;
            button.AutoSize = false;
            button.AutoEllipsis = false;
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            string fontfaimly = "幼圆";
            if (getfontstyle().Contains(fontstyle))
            {
                fontfaimly = fontstyle;
            }
            button.Font = new System.Drawing.Font(fontfaimly, fontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            if (System.IO.File.Exists(DBfile.apppimgath + bgimgpath))
            {
                button.Image = System.Drawing.Image.FromFile(DBfile.apppimgath + bgimgpath);
            }
            else if ((System.IO.File.Exists(bgimgpath)))
            {
                button.BackgroundImage = System.Drawing.Image.FromFile(bgimgpath);
            }
            else {
                button.BackgroundImage = System.Drawing.Image.FromFile(DBfile.apppimgath + @"好友框加下划线.png");
            }
              button.Size = new System.Drawing.Size(30*10, 30*row);
         //   button.UseVisualStyleBackColor = false;
          
           
            if (isself)
            {
                button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            }
            else
            {
                button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            }
            return button;
        }

        public static ArrayList getfontstyle()
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            ArrayList list = new ArrayList();
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                string FontName = MyFontFamilies[i].Name;
                list.Add(FontName);
            }

            return list;
        }


        /// <summary>
        /// 得到用户好友对话框容器
        /// </summary>
        /// <param name="friendname">好友姓名</param>
        /// <returns></returns>
        private FlowLayoutPanel GetUserContxtPanel(string friendname)
        {
            if (this.Controls.Count > 0)
            {
                foreach (Control con in this.Controls)
                {
                    if (con.Name.Contains("ptxt"))
                    {
                        FlowLayoutPanel coo = (FlowLayoutPanel)con;
                        if (coo.Name==friendname+"ptxt")
                        {
                            return coo;
                        }
                    }
                }

            }

            return null;
        }

        private void myform_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (my.client != null)
                {
                    Sendstring(Message.CretemessToServerLogout());
                    isexit = true;
                    my.br.Close();
                    my.bw.Close();
                    my.client.Close();
                    this.Dispose();
                    Thread.Sleep(1000);
                    this.Close();
                    
                }
            }
            catch (Exception)
            {
                
              
            }
            
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            {

               
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.Width > 400 || this.Height > 500 || this.Visible == true)
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                // this.WindowState = FormWindowState.Minimized;
                this.Size = new Size(0, 0);
                this.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Width < 400 || this.Height < 500 || this.Visible == false)
            {
                this.ShowInTaskbar = true;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                this.Size = new Size(800, 680);
                this.Visible = true;
            }   
        }

        private void addfriendbt_Click(object sender, EventArgs e)
        {
            if (utxtpanel.Visible == false)
            {
                utxtpanel.Visible = true;
            }
            else
            {
                utxtpanel.Visible = false;
            }
            searchpl.BringToFront();
            if (searchpl.Visible == true)
            {
                searchpl.Visible = false;
            }
            else
            {
                searchpl.Visible = true;
            }
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]

        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void myform_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (searchtxt.Text.Trim().ToString().Length > 0)
            {

                try
                {
                    Sendstring(Message.CretemessToServerF(my.Name,"search",searchtxt.Text.Trim()));
                 
                }
                catch (Exception)
                {
                }
               
            }
            else
            {

            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (usertxtbt.Text.Trim().ToString() == "当前无匹配好友")
            {
                toolTip1.SetToolTip(usertxtbt, "当前无匹配好友");

            }
            else
            {
                searchtxt.Text= usertxtbt.Text;
                toolTip1.SetToolTip(usertxtbt, "点击加载到搜素框添加好友！");
                string addusername = searchtxt.Text.Trim().ToString();
                if (!string.IsNullOrEmpty(addusername))
                {
                    this.Sendstring(Message.CretemessToServerF(my.Name, "add", addusername));
                }
                else
                {
                    toolTip1.SetToolTip(button4, "不能为空！");

                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchpl.BringToFront();
            if (searchpl.Visible == true)
            {
                searchpl.Visible = false;
            }
            else
            {
                searchpl.Visible = true;
            }
            utxtpanel.BringToFront();
            if (utxtpanel.Visible == false)
            {
                utxtpanel.Visible = true;
            }
            else
            {
                utxtpanel.Visible = false;
            }
        }

        private void addfbt_Click(object sender, EventArgs e)
        {
            string addusername = searchtxt.Text.Trim().ToString();
            this.Sendstring(Message.CretemessToServerF(my.Name,"add",addusername));
 
        }

        private void userlistpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void servermessbt_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog1.FileName))
            {
            }
            else if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                string ms = Message.CretemessToServerINFO(my.Name, "img", openFileDialog1.FileName);
                Sendstring(ms);
            }
            else {

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            setqianming(qianmingtxt.Text.ToString());
            string ms = Message.CretemessToServerINFO(my.Name,"txt",qianmingtxt.Text);
            Sendstring(ms);
            if (utxtpanel.Visible == false)
            {
                utxtpanel.Visible = true;
            }
            else
            {
                utxtpanel.Visible = false;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (utxtpanel.Visible == false)
            {
                utxtpanel.Visible = true;
            }
            else
            {
                utxtpanel.Visible = false;
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void utxtpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
