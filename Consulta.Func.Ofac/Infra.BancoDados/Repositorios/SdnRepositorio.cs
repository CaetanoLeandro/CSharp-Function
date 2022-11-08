using System.Data;
using Consulta.Func.Ofac.Dominio.Comum;
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Infra.BancoDados.Config;
using Consulta.Func.Ofac.Infra.BancoDados.Extensoes;
using Consulta.Func.Ofac.Infra.BancoDados.Util;
using Microsoft.Extensions.Options;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios
{
    public class SdnRepositorio : SqlServerExtensions, Interfaces.ISdnRepositorio
    {
        protected DataBaseConfig _config;

        public SdnRepositorio(IOptions<DataBaseConfig> options) : base(options)
        {
            _config = options.Value;
        }

        public int Adicionar(Sdn obj)
        {
            
                var ret = 0;
                var db = new ObjectDB("ofac.usp_Sdn_Adicionar", OperationType.Insert, _config.Safe2PayDB);

                db.AddParameter("@IdSdnExterno", obj.IdSdnExterno);
                db.AddParameter("@IdSdnLote", obj.IdSdnLote);
                db.AddParameter("@Nome", obj.NomeSdn);
                db.AddParameter("@Tipo", obj.TipoSdn);
                db.AddParameter("@DataCriacao", DateTime.Now.ToBrazilTime());

                db.AddParameter("@Retorno", 0, ParameterDirection.Output);

                db.Execute();

                if (db.Result != null)
                    ret = (int)db.Result;

                return ret;

            }
            public async Task<bool> AdicionarLote(List<Sdn> sdn)
            {
                var ret = false;
                var db = new ObjectDB("ofac.usp_Sdn_AdicionarEmLote", OperationType.Insert, _config.Safe2PayDB);

                await db.AddParameterSqlTableValue("@Data", "ofac.Sdn", sdn);


                await db.AddParameterSql("@Retorno", 0, ParameterDirection.Output);
                await db.ExecuteAsync();


                if (db.Result != null)
                    ret = (int)db.Result > 0;


                return ret;
            }
        }
    }