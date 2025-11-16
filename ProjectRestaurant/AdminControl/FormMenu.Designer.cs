namespace ProjectRestaurant.AdminControl
{
    partial class FormMenu
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            BackgroundIcon = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            MenuIcon1 = new Guna.UI2.WinForms.Guna2PictureBox();
            TextLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            AddFoodButton = new Guna.UI2.WinForms.Guna2Button();
            SearchFoodTBox = new Guna.UI2.WinForms.Guna2TextBox();
            FoodCategories = new Guna.UI2.WinForms.Guna2ComboBox();
            BackgroundIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MenuIcon1).BeginInit();
            SuspendLayout();
            // 
            // BackgroundIcon
            // 
            BackgroundIcon.BackColor = Color.Transparent;
            BackgroundIcon.BorderColor = Color.White;
            BackgroundIcon.BorderRadius = 30;
            BackgroundIcon.Controls.Add(MenuIcon1);
            BackgroundIcon.CustomizableEdges = customizableEdges3;
            BackgroundIcon.FillColor = Color.FromArgb(226, 188, 90);
            BackgroundIcon.FillColor2 = Color.FromArgb(255, 128, 0);
            BackgroundIcon.FillColor4 = Color.FromArgb(226, 188, 90);
            BackgroundIcon.ForeColor = Color.Transparent;
            BackgroundIcon.Location = new Point(37, 39);
            BackgroundIcon.Name = "BackgroundIcon";
            BackgroundIcon.ShadowDecoration.CustomizableEdges = customizableEdges4;
            BackgroundIcon.Size = new Size(106, 106);
            BackgroundIcon.TabIndex = 5;
            BackgroundIcon.Paint += BackgroundIcon_Paint;
            // 
            // MenuIcon1
            // 
            MenuIcon1.BackColor = Color.Transparent;
            MenuIcon1.CustomizableEdges = customizableEdges1;
            MenuIcon1.FillColor = Color.Transparent;
            MenuIcon1.Image = Properties.Resources.Menuwhite;
            MenuIcon1.ImageRotate = 0F;
            MenuIcon1.Location = new Point(23, 25);
            MenuIcon1.Name = "MenuIcon1";
            MenuIcon1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            MenuIcon1.Size = new Size(65, 65);
            MenuIcon1.SizeMode = PictureBoxSizeMode.StretchImage;
            MenuIcon1.TabIndex = 19;
            MenuIcon1.TabStop = false;
            MenuIcon1.Click += MenuIcon1_Click;
            // 
            // TextLabel1
            // 
            TextLabel1.BackColor = Color.Transparent;
            TextLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextLabel1.ForeColor = Color.Black;
            TextLabel1.Location = new Point(166, 64);
            TextLabel1.Name = "TextLabel1";
            TextLabel1.Size = new Size(322, 56);
            TextLabel1.TabIndex = 6;
            TextLabel1.Text = "Quản Lí Thực Đơn";
            // 
            // AddFoodButton
            // 
            AddFoodButton.BackColor = Color.Transparent;
            AddFoodButton.BorderColor = Color.Transparent;
            AddFoodButton.BorderRadius = 30;
            AddFoodButton.CustomizableEdges = customizableEdges5;
            AddFoodButton.DisabledState.BorderColor = Color.White;
            AddFoodButton.DisabledState.CustomBorderColor = Color.White;
            AddFoodButton.DisabledState.FillColor = Color.White;
            AddFoodButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddFoodButton.FillColor = Color.FromArgb(226, 160, 90);
            AddFoodButton.FocusedColor = Color.Transparent;
            AddFoodButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddFoodButton.ForeColor = Color.White;
            AddFoodButton.Image = Properties.Resources.Plus;
            AddFoodButton.ImageAlign = HorizontalAlignment.Left;
            AddFoodButton.ImageSize = new Size(40, 40);
            AddFoodButton.Location = new Point(2549, 50);
            AddFoodButton.Name = "AddFoodButton";
            AddFoodButton.PressedColor = Color.White;
            AddFoodButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            AddFoodButton.Size = new Size(349, 106);
            AddFoodButton.TabIndex = 10;
            AddFoodButton.Text = "   Thêm Món Ăn";
            AddFoodButton.TextFormatNoPrefix = true;
            // 
            // SearchFoodTBox
            // 
            SearchFoodTBox.AcceptsTab = true;
            SearchFoodTBox.BorderRadius = 20;
            SearchFoodTBox.BorderThickness = 3;
            SearchFoodTBox.CustomizableEdges = customizableEdges7;
            SearchFoodTBox.DefaultText = "";
            SearchFoodTBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            SearchFoodTBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            SearchFoodTBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            SearchFoodTBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            SearchFoodTBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            SearchFoodTBox.Font = new Font("Segoe UI", 14.1F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SearchFoodTBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            SearchFoodTBox.Location = new Point(37, 212);
            SearchFoodTBox.Margin = new Padding(11, 12, 11, 12);
            SearchFoodTBox.Name = "SearchFoodTBox";
            SearchFoodTBox.PlaceholderForeColor = Color.Gray;
            SearchFoodTBox.PlaceholderText = "Tìm kiếm món ăn...";
            SearchFoodTBox.SelectedText = "";
            SearchFoodTBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
            SearchFoodTBox.Size = new Size(2174, 94);
            SearchFoodTBox.TabIndex = 18;
            // 
            // FoodCategories
            // 
            FoodCategories.BackColor = Color.Transparent;
            FoodCategories.BorderRadius = 30;
            FoodCategories.BorderThickness = 0;
            FoodCategories.CustomizableEdges = customizableEdges9;
            FoodCategories.DrawMode = DrawMode.OwnerDrawFixed;
            FoodCategories.DropDownHeight = 300;
            FoodCategories.DropDownStyle = ComboBoxStyle.DropDownList;
            FoodCategories.DropDownWidth = 300;
            FoodCategories.FocusedColor = Color.FromArgb(94, 148, 255);
            FoodCategories.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            FoodCategories.Font = new Font("Segoe UI", 14.1F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FoodCategories.ForeColor = Color.FromArgb(68, 88, 112);
            FoodCategories.IntegralHeight = false;
            FoodCategories.ItemHeight = 79;
            FoodCategories.ItemsAppearance.Font = new Font("Microsoft Sans Serif", 14.1F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FoodCategories.Location = new Point(2276, 212);
            FoodCategories.Name = "FoodCategories";
            FoodCategories.ShadowDecoration.CustomizableEdges = customizableEdges10;
            FoodCategories.Size = new Size(430, 85);
            FoodCategories.TabIndex = 20;
            FoodCategories.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(2968, 1912);
            Controls.Add(FoodCategories);
            Controls.Add(SearchFoodTBox);
            Controls.Add(AddFoodButton);
            Controls.Add(TextLabel1);
            Controls.Add(BackgroundIcon);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += FormMenu_Load;
            BackgroundIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MenuIcon1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel BackgroundIcon;
        private Guna.UI2.WinForms.Guna2PictureBox MenuIcon1;
        private Guna.UI2.WinForms.Guna2HtmlLabel TextLabel1;
        private Guna.UI2.WinForms.Guna2Button AddFoodButton;
        private Guna.UI2.WinForms.Guna2TextBox SearchFoodTBox;
        private Guna.UI2.WinForms.Guna2ComboBox FoodCategories;
    }
}