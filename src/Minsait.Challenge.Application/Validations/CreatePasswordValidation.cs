using FluentValidation;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Application.Validations
{
    public class CreatePasswordValidation : AbstractValidator<CreatePasswordDTO>
    {
        public CreatePasswordValidation(IMerchantSearcher merchantSearcher)
        {
            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync(async (dto, _) => (await merchantSearcher.GetForLoginAsync(dto.Email, string.Empty)) != null)
                .WithMessage("Password already created");
        }
    }
}
