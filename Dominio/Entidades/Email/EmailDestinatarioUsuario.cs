namespace Dominio.Entidades
{
    public class EmailDestinatarioUsuario : EmailDestinatario
    {
        public EmailDestinatarioUsuario()
        {
            EmailDestinatarioTipoId = (int)EmailDestinatarioTipoEnum.Usuario;
        }

        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
