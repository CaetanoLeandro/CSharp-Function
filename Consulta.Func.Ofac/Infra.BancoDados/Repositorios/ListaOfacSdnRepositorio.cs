using System.Data;
using Consulta.Func.Ofac.Dominio.Comum;
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Infra.BancoDados.Config;
using Consulta.Func.Ofac.Infra.BancoDados.Extensoes;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces;
using Consulta.Func.Ofac.Infra.BancoDados.Util;
using Microsoft.Extensions.Options;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios
{
    public class ListaOfacSdnRepositorio : SqlServerExtensions, IListaOfacSdnRepositorio
    {
        protected DataBaseConfig _config;

        public ListaOfacSdnRepositorio(IOptions<DataBaseConfig> options) : base(options)
        {
            _config = options.Value;
        }

        public int Adicionar(ListaOfacSdn obj)
        {
            var ret = 0;
            var db = new ObjectDB("", OperationType.Insert, _config.Safe2PayDB);

            db.AddParameter("@IdSdn", obj.IdSdn);
            db.AddParameter("@NomeSdn", obj.NomeSdn);
            db.AddParameter("@TipoSdn", obj.TipoSdn);
            db.AddParameter("@Program", obj.Program);
            db.AddParameter("@CreatedDate", DateTime.Now.ToBrazilTime());

            db.AddParameter("@Retorno", 0, ParameterDirection.Output);

            db.Execute();

            if (db.Result != null)
                ret = (int)db.Result;

            return ret;
        }
    }
}