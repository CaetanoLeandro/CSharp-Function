using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;

public interface ISdnLoteServico
{
    public int BuscarPorLote(SdnLote obj);
}