namespace Dominio.Entidades
{
    public class PessoaJuridica : Pessoa
    {
        public PessoaJuridica() { }

        public PessoaJuridica(string documento, string nome, int paisId, string usuarioLogadoId) : base(documento, nome, paisId, usuarioLogadoId, (int)PessoaTipoEnum.Juridica)
        {
        }

        public string DadosCadastrais { get; set; }
        public virtual ICollection<Ambiente> Ambientes { get; set; }

        #region PropriedadesFormatadas   

        public virtual string CNPJFormatado
        {
            get
            {
                if (PaisId == (int)PaisEnum.Brasil)
                    return Convert.ToUInt64(Documento).ToString(@"00\.000\.000\/0000\-00");
                else
                    return Documento;
            }
        }

        public virtual string CNPJRaiz
        {
            get
            {
                return Documento.Substring(0, 8);
            }
        }

        public virtual string NomePersonalizado
        {
            get
            {
                return CNPJFormatado + " - " + (Nome.Count() > 18 ? Nome.Substring(0, 15) + "..." : Nome);
            }
        }             

        #endregion      
    }
}

