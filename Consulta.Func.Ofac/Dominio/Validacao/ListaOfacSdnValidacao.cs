using Consulta.Func.Ofac.Dominio.Entidades;
using FluentValidation;

namespace Consulta.Func.Ofac.Dominio.Validacao;

public class ListaOfacSdnValidacao : AbstractValidator<ListaOfacSdn>
{
    public ListaOfacSdnValidacao()
    {
        RuleFor(obj => obj.IdSdn)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.NomeSdn)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.TipoSdn)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(obj => obj.Program)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

    }
}