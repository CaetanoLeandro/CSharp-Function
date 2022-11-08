using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;

public interface ISdnLoteServico
{
    public int Adicionar(SdnLote obj);

    public int BuscarPorLote(SdnLote obj);

    public Task <bool> AtualizarRegistrosDaBase(bool EhConsolidado);
}