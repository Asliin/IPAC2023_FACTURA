using Datos;
using Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    public partial class UsuariosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }
        string tipoOperacion = string.Empty;
        UsuarioDB usuarioDB = new UsuarioDB();
        Usuario user = new Usuario();

        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            HabilitarControles();
            ModificarButton.Enabled = false;
            EliminarButton.Enabled = false;
            CodigoTextBox.Focus();
            tipoOperacion = "Nuevo";
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            PasswordTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            AdjuntarImagenButton.Enabled = true;
            CancelarButton.Enabled = true;
            NuevoButton.Enabled = false;
            GuardarButton.Enabled = true;
        }

        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            PasswordTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            AdjuntarImagenButton.Enabled = false;
            NuevoButton.Enabled = true;
            GuardarButton.Enabled = false;
            //EliminarButton.Enabled = false;
            CancelarButton.Enabled = false;
            //ModificarButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            NombreTextBox.Clear();
            PasswordTextBox.Clear();
            CorreoTextBox.Clear();
            RolComboBox.Text = "";
            EstaActivoCheckBox.Checked = false;
            FotoPictureBox.Image = null;
            errorProvider1.Clear();
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
            NuevoButton.Enabled = true;
            ModificarButton.Enabled = true;
            EliminarButton.Enabled = true;
        }

        private void AdjuntarImagenButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                FotoPictureBox.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese el código");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese el nombre");
                    NombreTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    errorProvider1.SetError(PasswordTextBox, "Ingrese el código");
                    PasswordTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                user.CodigoUsuario = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Password = PasswordTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstaActivo = EstaActivoCheckBox.Checked;

                if (FotoPictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    user.Foto = ms.GetBuffer();
                }

                //Insertar en la base de datos

                bool inserto = usuarioDB.Insertar(user);
                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    NuevoButton.Enabled = true;
                    ModificarButton.Enabled = true;
                    EliminarButton.Enabled = true;
                    TraerUsuarios();
                    MessageBox.Show("Registro guardado.");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro.");
                }


            }
            else if (tipoOperacion == "Modificar")
            {
                user.CodigoUsuario = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Password = PasswordTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstaActivo = EstaActivoCheckBox.Checked;

                if (FotoPictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    user.Foto = ms.GetBuffer();
                }

                bool edito = usuarioDB.Editar(user);
                if (edito)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    ModificarButton.Enabled = true;
                    EliminarButton.Enabled = true;
                    TraerUsuarios();
                    MessageBox.Show("Registro actualizado.");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro.");
                }
            }
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            tipoOperacion = "Modificar";

            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString();
                NombreTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                PasswordTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Contrasena"].Value.ToString();
                CorreoTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                RolComboBox.Text = UsuariosdataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(UsuariosdataGridView.CurrentRow.Cells["EstaActivo"].Value);

                byte[] img = usuarioDB.DevolverImagen(UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                if (img.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(img);
                    FotoPictureBox.Image = Bitmap.FromStream(ms);
                }

                HabilitarControles();
                ModificarButton.Enabled = false;
                EliminarButton.Enabled = false;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro.");
            }


        }

        private void UsuariosForm_Load(object sender, System.EventArgs e)
        {
            TraerUsuarios();
        }

        private void TraerUsuarios()
        {
            DataTable dt = new DataTable();
            dt = usuarioDB.DevolverUsuarios();
            UsuariosdataGridView.DataSource = dt;
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Seguro que desea eliminar el registro seleccionado?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
                if (dialogResult == DialogResult.Yes)
                {
                    bool elimino = usuarioDB.Eliminar(UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());
                    if (elimino)
                    {
                        MessageBox.Show("Registro eliminado exitosamente.");
                        TraerUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro.");
            }

        }
    }
}
