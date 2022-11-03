using Datos;
using Entidades;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
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

        Producto producto = null;
        List<DetalleFactura> detalles = new List<DetalleFactura>();
        Factura factura;
        Cliente cliente;
        FacturaDatos facturaDatos;

        decimal subTotal = 0;
        decimal isv = 0;
        decimal total = 0;

        string usuario = System.Threading.Thread.CurrentPrincipal.Identity.Name;

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

        private async void CodigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProductoDatos productoDatos = new ProductoDatos();
                Producto producto = new Producto();

                producto = await productoDatos.GetPorCodigo(Convert.ToInt32(CodigoTextBox.Text));

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
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (e.KeyChar == (char)Keys.Enter)
            {
                if (Convert.ToInt32(ExistenciaTextBox.Text) >= Convert.ToInt32(CantidadTextBox.Text))
                {
                    DetalleFactura detalle = new DetalleFactura();
                    detalle.CodigoProducto = producto.Codigo;
                    detalle.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                    detalle.Descripcion = producto.Descripcion;
                    detalle.Precio = producto.Precio;
                    detalle.Total = producto.Precio * Convert.ToInt32(CantidadTextBox.Text);

                    detalles.Add(detalle);
                    FacturaDataGridView.DataSource = null;
                    FacturaDataGridView.DataSource = detalles;

                    subTotal = subTotal + detalle.Total;
                    //subTotal += detalle.Total;
                    isv = subTotal * 0.15M;
                    total = subTotal + isv - Convert.ToDecimal(DescuentoTextBox.Text);

                    ISVTextBox.Text = isv.ToString("N");
                    SubTotalTextBox.Text = subTotal.ToString("N");
                    TotalTextBox.Text = total.ToString("N");

                    ExistenciaTextBox.Text = (producto.Existencia - Convert.ToInt32(CantidadTextBox.Text)).ToString();

                }
                else
                {
                    MessageBox.Show("No hay suficientes productos a vender.");
                }*/
            }

        private void FacturaForm_Load(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = VariableGlobal.UsuarioLogin;
            DescuentoTextBox.Text = "0.00";
        }

      
    }
}
