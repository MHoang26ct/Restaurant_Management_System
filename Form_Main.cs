using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectIT008
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new Form_Home());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddControls(new Form_DanhMuc());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddControls(new Form_SanPham());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddControls(new Form_DatBan());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void AddControls(Form f)
        {
            Panel_Main_Center.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            Panel_Main_Center.Controls.Add(f); 
            f.Show();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            label2.Text = "Xin chào "+Program.GetUsername();
        }

        private void Panel_Main_Center_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
