using ChatCliente;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        String connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ChatLine; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void BtnEntrarClick(object sender, EventArgs e)
        {
            //verifica se o login digitado existe no banco
            String sql = "(SELECT * from Usuario WHERE LOGIN = '" + txtLogin.Text + "' AND SENHA = '" + txtSenha.Text + "')";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            reader = cmd.ExecuteReader();
            try
            {
                if (!reader.Read())
                {
                    MessageBox.Show("LOGIN E SENHA INCORRETOS");
                }
                else
                {
                    this.Hide();
                    //Abrir a nova tela
                    Usuario usuario = new Usuario();
                    FormChatCliente chatCliente = new FormChatCliente();
                    //FormChatServidor chatServidor = new FormChatServidor();

                    reader.Close();
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        usuario.Login = reader2["LOGIN"].ToString();
                        usuario.Nome = reader2["NOME"].ToString();
                        usuario.Sobrenome = reader2["SOBRENOME"].ToString();
                        usuario.Cpf = reader2["CPF"].ToString();
                        usuario.DtNascimento = reader2["DTNASCIMENTO"].ToString();
                    }
                    chatCliente.CarregaUsuario(usuario);
                    //chatServidor.Show();
                    chatCliente.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void bntCadastrarClick(object sender, EventArgs e)
        {
            Cadastrar cadastrar = new Cadastrar();
            cadastrar.Show();
        }
    }
}
