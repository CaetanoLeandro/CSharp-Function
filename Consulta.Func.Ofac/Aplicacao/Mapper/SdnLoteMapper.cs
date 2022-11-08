using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Mapper;

public class SdnLoteMapper
{
    public static SdnLote ConverterListaOfacSdnParaListaOfacSdnDto(PublshInformationDto obj, string Descricao, 
        bool EhConsolidado )
    {
        return new SdnLote
        {
            DataPublicacao = obj.Publish_Date,
            Contagem = obj.Record_Count,
            Descricao = Descricao,
            EhConsolidado = EhConsolidado
        };
    }
}