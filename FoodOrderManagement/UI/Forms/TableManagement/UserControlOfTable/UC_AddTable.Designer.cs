namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    partial class UC_AddTable
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Shadowpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            doubleBufferedtlp1 = new DoubleBufferedTLP();
            CapacityNBox = new Guna.UI2.WinForms.Guna2NumericUpDown();
            CapacityLabel = new Label();
            AddButton = new Guna.UI2.WinForms.Guna2GradientButton();
            ExitButton = new Guna.UI2.WinForms.Guna2Button();
            Shadowpanel.SuspendLayout();
            doubleBufferedtlp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CapacityNBox).BeginInit();
            SuspendLayout();
            // 
            // Shadowpanel
            // 
            Shadowpanel.BackColor = Color.Transparent;
            Shadowpanel.Controls.Add(doubleBufferedtlp1);
            Shadowpanel.Dock = DockStyle.Fill;
            Shadowpanel.FillColor = Color.White;
            Shadowpanel.Location = new Point(0, 0);
            Shadowpanel.Name = "Shadowpanel";
            Shadowpanel.Radius = 10;
            Shadowpanel.ShadowColor = Color.Black;
            Shadowpanel.ShadowDepth = 50;
            Shadowpanel.ShadowShift = 3;
            Shadowpanel.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.Dropped;
            Shadowpanel.Size = new Size(300, 120);
            Shadowpanel.TabIndex = 0;
            // 
            // doubleBufferedtlp1
            // 
            doubleBufferedtlp1.ColumnCount = 3;
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.333334F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.302325F));
            doubleBufferedtlp1.Controls.Add(CapacityNBox, 1, 0);
            doubleBufferedtlp1.Controls.Add(CapacityLabel, 0, 0);
            doubleBufferedtlp1.Controls.Add(AddButton, 0, 1);
            doubleBufferedtlp1.Controls.Add(ExitButton, 2, 0);
            doubleBufferedtlp1.Dock = DockStyle.Fill;
            doubleBufferedtlp1.Location = new Point(0, 0);
            doubleBufferedtlp1.Name = "doubleBufferedtlp1";
            doubleBufferedtlp1.RowCount = 3;
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            doubleBufferedtlp1.Size = new Size(300, 120);
            doubleBufferedtlp1.TabIndex = 0;
            // 
            // CapacityNBox
            // 
            CapacityNBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            CapacityNBox.BackColor = Color.Transparent;
            CapacityNBox.BorderColor = Color.White;
            CapacityNBox.BorderRadius = 8;
            CapacityNBox.BorderThickness = 0;
            CapacityNBox.CustomizableEdges = customizableEdges1;
            CapacityNBox.FillColor = Color.Gainsboro;
            CapacityNBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CapacityNBox.Location = new Point(101, 11);
            CapacityNBox.Margin = new Padding(7, 5, 2, 9);
            CapacityNBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CapacityNBox.Name = "CapacityNBox";
            CapacityNBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            CapacityNBox.Size = new Size(168, 36);
            CapacityNBox.TabIndex = 20;
            CapacityNBox.UpDownButtonFillColor = Color.Gray;
            CapacityNBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CapacityLabel
            // 
            CapacityLabel.Anchor = AnchorStyles.Left;
            CapacityLabel.AutoSize = true;
            CapacityLabel.Font = new Font("Segoe UI", 11.5F);
            CapacityLabel.ForeColor = Color.Black;
            CapacityLabel.Location = new Point(3, 18);
            CapacityLabel.Margin = new Padding(3, 0, 3, 5);
            CapacityLabel.Name = "CapacityLabel";
            CapacityLabel.Size = new Size(83, 21);
            CapacityLabel.TabIndex = 0;
            CapacityLabel.Text = "Sức chứa : ";
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            AddButton.BorderRadius = 20;
            AddButton.BorderThickness = 2;
            doubleBufferedtlp1.SetColumnSpan(AddButton, 3);
            AddButton.CustomizableEdges = customizableEdges3;
            AddButton.DisabledState.BorderColor = Color.DarkGray;
            AddButton.DisabledState.CustomBorderColor = Color.DarkGray;
            AddButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            AddButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddButton.FillColor = Color.White;
            AddButton.FillColor2 = Color.White;
            AddButton.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddButton.ForeColor = Color.Black;
            AddButton.Location = new Point(3, 65);
            AddButton.Name = "AddButton";
            AddButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            AddButton.Size = new Size(294, 42);
            AddButton.TabIndex = 22;
            AddButton.Text = "Thêm bàn";
            AddButton.Click += AddButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ExitButton.BorderRadius = 5;
            ExitButton.CustomizableEdges = customizableEdges5;
            ExitButton.DisabledState.BorderColor = Color.DarkGray;
            ExitButton.DisabledState.CustomBorderColor = Color.DarkGray;
            ExitButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ExitButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ExitButton.FillColor = Color.White;
            ExitButton.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExitButton.ForeColor = Color.Black;
            ExitButton.HoverState.BorderColor = Color.Transparent;
            ExitButton.HoverState.CustomBorderColor = Color.Transparent;
            ExitButton.HoverState.FillColor = Color.Transparent;
            ExitButton.Location = new Point(271, 0);
            ExitButton.Margin = new Padding(0);
            ExitButton.Name = "ExitButton";
            ExitButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            ExitButton.Size = new Size(29, 21);
            ExitButton.TabIndex = 21;
            ExitButton.Text = "X";
            ExitButton.TextAlign = HorizontalAlignment.Right;
            ExitButton.Click += ExitButton_Click;
            // 
            // UC_AddTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(Shadowpanel);
            Name = "UC_AddTable";
            Size = new Size(300, 120);
            Shadowpanel.ResumeLayout(false);
            doubleBufferedtlp1.ResumeLayout(false);
            doubleBufferedtlp1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CapacityNBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel Shadowpanel;
        private DoubleBufferedTLP doubleBufferedtlp1;
        private Label CapacityLabel;
        private Guna.UI2.WinForms.Guna2NumericUpDown CapacityNBox;
        private Guna.UI2.WinForms.Guna2Button ExitButton;
        private Guna.UI2.WinForms.Guna2GradientButton AddButton;
    }
}
