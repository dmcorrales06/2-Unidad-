using Datos;
using Entidades;
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
    public partial class UsuariosForm : Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }
        UsuarioDatos userDatos = new UsuarioDatos();
        string tipoOperacion = string.Empty;
        Usuario user;
        private void CodigoTextBox_Load(object sender, EventArgs e)
        {
            LlenarDataGrid();
        }

        private async void LlenarDataGrid()
        {
            UsuariosDataGridView.DataSource = await userDatos.DevolverListaAsync();
        }

        

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            ClaveTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
        }
        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            ClaveTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
        }

     
        private void LimpiarControles()
        {                           //Dos formas mas
            CodigoTextBox.Clear(); //String.Empty
            NombreTextBox.Clear(); //"";
            ClaveTextBox.Clear();
            CorreoTextBox.Clear();
            RolComboBox.Text = String.Empty;
            EstaActivoCheckBox.Checked = false;
        }
      
        private void NuevoButton_Click_1(object sender, EventArgs e)
        {
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void ModificarButton_Click_1(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                NombreTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                ClaveTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Clave"].Value.ToString();
                CorreoTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                RolComboBox.Text = UsuariosDataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(UsuariosDataGridView.CurrentRow.Cells["EstaActivo"].Value);
                HabilitarControles();
                CodigoTextBox.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void EliminarButton_Click(object sender, EventArgs e)
        {
            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = await userDatos.EliminarAsync(UsuariosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                if (elimino)
                {
                    LlenarDataGrid();
                    MessageBox.Show("Usuario Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void GuardarButton_Click(object sender, EventArgs e)
        {
            user = new Usuario();

            if (tipoOperacion == "Nuevo")
            {
                if (CodigoTextBox.Text == "")
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un nombre");
                    NombreTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ClaveTextBox.Text))
                {
                    errorProvider1.SetError(ClaveTextBox, "Ingrese una clave");
                    ClaveTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }

                user.Codigo = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Clave = ClaveTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstaActivo = EstaActivoCheckBox.Checked;

                bool inserto = await userDatos.InsertarAsync(user);

                if (inserto)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Usuario Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoOperacion == "Modificar")
            {
                if (CodigoTextBox.Text == "")
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un nombre");
                    NombreTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ClaveTextBox.Text))
                {
                    errorProvider1.SetError(ClaveTextBox, "Ingrese una clave");
                    ClaveTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }

                user.Codigo = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Clave = ClaveTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstaActivo = EstaActivoCheckBox.Checked;

                bool modifico = await userDatos.ActualizarAsync(user);
                if (modifico)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Usuario Modificado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no modificado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {

            DesabilitarControles();
            LimpiarControles();
        }
    }
}
