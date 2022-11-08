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
    public class SdnLoteRepositorio : SqlServerExtensions, ISdnLoteRepositorio
    {
        protected DataBaseConfig _config;

        public SdnLoteRepositorio(IOptions<DataBaseConfig> options) : base(options)
        {
            _config = options.Value;
        }

        public int Adicionar(SdnLote sdnLote)
        {
            var ret = 0;
            var db = new ObjectDB("ofac.usp_SdnLote_Adicionar", OperationType.Insert, _config.Safe2PayDB);

             db.AddParameter("@Descricao", sdnLote.Descricao);
             db.AddParameter("@EhConsolidado", sdnLote.EhConsolidado);
             db.AddParameter("@DataPublicacao", sdnLote.DataPublicacao);
             db.AddParameter("@DataCriacao", DateTime.Now.ToBrazilTime());

             db.AddParameter("@Retorno", 0, ParameterDirection.Output);

             db.Execute();

            if (db.Result != null)
                ret = (int)db.Result;

            return ret;
        }

        public async Task<int> AdicionarLista(List<SdnLote> sdn)
        {
            var ret = 0;
            var db = new ObjectDB("ofac.usp_SdnLote_AdicionarEmLote", OperationType.Insert, _config.Safe2PayDB);

            await db.AddParameterSqlTableValue("@Data","ofac.SdnLote", sdn);


            await db.AddParameterSql("@Retorno", 0, ParameterDirection.Output);
            await db.ExecuteAsync();


            if (db.Result != null)
                ret = (int)db.Result;

            return ret;
        }

        public SdnLote BuscarPorDataPublicacao(SdnLote obj)
        {
            SdnLote ret = null;
            var db = new ObjectDB("ofac.usp_SdnLote_BuscarPorDataPublicacao", OperationType.Insert, _config.Safe2PayDB);

            db.AddParameter("@EhConsolidado", obj.EhConsolidado);
            db.AddParameter("@DataPublicacao", obj.DataPublicacao);

            db.Execute();

            if (db.Result != null)
            {
                var dt = (DataTable)db.Result;
                if (dt.Rows.Count > 0)
                {
                    ret = map(dt.Rows[0]);
                }
            }
            return ret;
        }

        /// <summary>
        /// Faz o mapeamento do objeto.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static SdnLote map(DataRow row)
        {
            SdnLote result = new(
                row.Get<string>("DataPublicacao"),
                row.Get<string>("Contagem"),
                row.Get<string>("Descricao"),
                row.Get<bool>("EhConsolidado")
              );

            return result;
        }
    }
}