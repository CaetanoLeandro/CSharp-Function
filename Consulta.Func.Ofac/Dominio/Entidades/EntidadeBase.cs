using Consulta.Func.Ofac.Dominio.Exception;
using FluentValidation.Results;

namespace Consulta.Func.Ofac.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public ValidationResult ValidationResult { get; set; }
        public NegocioException NegocioException { get; set; }

        public abstract bool EhValido();

        protected void Notificar(ValidationResult validationResult)
        {
            NegocioException = new NegocioException();

            foreach (var error in validationResult.Errors)
            {
                NegocioException.AddError(new FalhaNegocioException() { ErrorMessage = error.ErrorMessage });
            }

            NegocioException.ValidateAndThrow();
        }

        protected void Notificar(string mensagem)
        {
            NegocioException = new NegocioException();
            NegocioException.AddError(new FalhaNegocioException() { ErrorMessage = mensagem });
            NegocioException.ValidateAndThrow();
        }
    }
}
