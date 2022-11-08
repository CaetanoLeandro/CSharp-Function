using Consulta.Func.Ofac.Infra.BancoDados.Config;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consulta.Func.Ofac.Infra.BancoDados
{
    public static class Configuracao
    {
        public static void RegistrarRepositorios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISdnRepositorio, SdnRepositorio>();
            services.AddScoped<ISdnLoteRepositorio, SdnLoteRepositorio>();
            services.Configure<DataBaseConfig>(configuration.GetSection(key: nameof(DataBaseConfig)));
        }
    }
}
