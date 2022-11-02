using System;
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
                _usuariosForm.FormClosed += _usuariosForm_FormClosed;
                _usuariosForm.Show();
            }
            else
            {
                _usuariosForm.Activate();
            }
            
        }

        private void _usuariosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           _usuariosForm=null;
        }

        private void ProductoToolStripButton_Click(object sender, EventArgs e)
        {
            if (_productosForm==null)
            {
                _productosForm = new ProductosForm();
                _productosForm.MdiParent = this;
                _productosForm.FormClosed += _productosForm_FormClosed;
                _productosForm.Show();
            }
           else
            {
                _productosForm.Activate();
            }
        }

        private void _productosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           _productosForm=null;
        }
    }
}
