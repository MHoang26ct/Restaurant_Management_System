using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectIT008
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Login());
        }
        public static string con_string = @"Data Source=DESKTOP-RPVO6P0;Initial Catalog=Quanlybanhang;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection con = new SqlConnection(con_string);
        public static string USERNAME = "";
        public static string GetUsername() { return USERNAME;}
        public static bool KiemTraTaiKhoan(string username, string password)
        {
            con.Open();
            bool KetQua = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = @"select count(*) from users where username=@u and password=@p";
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                KetQua = true;
                USERNAME += username;
            }
            con.Close();
            return KetQua;
        }
    }
}
