
using System.Xml.Serialization;

namespace Consulta.Func.Ofac.Aplicacao.DTO
{
    
    public class SdnEntryDto
    {
        public string uid { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }
        public string sdnType { get; set; }

        [XmlElement("programList", typeof(ListaProgramDto))]
        public List<ListaProgramDto> ProgramList { get; set; }

    }
}
