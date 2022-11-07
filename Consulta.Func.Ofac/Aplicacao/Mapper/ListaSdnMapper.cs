using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Aplicacao.Mapper
{
    public static class ListaSdnMapper
    {
        public static List<ListaOfacSdn> ConverterListaOfacSdnDtoParaListaOfacSdn(List<SdnEntryDto> list)
        {
            return list.Select(x => new ListaOfacSdn
            {
                NomeSdn = x.firstName,
                IdSdn = x.uid,
                TipoSdn = x.sdnType,
                Program = x.ProgramList,

            }).ToList();
        }

        public static ListaOfacSdn ConverterListaOfacSdnParaListaOfacSdnDto(SdnEntryDto obj)
        {
            return new ListaOfacSdn
            {
                NomeSdn = obj.firstName,
                IdSdn = obj.uid,
                TipoSdn = obj.sdnType,
                Program = obj.ProgramList

            };
        }
    }
}
