using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;

public interface ISdnServico
{
    public int Adicionar(Sdn obj);

    public void AdicionarLista(List<Sdn> lista);
}