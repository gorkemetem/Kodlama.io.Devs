using Application.Features.Languages.Dtos;
using FluentValidation;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdatedLanguageDto>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(l => l.Name).NotEmpty();
        }
    }
}
