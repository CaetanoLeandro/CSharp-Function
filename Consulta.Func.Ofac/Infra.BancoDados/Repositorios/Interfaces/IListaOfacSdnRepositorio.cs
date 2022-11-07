using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces
{
    public interface IListaOfacSdnRepositorio
    {
        public int Adicionar(ListaOfacSdn obj);

        // public ListaOfacSdn BuscarPorIdSdn(int IdSdn);
        // public List<ListaOfacSdn> Listar();
    }
}