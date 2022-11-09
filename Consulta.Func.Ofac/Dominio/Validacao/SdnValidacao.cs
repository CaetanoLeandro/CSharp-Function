using Consulta.Func.Ofac.Dominio.Entidades;
using FluentValidation;

namespace Consulta.Func.Ofac.Dominio.Validacao;

public class SdnValidacao : AbstractValidator<Sdn>
{
    public SdnValidacao()
    {
        RuleFor(obj => obj.IdSdnExterno)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.NomeSdn)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.TipoSdn)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.Program)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

    }
}