using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class EmailAplicacao : BaseAplicacao<Email>, IEmailAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;      
        private readonly IProcessamentoAplicacao _processamentoAplicacao;
        public EmailAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao,IProcessamentoAplicacao processamentoAplicacao) : base(repositorio, configuracao)
        {
             _repositorio = repositorio;  
            _processamentoAplicacao = processamentoAplicacao;
        }     

        public Resultado<object> SalvarEmailParaUsuarios(List<Usuario> destinatarios, string assunto, string corpo, string usuarioId, ICollection<Anexo> Anexos)
        {
            var resultado = new Resultado<object>();

            if (string.IsNullOrEmpty(corpo))
                resultado.Mensagem = "O corpo não pode ser vazio";
            if (destinatarios == null || !destinatarios.Any(u => !u.Excluido))
                resultado.Mensagem = "Destinatário não encontrado";
            else
            {
                var limiteUsuarios = int.Parse(SelecionarParametro(ParametroAmbienteEnum.LimiteUsuariosEnvioEmail));
                for (int i = 0; i <= destinatarios.Count / limiteUsuarios; i++)
                {
                    Email email = GerarEmail(assunto, corpo, usuarioId, Anexos);
                    email.EmailsDestinatarios = new List<EmailDestinatario>();
                    foreach (var destinatario in destinatarios.Where(u => !u.Excluido).Skip(i * limiteUsuarios).Take(limiteUsuarios))
                        email.EmailsDestinatarios.Add(new EmailDestinatarioUsuario() { Usuario = destinatario });

                    Inserir(email);
                }
                resultado.Sucesso = true;
            }

            return resultado;
        }
        public Email GerarEmail(string assunto, string corpo, string usuarioId, ICollection<Anexo> Anexos)
        {
            var email = new Email(usuarioId)
            {
                RemetenteEmail = SelecionarParametro(ParametroAmbienteEnum.MailRelayFrom),
                Assunto = assunto,
                CorpoAnexo = new Anexo()
                {
                    AnexoTipoId = (int)AnexoTipoEnum.EmailCorpo,
                    AnexoArquivoTipoId = (int)AnexoArquivoTipoEnum.PaginaHTML,
                    AnexoArquivoTipo = _repositorio.AnexoArquivoTipo.SelecionarPorId((int)AnexoArquivoTipoEnum.PaginaHTML),
                    ArquivoFormatoId = (int)ArquivoFormatoEnum.HTML,
                    ArquivoFormato = _repositorio.ArquivoFormato.SelecionarPorId((int)ArquivoFormatoEnum.HTML),
                    NomeArquivoOriginal = "Email.html",
                    NomeArquivoAlterado = Guid.NewGuid() + ".html",
                    BlobContainerName = SelecionarParametro(ParametroAmbienteEnum.BlobContainerNameAnexos)
                }
            };
            email.CorpoAnexo.CaminhoArquivoBlobStorage = Path.Combine(DeterminarPastaAnexo(null, AnexoTipoEnum.EmailCorpo), email.CorpoAnexo.NomeArquivoAlterado);
            email.CorpoAnexo.AtribuirInformacoesRegistroParaInsercao(SelecionarParametro(ParametroAmbienteEnum.UsuarioSistemaId));

            var stream = GerarMemoryStreamString(corpo);

            var conn = SelecionarParametro(ParametroAmbienteEnum.StorageConnection);
            EnviarArquivoBlobStorage(conn, email.CorpoAnexo.BlobContainerName, email.CorpoAnexo.CaminhoArquivoBlobStorage, stream, GerarTagsParaBlobPorAnexo(email.CorpoAnexo));

            var procTipo = _repositorio.ProcessamentoTipo.SelecionarPorId((int)ProcessamentoTipoEnum.Email);

            var proc = (Processamento)email.ProcessamentoEmail;
            _processamentoAplicacao.AtribuirQueueProcessamento(ref proc, conn, procTipo.QueueNome, JsonConvert.SerializeObject(new { Id = proc.Id }), 1);

            if (Anexos != null && Anexos.Count > 0)
            {
                email.Anexos = new List<Anexo>();
                foreach (var an in Anexos)
                {
                    Anexo anexo;
                    if (an.Id > 0)
                        anexo = _repositorio.Anexo.SelecionarPorId(an.Id);
                    else
                        anexo = an;
                    email.Anexos.Add(anexo);
                }
            }

            return email;
        }
    }
}
