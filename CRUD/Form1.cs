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

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Incluir
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                MessageBox.Show("Elemento já inserido! Favor atualizar ou excluir"
                    , "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nome = txtNome.Text;
            string email = txtEmail.Text;
            string telefone = txtTelefone.Text;

            SqlConnection con = new SqlConnection();

            con.ConnectionString = Dados.StringDeConexao;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"INSERT INTO agenda (nome, email, telefone) values (@nome, @email, @telefone)";

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@telefone", telefone);

            con.Open();

            cmd.ExecuteNonQuery();

            MessageBox.Show("Registro Inserido", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();

            LimparCampos();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da.SelectCommand = new SqlCommand();
            con.ConnectionString = Dados.StringDeConexao;

            da.SelectCommand.CommandText = "select * from agenda";

            con.Open();

            da.SelectCommand.Connection = con;
            da.Fill(dt);
            dgvAgenda.DataSource = dt;

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtEmail.Clear();
            txtNome.Clear();
            txtTelefone.Clear();
            txtFiltro.Clear();
            AtualizaGrid();
        }

        #region dgv Agenda Double Click
        private void dgvAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvAgenda[0, dgvAgenda.CurrentRow.Index].Value.ToString();
            txtNome.Text = dgvAgenda[1, dgvAgenda.CurrentRow.Index].Value.ToString();
            txtEmail.Text = dgvAgenda[2, dgvAgenda.CurrentRow.Index].Value.ToString();
            txtTelefone.Text = dgvAgenda[3, dgvAgenda.CurrentRow.Index].Value.ToString();
        }
        #endregion

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Favor selecionar um elemento para atualizar!"
                    , "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int codigo = Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            string email = txtEmail.Text;
            string telefone = txtTelefone.Text;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Dados.StringDeConexao;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"UPDATE agenda SET nome = @nome, email = @email, telefone = @telefone where codigo = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@telefone", telefone);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro Atualizado", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception err)
            {
                MessageBox.Show("Erro na comunicação com o Banco de Dados: "+ err.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            LimparCampos();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Favor selecionar um elemento para excluir!"
                    , "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int codigo = Convert.ToInt32(txtCodigo.Text);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Dados.StringDeConexao;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"DELETE FROM agenda WHERE codigo = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro Excluido", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro na comunicação com o Banco de Dados: " + err.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            LimparCampos();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "")
            {
                MessageBox.Show("Favor informar um nome para buscar!"
                    , "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nome = txtFiltro.Text;
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            conn.ConnectionString = Dados.StringDeConexao;

            da.SelectCommand = new SqlCommand();
            da.SelectCommand.CommandText = @"SELECT * FROM agenda WHERE nome like @nome";
            da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");

            try
            {
                conn.Open();
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                dgvAgenda.DataSource = dt;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro na comunicação com o Banco de Dados: " + err.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
