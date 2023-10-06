namespace Dominio.Entidades
{
    public class DatatableRetorno<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public virtual ICollection<T> data { get; set; }
    }
}
