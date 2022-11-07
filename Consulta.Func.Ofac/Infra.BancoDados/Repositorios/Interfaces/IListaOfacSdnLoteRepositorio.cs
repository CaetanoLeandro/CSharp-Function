using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces
{
    public interface IListaOfacSdnLoteRepositorio
    {
        public int BuscarPorLote(ListaOfacSdn obj);
    }
}