using System.Collections.Generic;
using System.Linq;

namespace Consulta.Func.Ofac.Dominio.Exception
{
    public class NegocioException : System.Exception
    {
        public List<FalhaNegocioException> Errors { get; private set; } = new List<FalhaNegocioException>();

        public NegocioException()
        {
            this.Errors = new List<FalhaNegocioException>();
        }

        public NegocioException(string message, string errorName = "ValidationError") : base(message)
        {
            this.Errors.Add(new FalhaNegocioException()
            {
                ErrorMessage = message,
                ErrorName = errorName
            });
        }

        public NegocioException(string message, System.Exception innerException) : base(message, innerException)
        {
            this.Errors.Add(new FalhaNegocioException()
            {
                ErrorMessage = message,
                ErrorName = "InnerExceptionError"
            });
        }

        public void AddError(FalhaNegocioException error)
        {
            this.Errors.Add(error);
        }

        public void ValidateAndThrow()
        {
            if (this.Errors.Any())
                throw this;
        }
    }
}
