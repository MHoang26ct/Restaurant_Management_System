namespace FoodOrderManagement.UI.Forms.EmployeManagement.UserControlOfEmployee
{
    partial class UC_EmployeeItem
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            NameLabel = new Label();
            doubleBufferedtlp5 = new DoubleBufferedTLP();
            EditButton = new Guna.UI2.WinForms.Guna2Button();
            HireDateLabel = new Label();
            PositionLabel = new Label();
            EmailLabel = new Label();
            PhoneNumberLabel = new Label();
            DeleteButton = new Guna.UI2.WinForms.Guna2Button();
            doubleBufferedtlp5.SuspendLayout();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.Anchor = AnchorStyles.Left;
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLabel.ForeColor = Color.Black;
            NameLabel.Location = new Point(25, 14);
            NameLabel.Margin = new Padding(0);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(133, 21);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Nguyễn Hoàn Hải";
            // 
            // doubleBufferedtlp5
            // 
            doubleBufferedtlp5.ColumnCount = 9;
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.5F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5493288F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.3914757F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            doubleBufferedtlp5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.5F));
            doubleBufferedtlp5.Controls.Add(EditButton, 6, 0);
            doubleBufferedtlp5.Controls.Add(HireDateLabel, 5, 0);
            doubleBufferedtlp5.Controls.Add(PositionLabel, 4, 0);
            doubleBufferedtlp5.Controls.Add(EmailLabel, 3, 0);
            doubleBufferedtlp5.Controls.Add(PhoneNumberLabel, 2, 0);
            doubleBufferedtlp5.Controls.Add(NameLabel, 1, 0);
            doubleBufferedtlp5.Controls.Add(DeleteButton, 8, 0);
            doubleBufferedtlp5.Dock = DockStyle.Fill;
            doubleBufferedtlp5.Location = new Point(0, 0);
            doubleBufferedtlp5.Name = "doubleBufferedtlp5";
            doubleBufferedtlp5.RowCount = 1;
            doubleBufferedtlp5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            doubleBufferedtlp5.Size = new Size(1267, 50);
            doubleBufferedtlp5.TabIndex = 1;
            // 
            // EditButton
            // 
            EditButton.Anchor = AnchorStyles.Right;
            EditButton.BorderRadius = 5;
            EditButton.BorderThickness = 1;
            EditButton.CustomizableEdges = customizableEdges1;
            EditButton.DisabledState.BorderColor = Color.DarkGray;
            EditButton.DisabledState.CustomBorderColor = Color.DarkGray;
            EditButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            EditButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            EditButton.FillColor = Color.White;
            EditButton.Font = new Font("Segoe UI", 9F);
            EditButton.ForeColor = Color.Black;
            EditButton.HoverState.BorderColor = Color.FromArgb(192, 255, 192);
            EditButton.HoverState.CustomBorderColor = Color.FromArgb(192, 255, 192);
            EditButton.HoverState.FillColor = Color.FromArgb(192, 255, 192);
            EditButton.Image = Properties.Resources.EditBlack;
            EditButton.ImageSize = new Size(15, 15);
            EditButton.Location = new Point(1124, 11);
            EditButton.Margin = new Padding(0);
            EditButton.Name = "EditButton";
            EditButton.PressedColor = Color.Gray;
            EditButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            EditButton.Size = new Size(39, 27);
            EditButton.TabIndex = 14;
            EditButton.TabStop = false;
            EditButton.Click += EditButton_Click;
            // 
            // HireDateLabel
            // 
            HireDateLabel.Anchor = AnchorStyles.Left;
            HireDateLabel.AutoSize = true;
            HireDateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HireDateLabel.ForeColor = Color.Black;
            HireDateLabel.Location = new Point(921, 14);
            HireDateLabel.Name = "HireDateLabel";
            HireDateLabel.Size = new Size(85, 21);
            HireDateLabel.TabIndex = 12;
            HireDateLabel.Text = "15/1/2023";
            // 
            // PositionLabel
            // 
            PositionLabel.Anchor = AnchorStyles.Left;
            PositionLabel.AutoSize = true;
            PositionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PositionLabel.ForeColor = Color.Black;
            PositionLabel.Location = new Point(762, 14);
            PositionLabel.Name = "PositionLabel";
            PositionLabel.Size = new Size(64, 21);
            PositionLabel.TabIndex = 11;
            PositionLabel.Text = "Quản Lí";
            // 
            // EmailLabel
            // 
            EmailLabel.Anchor = AnchorStyles.Left;
            EmailLabel.AutoSize = true;
            EmailLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EmailLabel.ForeColor = Color.Black;
            EmailLabel.Location = new Point(414, 14);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(225, 21);
            EmailLabel.TabIndex = 10;
            EmailLabel.Text = "hoanhai24505646@gmail.com";
            // 
            // PhoneNumberLabel
            // 
            PhoneNumberLabel.Anchor = AnchorStyles.Left;
            PhoneNumberLabel.AutoSize = true;
            PhoneNumberLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PhoneNumberLabel.ForeColor = Color.Black;
            PhoneNumberLabel.Location = new Point(224, 14);
            PhoneNumberLabel.Name = "PhoneNumberLabel";
            PhoneNumberLabel.Size = new Size(100, 21);
            PhoneNumberLabel.TabIndex = 9;
            PhoneNumberLabel.Text = "0348850913";
            // 
            // DeleteButton
            // 
            DeleteButton.Anchor = AnchorStyles.Left;
            DeleteButton.BorderColor = Color.FromArgb(255, 128, 128);
            DeleteButton.BorderRadius = 5;
            DeleteButton.BorderThickness = 1;
            DeleteButton.CustomizableEdges = customizableEdges3;
            DeleteButton.DisabledState.BorderColor = Color.DarkGray;
            DeleteButton.DisabledState.CustomBorderColor = Color.DarkGray;
            DeleteButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            DeleteButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            DeleteButton.FillColor = Color.White;
            DeleteButton.Font = new Font("Segoe UI", 9F);
            DeleteButton.ForeColor = Color.Black;
            DeleteButton.HoverState.BorderColor = Color.FromArgb(255, 192, 192);
            DeleteButton.HoverState.CustomBorderColor = Color.FromArgb(255, 192, 192);
            DeleteButton.HoverState.FillColor = Color.FromArgb(255, 192, 192);
            DeleteButton.Image = Properties.Resources.BinRed;
            DeleteButton.ImageSize = new Size(15, 15);
            DeleteButton.Location = new Point(1169, 11);
            DeleteButton.Margin = new Padding(0);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.PressedColor = Color.FromArgb(255, 192, 192);
            DeleteButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            DeleteButton.Size = new Size(39, 27);
            DeleteButton.TabIndex = 15;
            DeleteButton.TabStop = false;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // UC_EmployeeItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(doubleBufferedtlp5);
            Margin = new Padding(0);
            Name = "UC_EmployeeItem";
            Size = new Size(1267, 50);
            Load += UC_EmployeeItem_Load;
            doubleBufferedtlp5.ResumeLayout(false);
            doubleBufferedtlp5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label NameLabel;
        private DoubleBufferedTLP doubleBufferedtlp5;
        private Label HireDateLabel;
        private Label PositionLabel;
        private Label EmailLabel;
        private Label PhoneNumberLabel;
        private Guna.UI2.WinForms.Guna2Button EditButton;
        private Guna.UI2.WinForms.Guna2Button DeleteButton;
    }
}
