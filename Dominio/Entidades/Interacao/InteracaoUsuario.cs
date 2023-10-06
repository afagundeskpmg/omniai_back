namespace Dominio.Entidades
{
    public class InteracaoUsuario : Interacao
    {
        public InteracaoUsuario()
        {
        }
        public InteracaoUsuario(Usuario usuario, string descricao, string? acessoIP)
        {
            Descricao = descricao.Trim();
            AtribuirInformacoesRegistroParaInsercao(usuario.Id);
            InteracaoTipoId = (int)InteracaoTipoEnum.Usuario;
            UsuarioId = usuario.Id;
            AcessoIP = acessoIP;
        }
        public InteracaoUsuario(string usuarioId, string descricao, string acessoIP) : base(usuarioId, descricao, (int)InteracaoTipoEnum.Usuario)
        {
            UsuarioId = usuarioId;
            AcessoIP = acessoIP;
        }

        public virtual string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string AcessoIP { get; set; }

        public override object SerializarParaListar()
        {
            return new
            {
                Id,
                Descricao,
                CriadoEm = CriadoEm?.ToString(),
                CriadoPor = CriadoPor?.UserName,
                InteracaoTipoId
            };
        }
    }
}
