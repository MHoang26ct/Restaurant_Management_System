namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    partial class UC_AddFoodOrder
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_AddFoodOrder));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            doubleBufferedtlp1 = new DoubleBufferedTLP();
            QuanlityFoodNBox = new Guna.UI2.WinForms.Guna2NumericUpDown();
            CategoriesFoodCBox = new Guna.UI2.WinForms.Guna2ComboBox();
            DeleteButton = new Guna.UI2.WinForms.Guna2Button();
            doubleBufferedtlp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuanlityFoodNBox).BeginInit();
            SuspendLayout();
            // 
            // doubleBufferedtlp1
            // 
            doubleBufferedtlp1.ColumnCount = 3;
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            doubleBufferedtlp1.Controls.Add(QuanlityFoodNBox, 1, 0);
            doubleBufferedtlp1.Controls.Add(CategoriesFoodCBox, 0, 0);
            doubleBufferedtlp1.Controls.Add(DeleteButton, 2, 0);
            doubleBufferedtlp1.Dock = DockStyle.Fill;
            doubleBufferedtlp1.Location = new Point(0, 0);
            doubleBufferedtlp1.Name = "doubleBufferedtlp1";
            doubleBufferedtlp1.RowCount = 1;
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            doubleBufferedtlp1.Size = new Size(521, 45);
            doubleBufferedtlp1.TabIndex = 0;
            // 
            // QuanlityFoodNBox
            // 
            QuanlityFoodNBox.BackColor = Color.Transparent;
            QuanlityFoodNBox.BorderColor = Color.White;
            QuanlityFoodNBox.BorderRadius = 8;
            QuanlityFoodNBox.BorderThickness = 0;
            QuanlityFoodNBox.CustomizableEdges = customizableEdges1;
            QuanlityFoodNBox.Dock = DockStyle.Fill;
            QuanlityFoodNBox.FillColor = Color.Gainsboro;
            QuanlityFoodNBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            QuanlityFoodNBox.Location = new Point(345, 5);
            QuanlityFoodNBox.Margin = new Padding(7, 5, 2, 9);
            QuanlityFoodNBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuanlityFoodNBox.Name = "QuanlityFoodNBox";
            QuanlityFoodNBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            QuanlityFoodNBox.Size = new Size(121, 31);
            QuanlityFoodNBox.TabIndex = 19;
            QuanlityFoodNBox.UpDownButtonFillColor = Color.Gray;
            QuanlityFoodNBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CategoriesFoodCBox
            // 
            CategoriesFoodCBox.BackColor = Color.Transparent;
            CategoriesFoodCBox.CustomizableEdges = customizableEdges3;
            CategoriesFoodCBox.Dock = DockStyle.Fill;
            CategoriesFoodCBox.DrawMode = DrawMode.OwnerDrawFixed;
            CategoriesFoodCBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CategoriesFoodCBox.FocusedColor = Color.FromArgb(94, 148, 255);
            CategoriesFoodCBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            CategoriesFoodCBox.Font = new Font("Segoe UI", 10F);
            CategoriesFoodCBox.ForeColor = Color.Black;
            CategoriesFoodCBox.ItemHeight = 30;
            CategoriesFoodCBox.Location = new Point(3, 6);
            CategoriesFoodCBox.Margin = new Padding(3, 6, 3, 0);
            CategoriesFoodCBox.Name = "CategoriesFoodCBox";
            CategoriesFoodCBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            CategoriesFoodCBox.Size = new Size(332, 36);
            CategoriesFoodCBox.TabIndex = 0;
            // 
            // DeleteButton
            // 
            DeleteButton.CustomizableEdges = customizableEdges5;
            DeleteButton.DisabledState.BorderColor = Color.DarkGray;
            DeleteButton.DisabledState.CustomBorderColor = Color.DarkGray;
            DeleteButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            DeleteButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            DeleteButton.Dock = DockStyle.Fill;
            DeleteButton.FillColor = Color.Transparent;
            DeleteButton.Font = new Font("Segoe UI", 9F);
            DeleteButton.ForeColor = Color.White;
            DeleteButton.Image = (Image)resources.GetObject("DeleteButton.Image");
            DeleteButton.Location = new Point(471, 0);
            DeleteButton.Margin = new Padding(3, 0, 3, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            DeleteButton.Size = new Size(47, 42);
            DeleteButton.TabIndex = 20;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // UC_AddFoodOrder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(doubleBufferedtlp1);
            Name = "UC_AddFoodOrder";
            Size = new Size(521, 45);
            doubleBufferedtlp1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)QuanlityFoodNBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedTLP doubleBufferedtlp1;
        private Guna.UI2.WinForms.Guna2ComboBox CategoriesFoodCBox;
        private Guna.UI2.WinForms.Guna2NumericUpDown QuanlityFoodNBox;
        private Guna.UI2.WinForms.Guna2Button DeleteButton;
    }
}
