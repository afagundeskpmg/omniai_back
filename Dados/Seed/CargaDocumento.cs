using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaDocumento
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new Documento() { Id = 1, Nome = "CPF", Mascara = "999.999.999-99", Principal = true, PessoaTipoId = (int)PessoaTipoEnum.Fisica, PaisId = (int)PaisEnum.Brasil },
                new Documento() { Id = 2, Nome = "CNPJ", Mascara = "00.000.000/0000-00", Principal = true, PessoaTipoId = (int)PessoaTipoEnum.Juridica, PaisId = (int)PaisEnum.Brasil },
            };
        }
    }
}
