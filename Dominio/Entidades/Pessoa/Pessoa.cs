namespace Dominio.Entidades
{
    public abstract class Pessoa : Registro
    {
        public Pessoa() { }
        public Pessoa(string documento, string nome, int paisId, string usuarioLogadoId, int pessoaTipoId)
        {
            Documento = documento;
            Nome = nome;
            PaisId = paisId;
            PessoaTipoId = pessoaTipoId;
            AtribuirInformacoesRegistroParaInsercao(usuarioLogadoId);
        }

        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public int PaisId { get; set; }
        public virtual int PessoaTipoId { get; set; }                
        public virtual bool Ativo { get; set; }
        public virtual string Documento { get; set; }
        public virtual int? DocumentoTipoId { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual PessoaTipo PessoaTipo { get; set; }
        public virtual Documento DocumentoTipo { get; set; }  
        public string NomeLimitado(int caracteres)
        {
            string nome;
            if (Nome.Length > caracteres)
                nome = Nome.Substring(0, caracteres) + "..";
            else
                nome = Nome;

            return nome;
        }
        public bool CombinarNome(string nomeComparar) => Validacoes.Nome.CompararNomes(Nome, nomeComparar);
        public bool IsDocumentoLGPD()
        {
            return Documento.Replace("*", "").Count() == 6;
        }
        public virtual string DocumentoFormatado
        {
            get
            {
                switch (DocumentoTipoId)
                {
                    case (int)DocumentoEnum.CNPJ:
                        return ((PessoaJuridica)this).CNPJFormatado;
                    default:
                        return Documento;
                }
            }
        }
        public object SerializarPessoaParaAPI()
        {
            return new
            {
                Nome,               
                DocumentoFormatado,
                PessoaTipo = new { PessoaTipo?.Id, PessoaTipo?.Nome },
            };
        }
        public virtual string NomeSemAcento
        {
            get
            {
                return Validacoes.Nome.RemoverAcentos(Nome.ToUpper()).Replace('/', ' ').Replace(@"\", " ");
            }
        }
        public object SerializarParaAtribuir()
        {
            if (PessoaTipoId == (int)PessoaTipoEnum.Fisica)
                return new { Nome, Documento };
            else
                return new { Documento };
        }
        public object SerializarParaGerarCrawler()
        {
            return new
            {
                PessoaIdTPRM = Id,
                Nome,
                Documento,
                PessoaTipoId,
            };
        }
        public bool PertencePais(PaisEnum pais) => PaisId == (int)pais;
    }
}
