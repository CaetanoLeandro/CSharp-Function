
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Dominio.Exception;
using FluentValidation;
using FluentValidation.Results;

namespace Consulta.Func.Ofac.Aplicacao.Servicos
{
    public class ServicoBase
    {
        public NegocioException businessExpection { get; set; }

        protected void Notificar(ValidationResult validationResult)
        {
            businessExpection = new NegocioException();

            foreach (var error in validationResult.Errors)
            {
                businessExpection.AddError(new FalhaNegocioException() { ErrorMessage = error.ErrorMessage });
            }

            businessExpection.ValidateAndThrow();
        }

        protected void Notificar(string mensagem)
        {
            businessExpection = new NegocioException();
            businessExpection.AddError(new FalhaNegocioException() { ErrorMessage = mensagem });
            businessExpection.ValidateAndThrow();
        }

        protected void ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : EntidadeBase
        {
            var validator = validacao.Validate(entidade);
            Notificar(validator);
        }
    }
}
