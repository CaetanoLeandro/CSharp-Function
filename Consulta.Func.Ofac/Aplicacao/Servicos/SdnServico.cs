using Consulta.Func.Ofac.Aplicacao.Config;
using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces;
using Microsoft.Extensions.Options;

namespace Consulta.Func.Ofac.Aplicacao.Servicos
{
    public class SdnServico : ServicoBase, ISdnServico
    {
        private readonly ISdnRepositorio _dsnRepositorio;

        private readonly AppConfig _config;

        public SdnServico(ISdnRepositorio sdnRepositorio, IOptions<AppConfig> options)
        {
            _dsnRepositorio = sdnRepositorio;
            _config = options.Value;
        }

        public int Adicionar(Sdn obj)
        {
            obj.Validar();

            int idLista = _dsnRepositorio.Adicionar(obj);

            return idLista;
        }    
        public async Task <bool> AdicionarLote(List<Sdn> lista)
        {
           // lista.Validar();

            bool idLista = await _dsnRepositorio.AdicionarLote(lista);

            return idLista;
        }


        public void AdicionarLista(List<Sdn> lista)
        {
            lista.ForEach(listaOfac =>
            {
                try
                {
                    Adicionar(listaOfac);
                }
                catch (Exception)
                {
                }
            });
        }
    }
}