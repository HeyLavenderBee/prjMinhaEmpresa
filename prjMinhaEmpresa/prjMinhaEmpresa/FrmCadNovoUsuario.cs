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
    public partial class frmCadNovoUsuario : Form
    {
        // Variáveis de controle da conexão
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        public frmCadNovoUsuario(){

            InitializeComponent();
            conexao = new MySqlConnection("server=localhost;port=3306;database=bd_MinhaEmpresa_2DS_MTEC_TA;user=root");

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtSenha.ReadOnly = true;
            txtNomeUsuario.ReadOnly = true;
            txtUsuario.ReadOnly = false;
            txtUsuario.Enabled = true;
            txtNomeUsuario.Text = "";
            txtSenha.Text = "";
            txtUsuario.Text = "";
            toolStripStatusMSG.Text = "Digite o usuário <Enter>";
        }

        private void btnCadatrar_Click(object sender, EventArgs e)
        {
            if (txtNomeUsuario.Text == "")
            {
                MessageBox.Show("Campo nome é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
                txtSenha.ReadOnly = false;
                txtNomeUsuario.ReadOnly = false;
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Campo senha é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
                txtSenha.ReadOnly = false;
                txtNomeUsuario.ReadOnly = false;
            }
            try
            {
                dr.Close();
                strSQL = "INSERT INTO tb_login SET log_usuario=@parUsuario, log_senha=@parSenha, log_nome=@parNome, log_ult_Atualizacao=CURRENT_TIMESTAMP";
                comando = new MySqlCommand(strSQL, conexao); // comparando o caminho com o comando
                comando.Parameters.Clear(); // limpar o parametro, pra ficar sem sujeira e deixar mais seguro
                comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text);
                comando.Parameters.AddWithValue("@parNome", txtNomeUsuario.Text);
                comando.Parameters.AddWithValue("@parSenha", txtSenha.Text); // colocar o valor certo no @parUsuario
                comando.ExecuteNonQuery(); //não vai retornar nada, apenas insert, deletar e atualizar
                //conexao já está aberta
            }
            catch (Exception Erro) //se der um erro
            {
                MessageBox.Show("Erro ao conectar banco de dados! " + Erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // return;
                Application.Exit();
                //throw;
            }
            finally
            {
                MessageBox.Show("Conexão feita com sucesso!");
                conexao.Close();
                comando = null;
                this.Visible = false;
                frmLogin objLogin = new frmLogin();
                objLogin.Show();
            }
            
            
            
        }

        private void frmCadNovoUsuario_Activated(object sender, EventArgs e)
        {
            txtSenha.Enabled = false;
            txtNomeUsuario.Enabled = false;
            btnCadatrar.Enabled = false;
            txtSenha.ReadOnly = true;
            txtNomeUsuario.ReadOnly = true;
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                
                try
                {
                    strSQL = "SELECT * FROM tb_login WHERE log_usuario = @parUsuario"; // variavel de parametro (@parUsuario)
                    comando = new MySqlCommand(strSQL, conexao); // comparando o caminho com o comando
                    comando.Parameters.Clear(); // limpar o parametro, pra ficar sem sujeira e deixar mais seguro
                    comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text); // colocar o valor certo no @parUsuario
                    conexao.Open();
                    dr = comando.ExecuteReader(); // pesquisar entre reader, scalar e nonquery
                    
                }
                catch (Exception Erro) //se der um erro
                {
                    MessageBox.Show("Erro ao conectar banco de dados! " + Erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    // return;
                    Application.Exit();
                    //throw;
                }
                finally
                {
                    if (!dr.HasRows)
                    { //libera os campos
                        txtSenha.Enabled = true;
                        txtNomeUsuario.Enabled = true;
                        btnCadatrar.Enabled = true;
                        txtSenha.ReadOnly = false;
                        txtNomeUsuario.ReadOnly = false;
                        txtNomeUsuario.Focus();
                        //bloquear usuario
                        txtUsuario.ReadOnly = true;

                        toolStripStatusMSG.Text = "Digite o nome do usuário <Enter>";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            txtNomeUsuario.Text = Convert.ToString(dr["log_nome"]);
                            txtSenha.Text = Convert.ToString(dr["log_senha"]);
                        }

                        MessageBox.Show("Usuário já existe!");
                        txtSenha.Enabled = false;
                        txtNomeUsuario.Enabled = false;
                        btnCadatrar.Enabled = false;
                        txtUsuario.ReadOnly = false;
                        txtSenha.ReadOnly = true;
                        txtNomeUsuario.ReadOnly = true;
                    }
                    //MessageBox.Show("Conexão feita com sucesso!");
                }

            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            frmLogin objLogin = new frmLogin();
            objLogin.Show();
        }
    }
}
