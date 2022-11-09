using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces
{
    public interface ISdnLoteRepositorio
    {
        public int Adicionar(SdnLote obj);
        public SdnLote BuscarPorDataPublicacao(SdnLote obj);
    }
}