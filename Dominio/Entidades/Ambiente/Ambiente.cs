namespace Dominio.Entidades
{
    public class Ambiente : Registro
    {
        public Ambiente(int id, int clienteId)
        {
            Id = id;
            ClienteId = clienteId;
            Usuarios = new List<Usuario>();
            CognitiveSearchIndexName = "omniai-index-vf";//verificar se existe
            CognitiveSearchSkillSetName = "skillset1695926195364"; // adicioar no app settings o default
            NumeroEntidades = 10;
            QuantidadeMaximaUsuariosAtivos = 10;
            CongnitiveSearchSize = 3;
            LimiteTokenPorRequisicao = 7160;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int CongnitiveSearchSize { get; set; }
        public string CognitiveSearchIndexName { get; set; }
        public string CognitiveSearchSkillSetName { get; set; }
        public int LimiteTokenPorRequisicao { get; set; }
        public int? NumeroEntidades { get; set; }
        public virtual PessoaJuridica Cliente { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<InteracaoAmbiente> Interacoes { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
        public virtual ICollection<DocumentoTipo> DocumentosTipo { get; set; }
        public int QuantidadeMaximaUsuariosAtivos { get; set; }        

        public int NumeroEntidadesCadastradas()
        {
            if (DocumentosTipo == null || (DocumentosTipo != null && !DocumentosTipo.Any(x => !x.Excluido && (x.Entidades != null && x.Entidades.Any()))))
                return 0;
            else
                return DocumentosTipo.Where(x => !x.Excluido && (x.Entidades != null && x.Entidades.Any())).Select(x => x.Entidades).Count();

        }

        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Cliente = new
                {
                    Cliente.Nome,
                    Cliente.Documento,
                    Cliente.DocumentoFormatado
                },
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorUserName = CriadoPor.UserName
            };
        }
    }
}
