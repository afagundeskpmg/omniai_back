using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ProjetoAnexo
    {
        public ProjetoAnexo() { }
        public ProjetoAnexo(Projeto projeto, ProcessamentoIndexer processamento, string? documentoTipoid, string usuarioId)
        {
            DocumentoTipoId = documentoTipoid;
            Projeto = projeto;
            ProjetoId = projeto.Id;
            ProcessamentoIndexer = processamento;
            ProcessamentoIndexerId = processamento.Id;
            ProcessamentoAnexo = new ProcessamentoAnexo(usuarioId, ProcessamentoStatusEnum.ProcessamentoSolicitado, this);
        }
        public int Id { get; set; }
        public int AnexoId { get; set; }
        public string? DocumentoTipoId { get; set; }
        public string ProjetoId { get; set; }
        public string ProcessamentoIndexerId { get; set; }
        public virtual Anexo Anexo { get; set; }
        public virtual DocumentoTipo? DocumentoTipo { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual ProcessamentoAnexo ProcessamentoAnexo { get; set; }
        public virtual ProcessamentoIndexer ProcessamentoIndexer { get; set; }
        public virtual ICollection<ProcessamentoPergunta> ProcessamentosPerguntas { get; set; }

        public object SerializarParaListar()
        {
            return new
            {
                Anexo = Anexo.SerializarParaListar(),
                DocumentoTipo = DocumentoTipo.Serializar(),
                Projeto = Projeto.Serializar(),
            };
        }
        public object Serializar()
        {
            return new
            {
                Anexo = new
                {
                    Id = Anexo.Id,
                    Nome = Anexo.NomeArquivoOriginal,
                    CriadoEm = Anexo.CriadoEm?.ToString("g"),
                    CriadoPor = Anexo.CriadoPor.UserName,
                    DocumentoTipo = DocumentoTipo.Serializar(),
                }
              
            };
        }
    }
}
