using System.Windows.Forms;

namespace Vista
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, System.EventArgs e)
        {
            if (CodigoUsuarioTextBox.Text == string.Empty)
            {
                errorProvider1.SetError(CodigoUsuarioTextBox, "Ingrese un usuario.");
            }
            errorProvider1.Clear();
            if (PasswordTextBox.Text == "")
            {
                errorProvider1.SetError(PasswordTextBox, "Ingrese la contraseña.");
            }
        }
    }
}
