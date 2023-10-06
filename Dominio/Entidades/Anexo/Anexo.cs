namespace Dominio.Entidades
{
    public class Anexo : Registro
    {
        public Anexo() { }
        public Anexo(int anexoTipoId, ArquivoFormato arquivoFormato, AnexoArquivoTipo anexoArquivoTipo, string nomeArquivoOriginal, string usuarioLogadoId)
        {
            AnexoTipoId = anexoTipoId;
            ArquivoFormato = arquivoFormato;
            ArquivoFormatoId = arquivoFormato.Id;
            AnexoArquivoTipo = anexoArquivoTipo;
            AnexoArquivoTipoId = anexoArquivoTipo.Id;
            NomeArquivoOriginal = nomeArquivoOriginal;
            NomeArquivoAlterado = Guid.NewGuid().ToString() + arquivoFormato.Nome;
            AtribuirInformacoesRegistroParaInsercao(usuarioLogadoId);
            Excluido = false;

            if (NomeArquivoOriginal.Count() > 100)
                NomeArquivoOriginal = NomeArquivoOriginal.Replace(arquivoFormato.Nome, "").Substring(0, 90) + arquivoFormato.Nome;

            Paginas = new List<AnexoPagina>();
            PaginasPai = new List<AnexoPagina>();
        }

        public virtual int Id { get; set; }
        public virtual string NomeArquivoAlterado { get; set; }
        public virtual string NomeArquivoOriginal { get; set; }
        public string CaminhoArquivoBlobStorage { get; set; }
        public string BlobContainerName { get; set; }
        public virtual int AnexoTipoId { get; set; }
        public virtual AnexoTipo AnexoTipo { get; set; }
        public virtual int AnexoArquivoTipoId { get; set; }
        public virtual AnexoArquivoTipo AnexoArquivoTipo { get; set; }
        public virtual int ArquivoFormatoId { get; set; }
        public virtual ArquivoFormato ArquivoFormato { get; set; }
        public virtual AnexoPagina AnexoPaginaPai
        {
            get
            {
                return PaginasPai.FirstOrDefault();
            }
        }
        public virtual ICollection<ProjetoAnexo> Projetos { get; set; }
        public virtual ICollection<AnexoPagina> PaginasPai { get; set; }
        public virtual ICollection<AnexoPagina> Paginas { get; set; }
        public virtual Email EmailCorpo { get; set; }
        public virtual ICollection<Email> Emails { get; set; }

        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Nome = NomeArquivoOriginal,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPor = CriadoPor.UserName,
                ArquivoFormato = ArquivoFormato.SerializarParaListar()
            };
        }
        public object Serializar()
        {
            return new
            {
                Id,
                Nome = NomeArquivoOriginal,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPor = CriadoPor.UserName,                
            };
        }

    }
}
