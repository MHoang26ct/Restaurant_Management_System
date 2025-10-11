namespace ProjectIT008
{
    partial class Form_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_tendn = new System.Windows.Forms.Label();
            this.textBox_tendn = new System.Windows.Forms.TextBox();
            this.label_matkhau = new System.Windows.Forms.Label();
            this.textBox_matkhau = new System.Windows.Forms.TextBox();
            this.Button_dangnhap = new Guna.UI2.WinForms.Guna2Button();
            this.Button_thoat = new Guna.UI2.WinForms.Guna2Button();
            this.panel_top_login = new System.Windows.Forms.Panel();
            this.label_vuilongnhap = new System.Windows.Forms.Label();
            this.PictureBox_login_user = new Guna.UI2.WinForms.Guna2PictureBox();
            this.MessageDialog_DNThanhCong = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.MessageDialog_DNThatBai = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.panel_top_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_login_user)).BeginInit();
            this.SuspendLayout();
            // 
            // label_tendn
            // 
            this.label_tendn.AutoSize = true;
            this.label_tendn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tendn.Location = new System.Drawing.Point(71, 348);
            this.label_tendn.Name = "label_tendn";
            this.label_tendn.Size = new System.Drawing.Size(128, 23);
            this.label_tendn.TabIndex = 1;
            this.label_tendn.Text = "Tên đăng nhập";
            this.label_tendn.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_tendn
            // 
            this.textBox_tendn.Location = new System.Drawing.Point(75, 392);
            this.textBox_tendn.Name = "textBox_tendn";
            this.textBox_tendn.Size = new System.Drawing.Size(169, 30);
            this.textBox_tendn.TabIndex = 2;
            this.textBox_tendn.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_matkhau
            // 
            this.label_matkhau.AutoSize = true;
            this.label_matkhau.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_matkhau.Location = new System.Drawing.Point(71, 493);
            this.label_matkhau.Name = "label_matkhau";
            this.label_matkhau.Size = new System.Drawing.Size(86, 23);
            this.label_matkhau.TabIndex = 1;
            this.label_matkhau.Text = "Mật khẩu";
            this.label_matkhau.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_matkhau
            // 
            this.textBox_matkhau.Location = new System.Drawing.Point(75, 537);
            this.textBox_matkhau.Name = "textBox_matkhau";
            this.textBox_matkhau.Size = new System.Drawing.Size(169, 30);
            this.textBox_matkhau.TabIndex = 2;
            this.textBox_matkhau.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Button_dangnhap
            // 
            this.Button_dangnhap.AutoRoundedCorners = true;
            this.Button_dangnhap.BackColor = System.Drawing.Color.Transparent;
            this.Button_dangnhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_dangnhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_dangnhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_dangnhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_dangnhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Button_dangnhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button_dangnhap.ForeColor = System.Drawing.Color.White;
            this.Button_dangnhap.Location = new System.Drawing.Point(59, 606);
            this.Button_dangnhap.Name = "Button_dangnhap";
            this.Button_dangnhap.Size = new System.Drawing.Size(120, 55);
            this.Button_dangnhap.TabIndex = 3;
            this.Button_dangnhap.Text = "Đăng nhập";
            this.Button_dangnhap.Click += new System.EventHandler(this.Button_dangnhap_Click);
            // 
            // Button_thoat
            // 
            this.Button_thoat.AutoRoundedCorners = true;
            this.Button_thoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_thoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_thoat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_thoat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_thoat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Button_thoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Button_thoat.ForeColor = System.Drawing.Color.White;
            this.Button_thoat.Location = new System.Drawing.Point(235, 606);
            this.Button_thoat.Name = "Button_thoat";
            this.Button_thoat.Size = new System.Drawing.Size(120, 55);
            this.Button_thoat.TabIndex = 3;
            this.Button_thoat.Text = "Thoát";
            this.Button_thoat.Click += new System.EventHandler(this.Button_thoat_Click);
            // 
            // panel_top_login
            // 
            this.panel_top_login.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel_top_login.Controls.Add(this.label_vuilongnhap);
            this.panel_top_login.Controls.Add(this.PictureBox_login_user);
            this.panel_top_login.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top_login.Location = new System.Drawing.Point(0, 0);
            this.panel_top_login.Name = "panel_top_login";
            this.panel_top_login.Size = new System.Drawing.Size(414, 290);
            this.panel_top_login.TabIndex = 0;
            this.panel_top_login.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label_vuilongnhap
            // 
            this.label_vuilongnhap.AutoSize = true;
            this.label_vuilongnhap.Location = new System.Drawing.Point(3, 267);
            this.label_vuilongnhap.Name = "label_vuilongnhap";
            this.label_vuilongnhap.Size = new System.Drawing.Size(282, 23);
            this.label_vuilongnhap.TabIndex = 1;
            this.label_vuilongnhap.Text = "Vui lòng nhập thông tin đăng nhập";
            // 
            // PictureBox_login_user
            // 
            this.PictureBox_login_user.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_login_user.Image = global::ProjectIT008.Properties.Resources.user;
            this.PictureBox_login_user.ImageRotate = 0F;
            this.PictureBox_login_user.Location = new System.Drawing.Point(104, 92);
            this.PictureBox_login_user.Name = "PictureBox_login_user";
            this.PictureBox_login_user.Size = new System.Drawing.Size(204, 154);
            this.PictureBox_login_user.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox_login_user.TabIndex = 0;
            this.PictureBox_login_user.TabStop = false;
            this.PictureBox_login_user.UseTransparentBackground = true;
            // 
            // MessageDialog_DNThanhCong
            // 
            this.MessageDialog_DNThanhCong.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.MessageDialog_DNThanhCong.Caption = "Thông báo";
            this.MessageDialog_DNThanhCong.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            this.MessageDialog_DNThanhCong.Parent = this;
            this.MessageDialog_DNThanhCong.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.MessageDialog_DNThanhCong.Text = "Đăng nhập thành công";
            // 
            // MessageDialog_DNThatBai
            // 
            this.MessageDialog_DNThatBai.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.MessageDialog_DNThatBai.Caption = "Thông báo";
            this.MessageDialog_DNThatBai.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
            this.MessageDialog_DNThatBai.Parent = this;
            this.MessageDialog_DNThatBai.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.MessageDialog_DNThatBai.Text = "Đăng nhập thất bại, vui lòng kiểm tra lại thông tin";
            // 
            // Form_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 696);
            this.Controls.Add(this.Button_thoat);
            this.Controls.Add(this.Button_dangnhap);
            this.Controls.Add(this.textBox_matkhau);
            this.Controls.Add(this.label_matkhau);
            this.Controls.Add(this.textBox_tendn);
            this.Controls.Add(this.label_tendn);
            this.Controls.Add(this.panel_top_login);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel_top_login.ResumeLayout(false);
            this.panel_top_login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_login_user)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_tendn;
        private System.Windows.Forms.TextBox textBox_tendn;
        private System.Windows.Forms.Label label_matkhau;
        private System.Windows.Forms.TextBox textBox_matkhau;
        private Guna.UI2.WinForms.Guna2Button Button_dangnhap;
        private Guna.UI2.WinForms.Guna2Button Button_thoat;
        private System.Windows.Forms.Panel panel_top_login;
        private Guna.UI2.WinForms.Guna2PictureBox PictureBox_login_user;
        private System.Windows.Forms.Label label_vuilongnhap;
        private Guna.UI2.WinForms.Guna2MessageDialog MessageDialog_DNThanhCong;
        private Guna.UI2.WinForms.Guna2MessageDialog MessageDialog_DNThatBai;
    }
}

