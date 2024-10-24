using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace prjMinhaEmpresa
{
    public partial class frmLogin : Form
    {
        // Variáveis de controle da conexão
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        public frmLogin()
        {
            InitializeComponent();
            conexao = new MySqlConnection("server=localhost;port=3306;database=bd_MinhaEmpresa_2DS_MTEC_TA;user=root");
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Campo usuário é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Campo senha é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                strSQL = "SELECT log_senha FROM tb_login WHERE log_usuario = @parUsuario"; // variavel de parametro (@parUsuario)
                comando = new MySqlCommand(strSQL, conexao); // comparando o caminho com o comando
                comando.Parameters.Clear(); // limpar o parametro, pra ficar sem sujeira e deixar mais seguro
                comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text); // colocar o valor certo no @parUsuario
                conexao.Open();
                comando.ExecuteScalar(); // pesquisar entre reader, scalar e nonquery
            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro ao conectar banco de dados! " + Erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // return;
                Application.Exit();
                //throw;
            }
            finally
            {
                if (comando.ExecuteScalar() == null)//caso não ache o usuario
                {
                    MessageBox.Show("Usuário não cadastrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                    conexao.Close();
                    comando = null;
                }
                else
                {
                    if (Convert.ToString(comando.ExecuteScalar()) != txtSenha.Text) //caso a senha do banco for diferente da digitada
                    {
                        MessageBox.Show("Senha inválida", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSenha.Focus();
                        conexao.Close();
                        comando = null;
                    }
                    else
                    {
                        conexao.Close();
                        comando = null;
                        MessageBox.Show("Acesso liberado!", "Acesso");

                        this.Visible = false;
                        frmPrincipal objPrincipal = new frmPrincipal();
                        objPrincipal.Show();
                    }
                }
            }

            
        }

        private void lblCadastrarUsuario_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            frmCadNovoUsuario objCadastro = new frmCadNovoUsuario();
            objCadastro.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtSenha.Focus();
            }
        }
    }
}
