namespace Dominio.Entidades
{
    public class Usuario : Registro
    {
        public Usuario()
        {
            Id = Guid.NewGuid().ToString();
            Claims = new List<UsuarioClaim>();
            Interacoes = new List<InteracaoUsuario>();
            Ambientes = new List<Ambiente>();
        }

        public string Id { get; set; }
        public string Nome { get; set; }        
        public string UserName { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }        
        public DateTime? UltimoAcessoEm { get; set; }
        public Ambiente Ambiente { get { return Ambientes?.First(); } }
        public string PapelId { get; set; }
        public virtual Papel Papel { get; set; }
        public virtual TemaConfiguracao TemaConfiguracao { get; set; }     
        public virtual ICollection<UsuarioClaim> Claims { get; set; }       
        public virtual ICollection<UsuarioLogin> Logins { get; set; }
        public virtual ICollection<InteracaoUsuario> Interacoes { get; set; }
        public virtual ICollection<Ambiente> Ambientes { get; set; }
        public virtual ICollection<EmailDestinatarioUsuario> Emails { get; set; }
        public bool Federado { get; set; }
        public bool EmailBoasVindasEnviado { get; set; }
        public bool IdentityIdGeradoLocalmente { get; set; }

        #region Properiedades de Registro
        public virtual ICollection<Processamento> ProcessamentosCriados { get; set; }
        public virtual ICollection<Processamento> ProcessamentosUltimasAlteracoes { get; set; }
        public virtual ICollection<Anexo> AnexosCriados { get; set; }
        public virtual ICollection<Anexo> AnexosUltimaAlteracao { get; set; }
        public virtual ICollection<Interacao> InteracoesCriadas { get; set; }
        public virtual ICollection<Interacao> InteracoesUltimasAlteracoes { get; set; }
        public virtual ICollection<Usuario> UsuariosCriados { get; set; }
        public virtual ICollection<Usuario> UsuariosUltimaAlteracao { get; set; }
        public virtual ICollection<Pessoa> PessoasCriadas { get; set; }
        public virtual ICollection<Pessoa> PessoasUltimaAlteracao { get; set; }
        public virtual ICollection<Projeto> ProjetosCriados { get; set; }
        public virtual ICollection<Projeto> ProjetosUltimaAlteracao { get; set; }
        public virtual ICollection<Ambiente> AmbientesCriados { get; set; }
        public virtual ICollection<Ambiente> AmbientesUltimaAlteracao { get; set; }
        public virtual ICollection<DocumentoTipo> DocumentosTipoCriados { get; set; }
        public virtual ICollection<DocumentoTipo> DocumentosTipoUltimaAlteracao { get; set; }
        public virtual ICollection<Entidade> EntidadesCriadas { get; set; }
        public virtual ICollection<Entidade> EntidadesUltimaAlteracao { get; set; }   
        public virtual ICollection<ProcessamentoIndexer> ProcessamentosIndexacaoLiberados { get; set; }
        #endregion

     
        public bool PossuiClaims(params string[] claims)
        {
            foreach (var claim in claims)
            {
                if (Claims?.Any(c => c.ClaimType == claim) == true)
                    return true;
            }
            return false;
        }

        public string IdentityId { get; set; }
        public string? UltimoAcessoIP { get; set; }

        public object SerializarParaListar()
        {           
            return new
            {
                Id,
                Nome,
                UserName,               
                Papel = Papel.SerializarParaListar(),                           
                UltimoAcessoEm = UltimoAcessoEm != null ? UltimoAcessoEm.Value.ToString("dd/MM/yy HH:mm") : "",
                Excluido
            };
        }       
        public bool PossuiClaim(string claim)
        {
            return this.Claims.Any(x => x.ClaimType == claim);
        }        
        public bool PertenceAoAmbiente(int ambienteId) => Ambientes.Any(e => e.Id == ambienteId);
    }
}
