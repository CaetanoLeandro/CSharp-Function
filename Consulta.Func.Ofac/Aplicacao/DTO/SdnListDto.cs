using Consulta.Func.Ofac.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Consulta.Func.Ofac.Aplicacao.DTO
{
    [XmlRoot("sdnList")]
    public class SdnListDto
    {
        [XmlElement("sdnEntry", typeof(SdnEntryDto))]
        public List<SdnEntryDto> sdnEntry { get; set; }
    }
}
