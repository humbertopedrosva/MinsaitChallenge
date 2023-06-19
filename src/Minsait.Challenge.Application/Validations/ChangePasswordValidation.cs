using FluentValidation;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Application.Validations
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidation(IMerchantSearcher merchantSearcher, IPasswordHasher passwordHasher)
        {
            RuleFor(x => x)
                .MustAsync(async (dto, _) => (await merchantSearcher.GetForLoginAsync(dto.Email, passwordHasher.HashPassword(dto.OldPassword))) != null)
                .WithMessage("Email or password not found");
        }
    }
}
