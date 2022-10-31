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
            _usuariosForm = new UsuariosForm();
            _usuariosForm.MdiParent = this;
            _usuariosForm.Show();
        }

        private void ProductoToolStripButton_Click(object sender, EventArgs e)
        {
            _productosForm = new ProductosForm();
            _productosForm.MdiParent = this;
            _productosForm.Show();
        }
    }
}
