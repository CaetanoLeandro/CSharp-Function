
using Consulta.Func.Ofac.Dominio.Validacao;

namespace Consulta.Func.Ofac.Dominio.Entidades
{
    public class SdnLote: EntidadeBase
    {
        public string DataPublicacao { get; set; }
        public string Contagem { get; set; }
        public string Descricao { get; set; }
        public bool EhConsolidado { get; set; }

        public SdnLote()
        {
        }

        public SdnLote(string dataPublicacao, string contagem, string descricao, bool ehConsolidado)
        {
            DataPublicacao = dataPublicacao;
            Contagem = contagem;
            Descricao = descricao;
            EhConsolidado = ehConsolidado;

            Validar();
        }

        public SdnLote(string DataPublicacao, string Contagem, string Descricao)
        {
            throw new NotImplementedException();
        }
        public void Validar()
        {
            ValidationResult = new SdnLoteValidacao().Validate(this);
            if (!ValidationResult.IsValid)
                Notificar(ValidationResult);
        }
        
        public sealed override bool EhValido()
        {
            ValidationResult = new SdnLoteValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}