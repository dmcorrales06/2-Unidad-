using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        UsuariosForm _usuariosForm = null;

        ProductosForm _productosForm = null;
        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            if (_usuariosForm==null)
            {
                _usuariosForm = new UsuariosForm();
                _usuariosForm.MdiParent = this;
                _usuariosForm.Show();
            }
            else
            {
                _usuariosForm.Activate();
            }
            
        }

        private void ProductoToolStripButton_Click(object sender, EventArgs e)
        {
            if (_productosForm==null)
            {
                _productosForm = new ProductosForm();
                _productosForm.MdiParent = this;
                _productosForm.Show();
            }
           else
            {
                _productosForm.Activate();
            }
        }
    }
}
