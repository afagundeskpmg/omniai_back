using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Projeto : Registro
    {
        public Projeto() { }
        public Projeto(string nome,int ambienteId, string usuarioId) {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            AmbienteId = ambienteId;
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public int AmbienteId { get; set; }
        public virtual Ambiente Ambiente { get; set; }
        public virtual ICollection<ProjetoAnexo> Anexos { get; set; }    
        public virtual ICollection<ProcessamentoIndexer> ProcessamentosIndexers {  get; set; }
        public virtual ICollection<ProcessamentoNer> ProcessamentosNer { get; set; }
        public virtual ICollection<PerguntaResposta> PerguntasRespostas { get; set; }
        public string CaminhoBlobStorage
        {
            get
            {
                return @Path.Combine("/Ambiente", AmbienteId.ToString(), "Anexos", "Projetos", Id).Replace("\\", "/");
            }
        }
        public object Serializar() 
        {
            return new 
            {
                Id,
                Nome,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorNome = CriadoPor.Nome,
            };
        }
        public object SerializarParaListar()
        {
            var projetoAnexos = this.Anexos?.Select(a => a.Serializar()).ToList();
            return new
            {
                Id,
                Nome,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorNome = CriadoPor.Nome,                                
                Ambiente = Ambiente.SerializarParaListar(),
                Anexos = projetoAnexos
            };
        }
    }
}
