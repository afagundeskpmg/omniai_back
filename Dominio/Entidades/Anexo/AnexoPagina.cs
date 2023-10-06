using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public  class AnexoPagina
    {
        public AnexoPagina() { }
        public AnexoPagina(int ordem) 
        {
            Ordem = ordem;            
        }
        public int Id { get; set; }
        public int Ordem { get; set; }  
        public int AnexoPaiId { get; set; }
        public virtual Anexo AnexoPai { get; set; }
        public int AnexoId { get; set; }
        public virtual Anexo Anexo { get; set; }

        public object Serializar() 
        {
            return new
            {
                Id = AnexoId,
                Nome = Anexo.NomeArquivoOriginal,
                Ordem = Ordem
            };

        }
    }
}
