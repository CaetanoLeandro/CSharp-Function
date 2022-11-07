using Consulta.Func.Ofac.Infra.BancoDados.Config;
using Microsoft.Extensions.Options;

namespace Consulta.Func.Ofac.Infra.BancoDados.Util
{
    public abstract class SqlServerExtensions
    {
        protected DataBaseConfig _config;
        protected string _connectionString;
        protected string _connectionStringAuth;

        public SqlServerExtensions(IOptions<DataBaseConfig> options)
        {
            _config = options.Value;
            _connectionString = _config.Safe2PayDB;
        }
    }
}