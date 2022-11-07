using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;

public interface IListaOfacSdnServico
{
    public int Adicionar(ListaOfacSdn obj);

    // public ListaOfacSdn BuscarPorIdSdn(int idSdn);
    // public List<ListaOfacSdn> Listar();

    public bool AtualizarRegistrosDaBase();
}