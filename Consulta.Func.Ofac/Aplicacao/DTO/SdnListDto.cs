using System.Xml.Serialization;

namespace Consulta.Func.Ofac.Aplicacao.DTO
{
    [XmlRoot("sdnList")]
    public class SdnListDto
    {
        [XmlElement("sdnEntry", typeof(SdnEntryDto))]
        public List<SdnEntryDto> sdnEntry { get; set; }

        public PublshInformationDto PublshInformation { get; set; }
    }
}
