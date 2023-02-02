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
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;

namespace WindowsFormsAppTrabalhoFinal
{
    public partial class Produto : Form
    {
        int ID = 0;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-065R760\\SQLEXPRESS;Initial Catalog=ProjetoLP2;Persist Security Info=True;User ID=ipca;Password=123456");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        //Inserir dados
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (txt_nome.Text != "" && txt_descricao.Text != "")
            {
                SqlCommand cmd;
                SqlDataAdapter adapt;
                cmd = new SqlCommand("insert into dados(NomeProduto,Descricao) values(@nome, @descricao)", con);
               
                con.Open();
                cmd.Parameters.AddWithValue("@nome", txt_nome.Text);
                cmd.Parameters.AddWithValue("@descricao", txt_descricao.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Registo inserido com sucesso!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Por favor, preencha os dados!");
            }
        }


        //Mostrar os dados na DataGridView
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from dados", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //Limpar os Dados
        private void ClearData()
        {
            txt_nome.Text = "";
            txt_descricao.Text = "";
            ID = 0;
        }
        //dataGridView1 RowHeaderMouseClick Event
        // Como fazer o evento:
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_nome.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_descricao.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        // Atualizar registos
        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txt_nome.Text != "" && txt_descricao.Text != "")
            {
                cmd = new SqlCommand("update dados set NomeProduto=@nome,Descricao=@descricao where ID = @id", con);
               
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@nome", txt_nome.Text);
                cmd.Parameters.AddWithValue("@descricao", txt_descricao.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registo autualizado com sucesso!");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Por favor, selecione o registo que pretende atualizar.");
            }
        }


        // Eliminar registos
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete dados where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Registo eliminado com sucesso!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Por favor, selecione o registo que pretende eliminar.");
            }
        }
        
        public Produto()
        {
            InitializeComponent();
        }

        private void btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }
    }
}
