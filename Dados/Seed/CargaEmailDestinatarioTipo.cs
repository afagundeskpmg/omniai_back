using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaEmailDestinatarioTipo
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new EmailDestinatarioTipo() { Id = (int)EmailDestinatarioTipoEnum.Usuario, Nome = "Usuario" },
                new EmailDestinatarioTipo() { Id = (int)EmailDestinatarioTipoEnum.Contato, Nome = "Contato" },
            };
        }
    }
}
