using Consulta.Func.Ofac.Dominio.Entidades;
using FluentValidation;

namespace Consulta.Func.Ofac.Dominio.Validacao;

public class SdnLoteValidacao: AbstractValidator<SdnLote>
{
    public SdnLoteValidacao()
    {
        RuleFor(obj => obj.Descricao)
            .NotEmpty().WithMessage("O campo precisa ser fornecido.");

        RuleFor(obj => obj.EhConsolidado)
            .NotEmpty().WithMessage("Esta lista não é consolidada.");

        RuleFor(obj => obj.Contagem)
            .NotEmpty().WithMessage("Valor inválido ou nulo.");

        RuleFor(obj => obj.DataPublicacao)
            .NotEmpty().WithMessage("Não há registros.");

    }
}