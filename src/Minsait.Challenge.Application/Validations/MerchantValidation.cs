using FluentValidation;
using Microsoft.AspNetCore.Http;
using Minsait.Challenge.Domain.DTOs;

namespace Minsait.Challenge.Application.Validations
{
    public class MerchantValidation : AbstractValidator<MerchantDTO>
    {
        public MerchantValidation
        (
            IHttpContextAccessor httpContextAccessor
        )
        {
            if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Post.Method)
            {
                RuleFor(x => x.Id).Empty();
            }

            if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Put.Method)
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            }

            RuleFor(x => x.Surname)
                        .NotNull()
                        .NotEmpty();

            RuleFor(x => x.Name)
                        .NotNull()
                        .NotEmpty();

            RuleFor(x => x.Email)
                        .NotNull()
                        .NotEmpty()
                        .EmailAddress();
        }
    }
}
