namespace chart3520
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.loginbt = new System.Windows.Forms.Button();
            this.registerbt = new System.Windows.Forms.Button();
            this.repassbt = new System.Windows.Forms.Button();
            this.idtxt = new System.Windows.Forms.TextBox();
            this.pwtxt = new System.Windows.Forms.TextBox();
            this.minboxbt = new System.Windows.Forms.Button();
            this.closebt = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtlb = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.idtip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(787, 35);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 25);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(850, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "主题：";
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // loginbt
            // 
            this.loginbt.BackColor = System.Drawing.Color.Transparent;
            this.loginbt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loginbt.FlatAppearance.BorderSize = 0;
            this.loginbt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginbt.Image = ((System.Drawing.Image)(resources.GetObject("loginbt.Image")));
            this.loginbt.Location = new System.Drawing.Point(155, 445);
            this.loginbt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginbt.Name = "loginbt";
            this.loginbt.Size = new System.Drawing.Size(77, 35);
            this.loginbt.TabIndex = 2;
            this.loginbt.UseVisualStyleBackColor = false;
            this.loginbt.Click += new System.EventHandler(this.button1_Click);
            this.loginbt.Enter += new System.EventHandler(this.loginbt_Enter);
            // 
            // registerbt
            // 
            this.registerbt.FlatAppearance.BorderSize = 0;
            this.registerbt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.registerbt.ForeColor = System.Drawing.Color.SkyBlue;
            this.registerbt.Location = new System.Drawing.Point(172, 482);
            this.registerbt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.registerbt.Name = "registerbt";
            this.registerbt.Size = new System.Drawing.Size(43, 28);
            this.registerbt.TabIndex = 4;
            this.registerbt.Text = "注册";
            this.registerbt.UseVisualStyleBackColor = true;
            this.registerbt.Click += new System.EventHandler(this.registerbt_Click);
            // 
            // repassbt
            // 
            this.repassbt.FlatAppearance.BorderSize = 0;
            this.repassbt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.repassbt.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.repassbt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.repassbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.repassbt.ForeColor = System.Drawing.Color.SkyBlue;
            this.repassbt.Location = new System.Drawing.Point(314, 520);
            this.repassbt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.repassbt.Name = "repassbt";
            this.repassbt.Size = new System.Drawing.Size(84, 26);
            this.repassbt.TabIndex = 4;
            this.repassbt.Text = "忘记?";
            this.repassbt.UseVisualStyleBackColor = true;
            // 
            // idtxt
            // 
            this.idtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.idtxt.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idtxt.Location = new System.Drawing.Point(106, 359);
            this.idtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.idtxt.MaxLength = 20;
            this.idtxt.Name = "idtxt";
            this.idtxt.Size = new System.Drawing.Size(175, 26);
            this.idtxt.TabIndex = 5;
            this.idtxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.idtxt.MouseEnter += new System.EventHandler(this.idtxt_MouseEnter);
            // 
            // pwtxt
            // 
            this.pwtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pwtxt.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pwtxt.Location = new System.Drawing.Point(106, 409);
            this.pwtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pwtxt.MaxLength = 20;
            this.pwtxt.Name = "pwtxt";
            this.pwtxt.PasswordChar = '■';
            this.pwtxt.Size = new System.Drawing.Size(175, 26);
            this.pwtxt.TabIndex = 5;
            this.pwtxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.pwtxt.MouseEnter += new System.EventHandler(this.pwtxt_MouseEnter);
            // 
            // minboxbt
            // 
            this.minboxbt.BackColor = System.Drawing.Color.Transparent;
            this.minboxbt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minboxbt.FlatAppearance.BorderSize = 0;
            this.minboxbt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minboxbt.Image = ((System.Drawing.Image)(resources.GetObject("minboxbt.Image")));
            this.minboxbt.Location = new System.Drawing.Point(312, 2);
            this.minboxbt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.minboxbt.Name = "minboxbt";
            this.minboxbt.Size = new System.Drawing.Size(40, 35);
            this.minboxbt.TabIndex = 2;
            this.minboxbt.UseVisualStyleBackColor = false;
            this.minboxbt.Click += new System.EventHandler(this.minboxbt_Click);
            this.minboxbt.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
            this.minboxbt.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            // 
            // closebt
            // 
            this.closebt.BackColor = System.Drawing.Color.Transparent;
            this.closebt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closebt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closebt.FlatAppearance.BorderSize = 0;
            this.closebt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebt.Image = ((System.Drawing.Image)(resources.GetObject("closebt.Image")));
            this.closebt.Location = new System.Drawing.Point(359, 2);
            this.closebt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.closebt.Name = "closebt";
            this.closebt.Size = new System.Drawing.Size(40, 35);
            this.closebt.TabIndex = 2;
            this.closebt.UseVisualStyleBackColor = false;
            this.closebt.Click += new System.EventHandler(this.closebt_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "三五二零聊天室";
            this.notifyIcon1.BalloonTipTitle = "请登录";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "三五二零";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.ForeColor = System.Drawing.Color.SkyBlue;
            this.checkBox1.Location = new System.Drawing.Point(303, 414);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            this.checkBox1.Enter += new System.EventHandler(this.checkBox1_Enter);
            // 
            // txtlb
            // 
            this.txtlb.AutoSize = true;
            this.txtlb.BackColor = System.Drawing.Color.Transparent;
            this.txtlb.Font = new System.Drawing.Font("等线 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtlb.Location = new System.Drawing.Point(-1, 2);
            this.txtlb.Name = "txtlb";
            this.txtlb.Size = new System.Drawing.Size(118, 14);
            this.txtlb.TabIndex = 7;
            this.txtlb.Text = "@Author三五二零";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // idtip
            // 
            this.idtip.IsBalloon = true;
            this.idtip.ShowAlways = true;
            this.idtip.ToolTipTitle = "ID";
            // 
            // Form1
            // 
            this.AcceptButton = this.loginbt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.closebt;
            this.ClientSize = new System.Drawing.Size(401, 551);
            this.Controls.Add(this.txtlb);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.registerbt);
            this.Controls.Add(this.loginbt);
            this.Controls.Add(this.repassbt);
            this.Controls.Add(this.idtxt);
            this.Controls.Add(this.pwtxt);
            this.Controls.Add(this.closebt);
            this.Controls.Add(this.minboxbt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(401, 551);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三五二零聊天室";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Button loginbt;
        private System.Windows.Forms.Button registerbt;
        private System.Windows.Forms.Button repassbt;
        private System.Windows.Forms.TextBox idtxt;
        private System.Windows.Forms.TextBox pwtxt;
        private System.Windows.Forms.Button minboxbt;
        private System.Windows.Forms.Button closebt;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label txtlb;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip idtip;
    }
}

