using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;

public interface IListaOfacSdnServico
{
    public int Adicionar(ListaOfacSdn obj);

    public bool AtualizarRegistrosDaBase();
}