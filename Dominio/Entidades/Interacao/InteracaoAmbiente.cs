namespace Dominio.Entidades
{
    public class InteracaoAmbiente : Interacao
    {
        public int AmbienteId { get; set; }
        public virtual Ambiente Ambiente { get; set; }

        public override object SerializarParaListar()
        {
            return new
            {
                Id,
                Descricao,
                CriadoEm = CriadoEm?.ToString("g"),
                CriadoPorUsername = CriadoPor.UserName
            };
        }
    }
}
