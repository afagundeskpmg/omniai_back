using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public  class DocumentoTipo :Registro
    {
        public DocumentoTipo() { }
        public DocumentoTipo(string nome,int ambienteId,string usuarioId) {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            AmbienteId = ambienteId;
            AtribuirInformacoesRegistroParaInsercao(usuarioId);
        }
        public string Id { get; set; }
        public string Nome { get; set; } 
        public int AmbienteId { get; set; }
        public virtual Ambiente Ambiente { get; set; }
        public virtual ICollection<Entidade> Entidades { get; set; }
        public virtual ICollection<ProjetoAnexo> Anexos { get; set; }        
        public object Serializar() 
        {
            return new {
                Id ,
                Nome                              
            };
        }
        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Nome,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorNome = CriadoPor.Nome,
                Ambiente = Ambiente.SerializarParaListar()                
            };
        }
    }
}
