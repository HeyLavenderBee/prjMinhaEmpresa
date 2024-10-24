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
    public partial class frmAlterarProduto: Form
    {
        // Variáveis de controle da conexão
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        //outras
        double vPrecoTotal = 0;
        double vPrecoCusto = 0;
        double vMargemLucro = 0;

        public frmAlterarProduto()
        {
            InitializeComponent();
            conexao = new MySqlConnection("server=localhost;port=3306;database=bd_MinhaEmpresa_2DS_MTEC_TA;user=root");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCadProduto_Activated(object sender, EventArgs e)
        {
            txtCodProduto.Focus();
        }

        private void txtCodProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                try
                {
                    strSQL = "SELECT * FROM tb_produtos WHERE Prod_Codigo = @parCodProduto"; // variavel de parametro (@parUsuario)
                    comando = new MySqlCommand(strSQL, conexao); // comparando o caminho com o comando
                    comando.Parameters.Clear(); // limpar o parametro, pra ficar sem sujeira e deixar mais seguro
                    comando.Parameters.AddWithValue("@parCodProduto", txtCodProduto.Text); // colocar o valor certo no @parUsuario
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
                        
                        toolStripStatusMSG.Text = "Código Inexistente!";
                        conexao.Close();
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            txtDescProduto.Text = Convert.ToString(dr["Prod_Descricao"]);
                            txtQtdEstoque.Text = Convert.ToString(dr["Prod_Qtd_Estoq"]);
                            txtEstoqueMin.Text = Convert.ToString(dr["Prod_Estoq_Min"]);
                            cmbUnidade.Text = Convert.ToString(dr["Prod_Unidade"]);
                            txtLocacao.Text = Convert.ToString(dr["Prod_Locacao"]);
                            txtPrecoCusto.Text = Convert.ToString(dr["Prod_PrcCusto"]);
                            txtMargemLucro.Text = Convert.ToString(dr["Prod_Marg_Lucro"]);

                            vPrecoCusto = double.Parse(txtPrecoCusto.Text);
                            vMargemLucro = double.Parse(txtMargemLucro.Text);
                        }

                        txtCodProduto.ReadOnly = true;
                        txtDescProduto.Focus();
                        txtDescProduto.ReadOnly = false;
                        txtQtdEstoque.ReadOnly = false;
                        cmbUnidade.Enabled = true;
                        //cmbUnidade.SelectedIndex = -1;
                        txtLocacao.ReadOnly = false;
                        txtEstoqueMin.ReadOnly = false;
                        txtPrecoCusto.ReadOnly = false;
                        txtMargemLucro.ReadOnly = false;
                        btnCadastrar.Enabled = true;
                        txtPrecoVenda.Text = "0,00";

                    }
                    //MessageBox.Show("Conexão feita com sucesso!");
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtDescProduto.Text == "")
            {
                MessageBox.Show("A descrição do produto é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescProduto.Focus();
                txtDescProduto.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (txtQtdEstoque.Text == "")
            {
                MessageBox.Show("A quantidade no estoque é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQtdEstoque.Focus();
                txtQtdEstoque.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (cmbUnidade.SelectedIndex < 0)
            {
                MessageBox.Show("O tipo de unidade é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbUnidade.Focus();
                cmbUnidade.Enabled = true;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (txtLocacao.Text == "")
            {
                MessageBox.Show("A locação é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLocacao.Focus();
                txtLocacao.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (txtEstoqueMin.Text == "")
            {
                MessageBox.Show("A quantidade mínima do estoque é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEstoqueMin.Focus();
                txtEstoqueMin.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (txtPrecoCusto.Text == "")
            {
                MessageBox.Show("O preço custo é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecoCusto.Focus();
                txtPrecoCusto.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }
            else if (txtMargemLucro.Text == "")
            {
                MessageBox.Show("A margem de lucro é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMargemLucro.Focus();
                txtMargemLucro.ReadOnly = false;
                btnCadastrar.Enabled = true;
                return;
            }


            try
            {
                dr.Close();
                strSQL = "UPDATE tb_produtos SET Prod_Codigo=@parCodigo, Prod_Descricao=@parDesc, Prod_Qtd_Estoq=@parQtdEstoque, Prod_Estoq_Min=@parEstoqueMin, Prod_Unidade=@parUnidade, Prod_Locacao=@parLocacao,";
                strSQL += "Prod_PrcCusto=@parPrecoCusto, Prod_Marg_Lucro=@parMargemLucro, Prod_ult_Atualizacao=CURRENT_TIMESTAMP";
                comando = new MySqlCommand(strSQL, conexao); // comparando o caminho com o comando
                comando.Parameters.Clear(); // limpar o parametro, pra ficar sem sujeira e deixar mais seguro
                comando.Parameters.AddWithValue("@parCodigo", txtCodProduto.Text);
                comando.Parameters.AddWithValue("@parDesc", txtDescProduto.Text);
                comando.Parameters.AddWithValue("@parQtdEstoque", txtQtdEstoque.Text);
                comando.Parameters.AddWithValue("@parEstoqueMin", txtEstoqueMin.Text);
                comando.Parameters.AddWithValue("@parUnidade", cmbUnidade.SelectedItem);
                comando.Parameters.AddWithValue("@parLocacao", txtLocacao.Text);
                comando.Parameters.AddWithValue("@parPrecoCusto", txtPrecoCusto.Text);
                comando.Parameters.AddWithValue("@parMargemLucro", txtMargemLucro.Text);
                calcularPreco();
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
                MessageBox.Show("Produto cadastrado com sucesso!");
                conexao.Close();
                comando = null;

            }
        }

        private void txtDescProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtQtdEstoque.Focus();
            }
        }

        private void txtQtdEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                cmbUnidade.Focus();
            }
        }

        private void cmbUnidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtLocacao.Focus();
            }
        }

        private void txtLocacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtEstoqueMin.Focus();
            }
        }

        private void txtEstoqueMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtPrecoCusto.Focus();
            }
        }

        private void txtPrecoCusto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                txtMargemLucro.Focus();
            }
        }

        private void txtMargemLucro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //faz o tab virar enter
            {
                btnAlterar_Click(sender, e);
                calcularPreco();
            }
        }

        private void calcularPreco()
        {
            double vPrecoCusto = Convert.ToInt32(txtPrecoCusto.Text);
            double vMargemLucro = Convert.ToInt32(txtMargemLucro.Text);
            vPrecoTotal = ((vMargemLucro / 100) * vPrecoCusto) + vPrecoCusto;
            txtPrecoVenda.Text = vPrecoTotal.ToString();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            conexao.Close();
            txtCodProduto.Focus();
            txtCodProduto.ReadOnly = false;
            txtCodProduto.Clear();
            txtDescProduto.ReadOnly = true;
            txtDescProduto.Clear();
            txtQtdEstoque.ReadOnly = true;
            txtQtdEstoque.Clear();
            cmbUnidade.Enabled = false;
            cmbUnidade.SelectedIndex = -1;
            txtLocacao.ReadOnly = true;
            txtLocacao.Clear();
            txtEstoqueMin.ReadOnly = true;
            txtEstoqueMin.Clear();
            txtPrecoCusto.ReadOnly = true;
            txtPrecoCusto.Clear();
            txtMargemLucro.ReadOnly = true;
            txtMargemLucro.Clear();
            btnCadastrar.Enabled = false;
            txtPrecoVenda.Text = "0,00";
        }



    }
}
