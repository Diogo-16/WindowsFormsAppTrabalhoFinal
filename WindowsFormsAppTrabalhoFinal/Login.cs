using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace WindowsFormsAppTrabalhoFinal
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-065R760\\SQLEXPRESS;Initial Catalog=ProjetoLP2;Persist Security Info=True;User ID=ipca;Password=123456");

        private void button1_Click(object sender, EventArgs e)
        {

            String email, password;
            email = txt_email.Text;
            password = txt_password.Text;

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM LoginNew1 WHERE email = @email AND password = @password", con);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    Produto formProduto = new Produto();
                    formProduto.Show();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Você introduziu credênciais não válidas!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registo registo = new Registo();
            registo.Show();
        }
    }
}
