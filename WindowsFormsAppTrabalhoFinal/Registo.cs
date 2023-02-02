using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTrabalhoFinal
{
    public partial class Registo : Form
    {
        public Registo()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-065R760\\SQLEXPRESS;Initial Catalog=ProjetoLP2;Persist Security Info=True;User ID=ipca;Password=123456");

        private void btn_registar_Click(object sender, EventArgs e)
        {
            String email, password;
            email = txt_email.Text;
            password = txt_password.Text;

            if (email != "" && password != "")
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO LoginNew1 VALUES (@email, @password)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                cmd.Clone();

                this.Hide();
                Login login = new Login();
                login.Show();
            }
            else
            {
                MessageBox.Show("Você introduziu credênciais não válidas!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
