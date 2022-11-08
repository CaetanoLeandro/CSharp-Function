using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consulta.Func.Ofac.Dominio.Entidades
{
    internal class SdnTabela
    {
        public string NomeSdn { get; set; }
        public string IdSdnExterno { get; set; }
        public int IdSdnLote { get; set; }
        public string TipoSdn { get; set; }
    }
}
