using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Mapper
{
    public static class SdnMapper
    {
        public static List<Sdn> ConverterListaOfacSdnDtoParaListaOfacSdn(List<SdnEntryDto> list, int idSdnLote)
        {
            return list.Select(x => new Sdn
            {
                NomeSdn = x.firstName + " " + x.lastName,
                IdSdnExterno = x.uid,
                TipoSdn = x.sdnType,
                Program = x.ProgramList,
                IdSdnLote = idSdnLote

            }).ToList();
        }

        public static Sdn ConverterListaOfacSdnParaListaOfacSdnDto(SdnEntryDto obj,int idSdnLote)
        {
            return new Sdn
            {
                NomeSdn = obj.firstName + " " + obj.lastName,
                IdSdnExterno = obj.uid,
                TipoSdn = obj.sdnType,
                Program = obj.ProgramList,
                IdSdnLote = idSdnLote
            };
        }
    }
}
