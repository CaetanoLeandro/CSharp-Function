using Consulta.Func.Ofac.Aplicacao.Config;
using Consulta.Func.Ofac.Aplicacao.Servicos;
using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Consulta.Func.Ofac.Dominio.Notificador;
using Consulta.Func.Ofac.Dominio.Notificador.Interfaces;
using Consulta.Func.Ofac.Infra.BancoDados;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consulta.Func.Ofac.Aplicacao
{
    public static class Configuracao
    {
        public static void RegistrarServicos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISdnServico, SdnServico>();
            services.AddScoped<ISdnLoteServico, SdnLoteServico>();
            services.AddScoped<INotificador, Notificador>();
            services.Configure<AppConfig>(configuration.GetSection(key: nameof(AppConfig)));

            services.RegistrarRepositorios(configuration);
        }
    }
}
