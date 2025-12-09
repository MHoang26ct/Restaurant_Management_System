namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    partial class UC_AddTableCard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            AddTableButton = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2GradientPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.BackColor = Color.Transparent;
            guna2GradientPanel1.BorderColor = Color.DimGray;
            guna2GradientPanel1.BorderRadius = 20;
            guna2GradientPanel1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            guna2GradientPanel1.BorderThickness = 2;
            guna2GradientPanel1.Controls.Add(AddTableButton);
            guna2GradientPanel1.CustomBorderColor = Color.White;
            guna2GradientPanel1.CustomizableEdges = customizableEdges3;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.FillColor = SystemColors.Control;
            guna2GradientPanel1.FillColor2 = SystemColors.Control;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2GradientPanel1.Size = new Size(200, 230);
            guna2GradientPanel1.TabIndex = 0;
            // 
            // AddTableButton
            // 
            AddTableButton.BorderRadius = 20;
            AddTableButton.CustomizableEdges = customizableEdges1;
            AddTableButton.DisabledState.BorderColor = Color.DarkGray;
            AddTableButton.DisabledState.CustomBorderColor = Color.DarkGray;
            AddTableButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddTableButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            AddTableButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddTableButton.Dock = DockStyle.Fill;
            AddTableButton.FillColor = Color.Transparent;
            AddTableButton.FillColor2 = Color.Transparent;
            AddTableButton.FocusedColor = Color.Transparent;
            AddTableButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddTableButton.ForeColor = Color.DarkGray;
            AddTableButton.HoverState.BorderColor = Color.Transparent;
            AddTableButton.HoverState.CustomBorderColor = Color.Transparent;
            AddTableButton.HoverState.FillColor = Color.Transparent;
            AddTableButton.HoverState.FillColor2 = Color.Transparent;
            AddTableButton.Image = Properties.Resources.add__1_;
            AddTableButton.Location = new Point(0, 0);
            AddTableButton.Name = "AddTableButton";
            AddTableButton.PressedColor = Color.White;
            AddTableButton.PressedDepth = 0;
            AddTableButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            AddTableButton.Size = new Size(200, 230);
            AddTableButton.TabIndex = 0;
            AddTableButton.Text = "Thêm Bàn ";
            AddTableButton.Click += AddTableButton_Click;
            // 
            // UC_AddTableCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2GradientPanel1);
            Name = "UC_AddTableCard";
            Size = new Size(200, 230);
            guna2GradientPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientButton AddTableButton;
    }
}
