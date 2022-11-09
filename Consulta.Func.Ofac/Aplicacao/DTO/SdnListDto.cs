using System.Xml.Serialization;

namespace Consulta.Func.Ofac.Aplicacao.DTO
{
    [XmlRoot("sdnList")]
    public class SdnListDto
    {
        [XmlElement("sdnEntry", typeof(SdnEntryDto))]
        public List<SdnEntryDto> sdnEntry { get; set; }

        [XmlElement("publshInformation", typeof(PublshInformationDto))]
        public PublshInformationDto publshInformation { get; set; }
    }
}
