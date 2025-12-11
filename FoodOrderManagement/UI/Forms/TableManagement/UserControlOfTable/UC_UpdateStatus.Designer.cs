namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    partial class UC_UpdateStatus
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
            Gradient = new Guna.UI2.WinForms.Guna2GradientPanel();
            doubleBufferedtlp1 = new DoubleBufferedTLP();
            Gradient.SuspendLayout();
            SuspendLayout();
            // 
            // Gradient
            // 
            Gradient.BorderRadius = 20;
            Gradient.Controls.Add(doubleBufferedtlp1);
            Gradient.CustomizableEdges = customizableEdges1;
            Gradient.Dock = DockStyle.Fill;
            Gradient.FillColor = Color.White;
            Gradient.FillColor2 = Color.White;
            Gradient.Location = new Point(0, 0);
            Gradient.Name = "Gradient";
            Gradient.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Gradient.Size = new Size(500, 500);
            Gradient.TabIndex = 0;
            // 
            // doubleBufferedtlp1
            // 
            doubleBufferedtlp1.ColumnCount = 1;
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            doubleBufferedtlp1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            doubleBufferedtlp1.Dock = DockStyle.Fill;
            doubleBufferedtlp1.Location = new Point(0, 0);
            doubleBufferedtlp1.Name = "doubleBufferedtlp1";
            doubleBufferedtlp1.RowCount = 7;
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            doubleBufferedtlp1.Size = new Size(500, 500);
            doubleBufferedtlp1.TabIndex = 0;
            // 
            // UC_UpdateStatus
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(Gradient);
            Name = "UC_UpdateStatus";
            Size = new Size(500, 500);
            Gradient.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel Gradient;
        private DoubleBufferedTLP doubleBufferedtlp1;
    }
}
