using Datos;
using Entidades;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FacturaForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        private async void IdentidadClienteMaskedTextBox_KeyPressAsync(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ClienteDatos clienteDatos = new ClienteDatos();
                Cliente cliente = new Cliente();

                cliente = await clienteDatos.GetPorIdentidad(IdentidadClienteMaskedTextBox.Text);

                if (cliente.Identidad != null)
                {
                    NombreClienteTextBox.Text = cliente.Nombre;
                    errorProvider1.Clear();
                    CodigoTextBox.Focus();
                }
                else
                {
                    errorProvider1.SetError(NombreClienteTextBox, "No existe el cliente");
                }
            }
        }

       /* private async void CodigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProductoDatos productoDatos = new ProductoDatos();
               Producto  producto = new Producto();

                producto = await productoDatos.GetPorCodigo(Convert.ToInt32(CodigoProductoTextBox.Text));

                if (producto.Codigo > 0)
                {
                    DescripcionTextBox.Text = producto.Descripcion;
                    ExistenciaTextBox.Text = producto.Existencia.ToString();
                    errorProvider1.Clear();
                    CantidadTextBox.Focus();
                }
                else
                {
                    errorProvider1.SetError(DescripcionTextBox, "No existe el producto");
                }
            }
        }*/
    }
}
