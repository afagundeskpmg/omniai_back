using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Dominio.Entidades;

namespace Web.Providers
{
    public class SessionManager
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly Dictionary<string, CookieTemp> _sessionDictionary = new Dictionary<string, CookieTemp>();

        private static SessionManager _instance;
        public static SessionManager Instance => _instance ?? (_instance = new SessionManager());
        public void AddSession(string userId, string sessionId)
        {
            if (_sessionDictionary.ContainsKey(userId))
            {
                _sessionDictionary[userId] = new CookieTemp(sessionId);
            }
            else
            {
                _sessionDictionary.Add(userId, new CookieTemp(sessionId));
            }
        }
        public void RemoveSession(string userId)
        {
            if (_sessionDictionary.ContainsKey(userId))
            {
                _sessionDictionary[userId].active = false;
            }
        }
        public void RemoveSessionBySessionId(string sessionId)
        {

            if (_sessionDictionary.Any(x => x.Value.sessionId == sessionId && x.Value.active))
            {
                _sessionDictionary.FirstOrDefault(x => x.Value.sessionId == sessionId && x.Value.active).Value.active = false;
            }
        }
        public bool IsSessionActive(string userId, string sessionId)
        {
            if (_sessionDictionary.TryGetValue(userId, out var cookie))
            {
                return cookie.sessionId != sessionId && cookie.active;
            }

            return false;
        }
        public CookieTemp SelectSession(string userId, string sessionId)
        {
            if (_sessionDictionary.TryGetValue(userId, out var cookie))
            {
                return cookie;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateUserAuthLog(string email, string ip)
        {
            using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
            {
                var usuario = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == email);
                if (usuario != null)
                {
                    usuario.UltimoAcessoEm = DateTime.Now;
                    usuario.UltimoAcessoIP = ip;

                    if (usuario.Interacoes == null)
                        usuario.Interacoes = new List<InteracaoUsuario>();

                    usuario.Interacoes.Add(new InteracaoUsuario(usuario.Id, "Login realizado com sucesso", ip));//Traduzir

                     _aplicacao.Usuario.Atualizar(usuario);
                    _aplicacao.SaveChanges();
                }
            }
        }
        public static Resultado<List<UsuarioClaim>> SelectUserClaims(string email)
        {
            var resultado = new Resultado<List<UsuarioClaim>>();
            using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
            {
                var usuario = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == email);
                if (usuario != null)
                {
                    resultado.Retorno = usuario.Claims.ToList();
                }
            }
            resultado.Sucesso = true;

            return resultado;
        }

        public static Resultado<object> UserIsValid(string email)
        {
            var resultado = new Resultado<object>();

            using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
            {
                var usuario = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == email);

                if (usuario == null || (usuario != null && usuario.IdentityId == null))
                    resultado.Mensagem = "Usuário sem acesso a plataforma";//Traduzir
                else if (usuario != null && usuario.Excluido)
                    resultado.Mensagem = "Usuário bloqueado";//Traduzir
                else if (usuario != null && (usuario.Claims == null || (usuario.Claims != null && !usuario.Claims.Any())))
                    resultado.Mensagem = "Usuário sem permissão";//Traduzir
                else
                    resultado.Sucesso = true;
            }

            return resultado;
        }
        public static Resultado<object> RegisterAttempts(string email)
        {
            var resultado = new Resultado<object>();

            using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
            {
                var usuario = _aplicacao.Usuario.SelecionarFirstOrDefault(x => x.UserName == email);

                if (usuario != null)
                {
                    usuario.AccessFailedCount += 1;

                    if (usuario.AccessFailedCount == 3)
                    {
                        usuario.Excluido = true;
                        resultado.Mensagem = "Usuário bloqueado por multiplas tentativas de acesso, contate um administrador para realizar o desbloqueio";
                    }                   
                }

                if (string.IsNullOrEmpty(resultado.Mensagem))
                    resultado.Sucesso = true;              
            }

            return resultado;
        }

        public class CookieTemp
        {
            public CookieTemp(string session)
            {
                sessionId = session;
                createdIn = DateTime.Now;
                active = true;
            }
            public string sessionId { get; set; }
            public DateTime createdIn { get; set; }
            public bool active { get; set; }
        }
    }
}