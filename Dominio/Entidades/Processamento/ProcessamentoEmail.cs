namespace Dominio.Entidades
{
    public class ProcessamentoEmail : Processamento
    {
        public ProcessamentoEmail(string usuarioId, Email email) : base(usuarioId, (int)ProcessamentoTipoEnum.Email)
        {
            Id = Guid.NewGuid().ToString();
            Email = email;
        }

        public ProcessamentoEmail()
        {
            Id = Guid.NewGuid().ToString();
        }

        public int EmailId { get; set; }
        public virtual Email Email { get; set; }

        public override object SerializarParaListar()
        {
            throw new NotImplementedException();
        }
    }
}
