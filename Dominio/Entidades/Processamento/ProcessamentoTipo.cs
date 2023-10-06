namespace Dominio.Entidades
{
    public class ProcessamentoTipo
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string QueueNome { get; set; }
        public virtual ICollection<Processamento> Processamentos { get; set; }

        public object SerializarParaListar()
        {
            return new
            {
                Id,
                Nome
            };
        }

        //public string NomeTraduzido
        //{
        //    get => Resources.Tipos.Processamento.ResourceManager.GetString(((ProcessamentoTipoEnum)Id).ToString()) ?? Nome;
        //}
    }
}
