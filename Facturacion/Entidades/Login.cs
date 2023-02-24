namespace Entidades
{
    public class Login
    {
        public string CodigoUsuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public Login()
        {
        }

        public Login(string codigoUsuario, string password, string rol)
        {
            CodigoUsuario = codigoUsuario;
            Password = password;
            Rol = rol;
        }
    }
}
