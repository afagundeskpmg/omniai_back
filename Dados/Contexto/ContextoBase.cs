using Dados.Configuracao;
using Dados.Seed;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Dados.Contexto
{
    public class ContextoBase : DbContext
    {
        private readonly IConfiguration _configuration;

        public ContextoBase()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _configuration = new ConfigurationBuilder()
                .SetBasePath(executableLocation)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public ContextoBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(_configuration.GetConnectionString("Contexto"));
        }

        #region DBSset
        public DbSet<Anexo> Anexo { get; set; }
        public DbSet<AnexoPagina> AnexoPagina { get; set; }
        public DbSet<AnexoArquivoTipo> AnexoArquivoTipo { get; set; }
        public DbSet<AnexoArquivoTipoArquivoFormato> AnexoArquivoTipoArquivoFormato { get; set; }
        public DbSet<AnexoTipo> AnexoTipo { get; set; }
        public DbSet<ArquivoFormato> ArquivoFormato { get; set; }
        public DbSet<ArquivoFormatoAssinatura> ArquivoFormatoAssinatura { get; set; }
        public DbSet<ArquivoFormatoAssinaturaByte> ArquivoFormatoAssinaturaByte { get; set; }
        public DbSet<Ambiente> Ambiente { get; set; }
        public DbSet<Claim> Claim { get; set; }
        public DbSet<ClaimGrupo> ClaimGrupo { get; set; }
        public DbSet<DocumentoTipo> DocumentoTipo { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Entidade> Entidade { get; set; }
        public DbSet<Interacao> Interacao { get; set; }
        public DbSet<InteracaoAmbiente> InteracaoAmbiente { get; set; }
        public DbSet<InteracaoTipo> InteracaoTipo { get; set; }
        public DbSet<InteracaoUsuario> InteracaoUsuario { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Papel> Papel { get; set; }
        public DbSet<PapelAcessivel> PapelAcessivel { get; set; }
        public DbSet<PapelClaim> PapelClaim { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public DbSet<PessoaTipo> PessoaTipo { get; set; }        
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<ProjetoAnexo> ProjetoAnexo { get; set; }
        public DbSet<Processamento> Processamento { get; set; }
         public DbSet<ProcessamentoNer> ProcessamentoNer { get; set; }
        public DbSet<ProcessamentoPergunta> ProcessamentoPergunta { get; set; }
        public DbSet<ProcessamentoEmail> ProcessamentoEmail { get; set; }        
        public DbSet<ProcessamentoLog> ProcessamentoLog { get; set; }
        public DbSet<ProcessamentoStatus> ProcessamentoStatus { get; set; }
        public DbSet<ProcessamentoTipo> ProcessamentoTipo { get; set; }
        public DbSet<ProcessamentoIndexer> ProcessamentoIndexer { get; set; }
        public DbSet<ProcessamentoAnexo> ProcessamentoAnexo { get; set; }
        public DbSet<PerguntaResposta> PerguntaResposta { get; set; }
        public DbSet<RelacaoClaimGrupo> RelacaoClaimGrupo { get; set; }
        public DbSet<TemaConfiguracao> TemaConfiguracao { get; set; }
        public DbSet<UsuarioClaim> UserClaim { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioLogin> UsuarioLogin { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuration
            modelBuilder.ApplyConfiguration(new AnexoArquivoTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new AnexoArquivoTipoArquivoFormatoConfiguracao());
            modelBuilder.ApplyConfiguration(new AnexoConfiguracao());
            modelBuilder.ApplyConfiguration(new AnexoPaginaConfiguracao());
            modelBuilder.ApplyConfiguration(new AnexoTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new ArquivoFormatoAssinaturaByteConfiguracao());
            modelBuilder.ApplyConfiguration(new ArquivoFormatoAssinaturaConfiguracao());
            modelBuilder.ApplyConfiguration(new ArquivoFormatoConfiguracao());
            modelBuilder.ApplyConfiguration(new AmbienteConfiguracao());
            modelBuilder.ApplyConfiguration(new ClaimConfiguracao());
            modelBuilder.ApplyConfiguration(new ClaimGrupoConfiguracao());
            modelBuilder.ApplyConfiguration(new DocumentoConfiguracao());
            modelBuilder.ApplyConfiguration(new DocumentoTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new EntidadeConfiguracao());
            modelBuilder.ApplyConfiguration(new EmailConfiguracao());
            modelBuilder.ApplyConfiguration(new EmailDestinatarioConfiguracao());
            modelBuilder.ApplyConfiguration(new EmailDestinatarioTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new EmailDestinatarioUsuarioConfiguracao());
            modelBuilder.ApplyConfiguration(new InteracaoConfiguracao());
            modelBuilder.ApplyConfiguration(new InteracaoAmbienteConfiguracao());
            modelBuilder.ApplyConfiguration(new InteracaoTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new InteracaoUsuarioConfiguracao());
            modelBuilder.ApplyConfiguration(new PaisConfiguracao());
            modelBuilder.ApplyConfiguration(new PessoaConfiguracao());
            modelBuilder.ApplyConfiguration(new PessoaJuridicaConfiguracao());
            modelBuilder.ApplyConfiguration(new PessoaTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new ProjetoConfiguracao());
            modelBuilder.ApplyConfiguration(new ProjetoAnexoConfiguracao());            
            modelBuilder.ApplyConfiguration(new ProcessamentoConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoEmailConfiguracao());            
            modelBuilder.ApplyConfiguration(new ProcessamentoStatusConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoTipoConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoIndexerConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoAnexoConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoNerConfiguracao());
            modelBuilder.ApplyConfiguration(new ProcessamentoPerguntaConfiguracao());
            modelBuilder.ApplyConfiguration(new PerguntaRespostaConfiguracao());            
            modelBuilder.ApplyConfiguration(new PapelConfiguracao());
            modelBuilder.ApplyConfiguration(new PapelClaimConfiguracao());
            modelBuilder.ApplyConfiguration(new PapelAcessivelConfiguracao());            
            modelBuilder.ApplyConfiguration(new RelacaoClaimGrupoConfiguracao());
            modelBuilder.ApplyConfiguration(new TemaConfiguracaoConfiguracao());
            modelBuilder.ApplyConfiguration(new UsuarioClaimConfiguracao());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
            modelBuilder.ApplyConfiguration(new UsuarioLoginConfiguracao());
            #endregion

            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 6);
        }

        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnexoTipo>().HasData(CargaAnexoTipo.GerarCarga());
            modelBuilder.Entity<ArquivoFormato>().HasData(CargaArquivoFormato.GerarCarga());
            modelBuilder.Entity<ArquivoFormatoAssinatura>().HasData(CargaArquivoFormatoAssinatura.GerarCarga());
            modelBuilder.Entity<ArquivoFormatoAssinaturaByte>().HasData(CargaArquivoFormatoAssinaturaByte.GerarCarga());
            modelBuilder.Entity<AnexoArquivoTipo>().HasData(CargaAnexoArquivoTipo.GerarCarga());
            modelBuilder.Entity<AnexoArquivoTipoArquivoFormato>().HasData(CargaAnexoArquivoTipoArquivoFormato.GerarCarga());
            modelBuilder.Entity<Claim>().HasData(CargaClaim.GerarCarga());
            modelBuilder.Entity<ClaimGrupo>().HasData(CargaClaimGrupo.GerarCarga());
            modelBuilder.Entity<Documento>().HasData(CargaDocumento.GerarCarga());
            modelBuilder.Entity<EmailDestinatarioTipo>().HasData(CargaEmailDestinatarioTipo.GerarCarga());
            modelBuilder.Entity<InteracaoTipo>().HasData(CargaInteracaoTipo.GerarCarga());
            modelBuilder.Entity<PessoaTipo>().HasData(CargaPessoaTipo.GerarCarga());
            modelBuilder.Entity<Pais>().HasData(CargaPais.GerarCarga());
            modelBuilder.Entity<Papel>().HasData(CargaPapel.GerarCarga());
            modelBuilder.Entity<PapelClaim>().HasData(CargaPapelClaim.GerarCarga());
            modelBuilder.Entity<PapelAcessivel>().HasData(CargaPapelAcessivel.GerarCarga());
            modelBuilder.Entity<ProcessamentoStatus>().HasData(CargaProcessamentoStaus.GerarCarga());
            modelBuilder.Entity<ProcessamentoTipo>().HasData(CargaProcessamentoTipo.GerarCarga());            
            modelBuilder.Entity<UsuarioClaim>().HasData(CargaUsuarioClaim.GerarCarga());

            GerarAdminBase(modelBuilder);
        }

        public void GerarAdminBase(ModelBuilder modelBuilder)
        {
            var emailUsuarioadmin = _configuration[ParametroAmbienteEnum.UsuarioAdmin.ToString()];

            var dataDefault = new DateTime(2023, 1, 1);
            var papeis = CargaPapel.GerarCarga().Cast<Papel>();
            var papelKPMG = papeis.First(p => p.Nome == "KPMG");
            var papelSistema = papeis.First(p => p.Nome == "Sistema");

            var usuarioSistema = new Usuario()
            {
                Id = "f60753d7-c5a7-4496-a360-c1a301d87763",
                Nome = "Usuário de integração",
                UserName = "integracao@watch.kpmg.com.br",
                IdentityId = "f60753d7-c5a7-4496-a360-c1a301d87763",
                PapelId = papelKPMG.Id,
            };
            modelBuilder.Entity<Usuario>().HasData(usuarioSistema);

            var usuarioKPMG = new Usuario()
            {
                Id = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4",
                Nome = "Administrador",
                UserName = "admin@kpmg.com",                
                IdentityId = "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4",
                PapelId = papelKPMG.Id,
            };
            usuarioKPMG.AtribuirInformacoesRegistroParaInsercao(usuarioSistema.Id);
            modelBuilder.Entity<Usuario>().HasData(usuarioKPMG);
            usuarioKPMG.CriadoEm = dataDefault;
            usuarioKPMG.UltimaAlteracaoEm = dataDefault;

            var empresaBase = new PessoaJuridica()
            {
                Id = 1,
                DadosCadastrais = "",
                DocumentoTipoId = (int)DocumentoEnum.CNPJ,
                CriadoEm = dataDefault,
                UltimaAlteracaoEm = dataDefault,
                Ativo = true,
                Documento = "11111111111111",
                Nome = "KPMG - Administrativo",
                PessoaTipoId = (int)PessoaTipoEnum.Juridica,
                PaisId = (int)PaisEnum.Brasil,
            };

            modelBuilder.Entity<PessoaJuridica>().HasData(empresaBase);

            var ambiente = new Ambiente(1, empresaBase.Id);           

            ambiente.AtribuirInformacoesRegistroParaInsercao(usuarioSistema.Id);
            modelBuilder.Entity<Ambiente>().HasData(ambiente);
            ambiente.CriadoEm = dataDefault;
            ambiente.UltimaAlteracaoEm = dataDefault;

            modelBuilder.Entity("UsuarioAmbiente")
                .HasData(
                    new { UsuarioId = usuarioSistema.Id, AmbienteId = ambiente.Id },
                    new { UsuarioId = usuarioKPMG.Id, AmbienteId = ambiente.Id }
                );
        }
    }
}
