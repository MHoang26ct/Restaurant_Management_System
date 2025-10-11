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
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_dangnhap_Click(object sender, EventArgs e)
        {
            if (Program.KiemTraTaiKhoan(textBox_tendn.Text, textBox_matkhau.Text))
            {

                MessageDialog_DNThanhCong.Show();
                Form_Main form1 = new Form_Main();
                form1.Show();
                this.Hide();
            }
            else MessageDialog_DNThatBai.Show();
        }
    }
}
