using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Dominio.Validacao;

namespace Consulta.Func.Ofac.Dominio.Entidades
{
    public class Sdn : EntidadeBase
    {
        public string NomeSdn { get; set; }
        public string IdSdnExterno { get; set; }
        public int IdSdnLote { get; set; }
        public string TipoSdn { get; set; }

        public List<ListaProgramDto> Program { get; set; }


        public Sdn() 
        {
        }

        public Sdn(string nomeSdn, string idSdnExterno, string tipoSdn, List<ListaProgramDto> program)
        {
            NomeSdn = nomeSdn;
            IdSdnExterno = idSdnExterno;
            TipoSdn = tipoSdn;
            Program = program;

            Validar();
        }

        public Sdn(string nomeSdn, string idSdnExterno, string tipoSdn, string programa)
        {
            throw new NotImplementedException();
        }

        public void Validar()
        {
            ValidationResult = new SdnValidacao().Validate(this);
            if (!ValidationResult.IsValid)
                Notificar(ValidationResult);
        }
        
        public sealed override bool EhValido()
        {
            ValidationResult = new SdnValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
