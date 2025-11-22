using FluentValidation;
using volei_app.Features.Players.DTOs;

namespace volei_app.Features.Players.Validator;

public class ConfirmPlayerRequestValidator : AbstractValidator<ConfirmPlayerRequestDto>
{
    
    public ConfirmPlayerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do jogador é obrigatório.")
            .Length(2, 50).WithMessage("O nome não pode ter mais de 50 caracteres.");
    }   
    
}