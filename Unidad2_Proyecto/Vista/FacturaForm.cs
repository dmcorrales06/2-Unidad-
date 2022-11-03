using Datos;
using Entidades;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FacturaForm : Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        Producto _producto = null;
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
               cliente = new Cliente();

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
                _producto = new Producto();

                _producto = await productoDatos.GetPorCodigo(Convert.ToInt32(CodigoTextBox.Text));

                if (_producto.Codigo > 0)
                {
                    DescripcionTextBox.Text = _producto.Descripcion;
                    ExistenciaTextBox.Text = _producto.Existencia.ToString();
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Convert.ToInt32(ExistenciaTextBox.Text) >= Convert.ToInt32(CantidadTextBox.Text))
                {
                    DetalleFactura detalle = new DetalleFactura();
                    detalle.CodigoProducto = _producto.Codigo;
                    detalle.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                    detalle.Descripcion = _producto.Descripcion;
                    detalle.Precio=_producto.Precio;
                    detalle.Total=_producto.Precio * Convert.ToInt32(CantidadTextBox.Text);

                    detalles.Add(detalle);
                    FacturaDataGridView.DataSource = null;
                    FacturaDataGridView.DataSource=detalles;


                    subTotal = subTotal + detalle.Total;
                    //subTotal += detalle.Total;
                    isv = subTotal * 0.15M;
                    total = subTotal + isv - Convert.ToDecimal(DescuentoTextBox.Text);

                    ISVTextBox.Text = isv.ToString("N");
                    SubTotalTextBox.Text = subTotal.ToString("N");      
                    TotalTextBox.Text = total.ToString("N");
                    
                    ExistenciaTextBox.Text = (_producto.Existencia - Convert.ToInt32(CantidadTextBox.Text)).ToString();

                }
                else
                {
                    MessageBox.Show("No hay suficientes productos a vender.");
                }
            }

           
        }

        private void FacturaForm_Load(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = VariableGlobal.UsuarioLogin;
            DescuentoTextBox.Text = "0.00";

        }

        private async void GuardarButton_Click(object sender, EventArgs e)
        {
            if (cliente == null)
            {
                errorProvider1.SetError(IdentidadClienteMaskedTextBox, "Consulte un cliente");
                IdentidadClienteMaskedTextBox.Focus();
                return;
            }

            factura = new Factura();

            factura.Fecha = FechaDateTimePicker.Value;
            factura.CodigoUsuario = UsuarioTextBox.Text;
            factura.ISV = isv;
            factura.SubTotal = subTotal;
            factura.Descuento = Convert.ToDecimal(DescuentoTextBox.Text);
            factura.Total = total;
            factura.IdentidadCliente = cliente.Identidad;

            facturaDatos = new FacturaDatos();
            bool inserto = await facturaDatos.InsertarAsync(factura, detalles);

            if (inserto)
            {
                MessageBox.Show("Factura guardada");
            }

        }
    }
}
