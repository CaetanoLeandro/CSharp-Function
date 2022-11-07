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
    public class ListaOfacSdnLoteRepositorio : SqlServerExtensions, IListaOfacSdnLoteRepositorio
    {
        protected DataBaseConfig _config;

        public ListaOfacSdnLoteRepositorio(IOptions<DataBaseConfig> options) : base(options)
        {
            _config = options.Value;
        }

        public int BuscarPorLote(ListaOfacSdn obj)
        {
            var ret = 0;
            var db = new ObjectDB("", OperationType.Insert, _config.Safe2PayDB);
            
            db.AddParameter("@IdSdnExterno",obj.IdSdn);
            db.AddParameter("@IdSdnLote",obj.IdSdn);
            db.AddParameter("@Nome",obj.NomeSdn);
            db.AddParameter("@Tipo",obj.TipoSdn);
            db.AddParameter("@DataCriacao", DateTime.Now.ToBrazilTime());

            db.AddParameter("@Retorno", 0, ParameterDirection.Output);
            
            db.Execute();

            if (db.Result != null)
                ret = (int)db.Result;

            return ret;
        }
        
    }
}