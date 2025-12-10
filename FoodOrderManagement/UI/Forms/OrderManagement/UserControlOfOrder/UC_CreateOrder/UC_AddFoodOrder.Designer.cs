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
            QuantityFoodNBox = new Guna.UI2.WinForms.Guna2NumericUpDown();
            NameFoodCBox = new Guna.UI2.WinForms.Guna2ComboBox();
            DeleteButton = new Guna.UI2.WinForms.Guna2Button();
            doubleBufferedtlp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuantityFoodNBox).BeginInit();
            SuspendLayout();
            // 
            // doubleBufferedtlp1
            // 
            doubleBufferedtlp1.ColumnCount = 3;
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            doubleBufferedtlp1.Controls.Add(QuantityFoodNBox, 1, 0);
            doubleBufferedtlp1.Controls.Add(NameFoodCBox, 0, 0);
            doubleBufferedtlp1.Controls.Add(DeleteButton, 2, 0);
            doubleBufferedtlp1.Dock = DockStyle.Fill;
            doubleBufferedtlp1.Location = new Point(0, 0);
            doubleBufferedtlp1.Name = "doubleBufferedtlp1";
            doubleBufferedtlp1.RowCount = 1;
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            doubleBufferedtlp1.Size = new Size(521, 45);
            doubleBufferedtlp1.TabIndex = 0;
            // 
            // QuantityFoodNBox
            // 
            QuantityFoodNBox.BackColor = Color.Transparent;
            QuantityFoodNBox.BorderColor = Color.White;
            QuantityFoodNBox.BorderRadius = 8;
            QuantityFoodNBox.BorderThickness = 0;
            QuantityFoodNBox.CustomizableEdges = customizableEdges1;
            QuantityFoodNBox.Dock = DockStyle.Fill;
            QuantityFoodNBox.FillColor = Color.Gainsboro;
            QuantityFoodNBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            QuantityFoodNBox.Location = new Point(345, 5);
            QuantityFoodNBox.Margin = new Padding(7, 5, 2, 9);
            QuantityFoodNBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuantityFoodNBox.Name = "QuantityFoodNBox";
            QuantityFoodNBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            QuantityFoodNBox.Size = new Size(121, 31);
            QuantityFoodNBox.TabIndex = 19;
            QuantityFoodNBox.UpDownButtonFillColor = Color.Gray;
            QuantityFoodNBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NameFoodCBox
            // 
            NameFoodCBox.BackColor = Color.Transparent;
            NameFoodCBox.CustomizableEdges = customizableEdges3;
            NameFoodCBox.Dock = DockStyle.Fill;
            NameFoodCBox.DrawMode = DrawMode.OwnerDrawFixed;
            NameFoodCBox.DropDownStyle = ComboBoxStyle.DropDownList;
            NameFoodCBox.FocusedColor = Color.FromArgb(94, 148, 255);
            NameFoodCBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            NameFoodCBox.Font = new Font("Segoe UI", 10F);
            NameFoodCBox.ForeColor = Color.Black;
            NameFoodCBox.ItemHeight = 30;
            NameFoodCBox.Location = new Point(3, 6);
            NameFoodCBox.Margin = new Padding(3, 6, 3, 0);
            NameFoodCBox.Name = "NameFoodCBox";
            NameFoodCBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            NameFoodCBox.Size = new Size(332, 36);
            NameFoodCBox.TabIndex = 0;
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
            DeleteButton.Click += this.DeleteButton_Click;
            // 
            // UC_AddFoodOrder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(doubleBufferedtlp1);
            Name = "UC_AddFoodOrder";
            Size = new Size(521, 45);
            doubleBufferedtlp1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)QuantityFoodNBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedTLP doubleBufferedtlp1;
        private Guna.UI2.WinForms.Guna2ComboBox NameFoodCBox;
        private Guna.UI2.WinForms.Guna2NumericUpDown QuantityFoodNBox;
        private Guna.UI2.WinForms.Guna2Button DeleteButton;
    }
}
