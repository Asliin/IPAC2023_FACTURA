using Entidades;
using System.Drawing;
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


        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            HabilitarControles();
            GuardarButton.Enabled = true;
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
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
            ModificarButton.Enabled = false;
            GuardarButton.Enabled = false;
            EliminarButton.Enabled = false;
            CancelarButton.Enabled = false;
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

                Usuario user = new Usuario();

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



            }
            else if (tipoOperacion == "Modificar")
            {

            }
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            tipoOperacion = "Modificar";
        }
    }
}
