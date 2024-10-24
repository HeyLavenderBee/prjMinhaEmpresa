using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjMinhaEmpresa
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void sairDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadProduto objCadProduto = new frmCadProduto();
            objCadProduto.MdiParent = this;
            objCadProduto.Show();
        }

        private void alterasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlterarProduto objAlterarProduto = new frmAlterarProduto();
            objAlterarProduto.MdiParent = this;
            objAlterarProduto.Show();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeletarProduto objDeletarProduto = new frmDeletarProduto();
            objDeletarProduto.MdiParent = this;
            objDeletarProduto.Show();
        }
    }
}
