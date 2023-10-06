using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IEmailAplicacao: IBaseAplicacao<Email>
    {
        Resultado<object> SalvarEmailParaUsuarios(List<Usuario> destinatarios, string assunto, string corpo, string usuarioId, ICollection<Anexo> Anexos);
        Email GerarEmail(string assunto, string corpo, string usuarioId, ICollection<Anexo> Anexos);
    }
}
