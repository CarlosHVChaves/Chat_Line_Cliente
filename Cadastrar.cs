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

namespace Chat_Line
{
    public partial class Cadastrar : Form
    {
        String connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ChatLine; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void BtnCadastrarClick(object sender, EventArgs e)
        {
            if(txtLogin.Text == "" || txtSenha.Text == "" || txtNome.Text == "")
            {
                MessageBox.Show("Campos nao podem ficar em branco");
            }
            else
            {
                String sql = "INSERT INTO USUARIO (LOGIN, SENHA, NOME, SOBRENOME, CPF, DTNASCIMENTO)"
                             + "VALUES ('" + txtLogin.Text + "', '" + txtSenha.Text + "', '" + txtNome.Text + "', '" +
                              txtSobrenome.Text + "', '" + txtCpf.Text + "', '" + txtNascimento.Text + "')";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Cadastro realizado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                txtLogin.Clear();
                txtSenha.Clear();
                txtNome.Clear();
                txtSobrenome.Clear();
                txtCpf.Clear();
                txtNascimento.Clear();
            }
        }
    }
}
