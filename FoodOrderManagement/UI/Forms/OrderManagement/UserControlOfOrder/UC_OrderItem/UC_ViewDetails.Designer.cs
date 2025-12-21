namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    partial class UC_ViewDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            doubleBufferedtlp1 = new DoubleBufferedTLP();
            TotalMoney = new Label();
            TimeOrderLabel = new Label();
            doubleBufferedtlp2 = new DoubleBufferedTLP();
            OrderIDLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ClosedButton = new Guna.UI2.WinForms.Guna2CircleButton();
            NameCustomerLabel = new Label();
            TableIDLabel = new Label();
            dgvChiTiet = new DataGridView();
            guna2Panel1.SuspendLayout();
            doubleBufferedtlp1.SuspendLayout();
            doubleBufferedtlp2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderRadius = 15;
            guna2Panel1.Controls.Add(doubleBufferedtlp1);
            guna2Panel1.CustomizableEdges = customizableEdges2;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2Panel1.Size = new Size(532, 298);
            guna2Panel1.TabIndex = 0;
            // 
            // doubleBufferedtlp1
            // 
            doubleBufferedtlp1.ColumnCount = 3;
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            doubleBufferedtlp1.Controls.Add(TotalMoney, 1, 6);
            doubleBufferedtlp1.Controls.Add(TimeOrderLabel, 1, 4);
            doubleBufferedtlp1.Controls.Add(doubleBufferedtlp2, 1, 0);
            doubleBufferedtlp1.Controls.Add(NameCustomerLabel, 1, 2);
            doubleBufferedtlp1.Controls.Add(TableIDLabel, 1, 3);
            doubleBufferedtlp1.Controls.Add(dgvChiTiet, 1, 5);
            doubleBufferedtlp1.Dock = DockStyle.Fill;
            doubleBufferedtlp1.Location = new Point(0, 0);
            doubleBufferedtlp1.Name = "doubleBufferedtlp1";
            doubleBufferedtlp1.RowCount = 8;
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
            doubleBufferedtlp1.Size = new Size(532, 298);
            doubleBufferedtlp1.TabIndex = 0;
            // 
            // TotalMoney
            // 
            TotalMoney.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TotalMoney.AutoSize = true;
            TotalMoney.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TotalMoney.ForeColor = Color.Black;
            TotalMoney.Location = new Point(29, 251);
            TotalMoney.Name = "TotalMoney";
            TotalMoney.Size = new Size(56, 21);
            TotalMoney.TabIndex = 30;
            TotalMoney.Text = "0VND ";
            // 
            // TimeOrderLabel
            // 
            TimeOrderLabel.AutoSize = true;
            TimeOrderLabel.Dock = DockStyle.Fill;
            TimeOrderLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TimeOrderLabel.ForeColor = Color.Gray;
            TimeOrderLabel.Location = new Point(28, 95);
            TimeOrderLabel.Margin = new Padding(2, 0, 0, 0);
            TimeOrderLabel.Name = "TimeOrderLabel";
            TimeOrderLabel.Size = new Size(487, 23);
            TimeOrderLabel.TabIndex = 17;
            TimeOrderLabel.Text = "Thời gian : 16/10/2025 14:35";
            TimeOrderLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // doubleBufferedtlp2
            // 
            doubleBufferedtlp2.ColumnCount = 2;
            doubleBufferedtlp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            doubleBufferedtlp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            doubleBufferedtlp2.Controls.Add(OrderIDLabel, 0, 0);
            doubleBufferedtlp2.Controls.Add(ClosedButton, 1, 0);
            doubleBufferedtlp2.Dock = DockStyle.Fill;
            doubleBufferedtlp2.Location = new Point(29, 3);
            doubleBufferedtlp2.Name = "doubleBufferedtlp2";
            doubleBufferedtlp2.RowCount = 1;
            doubleBufferedtlp2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            doubleBufferedtlp2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            doubleBufferedtlp2.Size = new Size(483, 38);
            doubleBufferedtlp2.TabIndex = 0;
            // 
            // OrderIDLabel
            // 
            OrderIDLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OrderIDLabel.BackColor = Color.Transparent;
            OrderIDLabel.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            OrderIDLabel.ForeColor = Color.Black;
            OrderIDLabel.Location = new Point(3, 8);
            OrderIDLabel.Name = "OrderIDLabel";
            OrderIDLabel.Size = new Size(151, 27);
            OrderIDLabel.TabIndex = 0;
            OrderIDLabel.Text = "Đơn Hàng #3636";
            // 
            // ClosedButton
            // 
            ClosedButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ClosedButton.DisabledState.BorderColor = Color.DarkGray;
            ClosedButton.DisabledState.CustomBorderColor = Color.DarkGray;
            ClosedButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ClosedButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ClosedButton.FillColor = Color.Transparent;
            ClosedButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClosedButton.ForeColor = Color.Black;
            ClosedButton.Location = new Point(450, 3);
            ClosedButton.Margin = new Padding(3, 3, 0, 0);
            ClosedButton.Name = "ClosedButton";
            ClosedButton.PressedColor = Color.White;
            ClosedButton.ShadowDecoration.CustomizableEdges = customizableEdges1;
            ClosedButton.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            ClosedButton.Size = new Size(33, 22);
            ClosedButton.TabIndex = 1;
            ClosedButton.Text = "X";
            ClosedButton.Click += ClosedButton_Click;
            // 
            // NameCustomerLabel
            // 
            NameCustomerLabel.AutoSize = true;
            NameCustomerLabel.Dock = DockStyle.Fill;
            NameCustomerLabel.Font = new Font("Segoe UI", 12F);
            NameCustomerLabel.ForeColor = Color.Gray;
            NameCustomerLabel.ImageAlign = ContentAlignment.MiddleRight;
            NameCustomerLabel.Location = new Point(28, 49);
            NameCustomerLabel.Margin = new Padding(2, 0, 0, 0);
            NameCustomerLabel.Name = "NameCustomerLabel";
            NameCustomerLabel.Size = new Size(487, 23);
            NameCustomerLabel.TabIndex = 6;
            NameCustomerLabel.Text = "Khách hàng: Nguyễn Hoàn Hải";
            NameCustomerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TableIDLabel
            // 
            TableIDLabel.AutoSize = true;
            TableIDLabel.Dock = DockStyle.Fill;
            TableIDLabel.Font = new Font("Segoe UI", 12F);
            TableIDLabel.ForeColor = Color.Gray;
            TableIDLabel.Location = new Point(28, 72);
            TableIDLabel.Margin = new Padding(2, 0, 0, 0);
            TableIDLabel.Name = "TableIDLabel";
            TableIDLabel.Size = new Size(487, 23);
            TableIDLabel.TabIndex = 7;
            TableIDLabel.Text = "Bàn 10";
            TableIDLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.BackgroundColor = SystemColors.ControlLightLight;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.Location = new Point(29, 121);
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.Size = new Size(483, 119);
            dgvChiTiet.TabIndex = 31;
            // 
            // UC_ViewDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(guna2Panel1);
            DoubleBuffered = true;
            Name = "UC_ViewDetails";
            Size = new Size(532, 298);
            guna2Panel1.ResumeLayout(false);
            doubleBufferedtlp1.ResumeLayout(false);
            doubleBufferedtlp1.PerformLayout();
            doubleBufferedtlp2.ResumeLayout(false);
            doubleBufferedtlp2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private DoubleBufferedTLP doubleBufferedtlp1;
        private Label TotalMoney;
        private Label TimeOrderLabel;
        private DoubleBufferedTLP doubleBufferedtlp2;
        private Guna.UI2.WinForms.Guna2HtmlLabel OrderIDLabel;
        private Guna.UI2.WinForms.Guna2CircleButton ClosedButton;
        private Label NameCustomerLabel;
        private Label TableIDLabel;
        private DataGridView dgvChiTiet;
    }
}
