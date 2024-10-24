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
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < 100)
            {
                progressBar.Value += 1;
            }
            else
            {
                this.Visible = false;
                timer.Enabled = false;
                frmLogin objLogin = new frmLogin();
                objLogin.Show();
            }
            
        }

        private void frmSplash_Activated(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {

        }
    }
}
