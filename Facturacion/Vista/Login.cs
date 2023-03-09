using System.Windows.Forms;

namespace Vista
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Activated(object sender, System.EventArgs e)
        {
            CodigoUsuarioTextBox.Focus();
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void AceptarButton_Click(object sender, System.EventArgs e)
        {
            if (CodigoUsuarioTextBox.Text == string.Empty)
            {
                errorProvider1.SetError(CodigoUsuarioTextBox, "Ingrese un usuario.");
                return;
            }
            errorProvider1.Clear();
            if (PasswordTextBox.Text == "")
            {
                errorProvider1.SetError(PasswordTextBox, "Ingrese la contraseña.");
                return;
            }
            errorProvider1.Clear();

            //Validar en la base de datos


            //Mandarlo al menú
            Menu menuFormulario = new Menu();
            this.Hide();
            menuFormulario.Show();


        }

        private void MostrarButton_Click(object sender, System.EventArgs e)
        {
            if (PasswordTextBox.PasswordChar == '*')
            {
                PasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextBox.PasswordChar = '*';
            }

        }
    }
}
