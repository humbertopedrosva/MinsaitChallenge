using FluentValidation;
using Microsoft.AspNetCore.Http;
using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Application.Validations
{
    public class ReleaseValidation : AbstractValidator<ReleaseDTO>
    {
        public ReleaseValidation
        (
            IHttpContextAccessor httpContextAccessor,
            IMerchantReleaseSearcher merchantReleaseSearcher
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

            RuleFor(x => x.Date).Empty();

            RuleFor(x => x.Value)
                .NotNull()
                .NotEmpty();


            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.MerchantId)
                .NotEmpty()
                .DependentRules(() => RuleFor(x => x.MerchantId)
                                        .MustAsync(async (merchantId, _) => await merchantReleaseSearcher.GetAsync(merchantId) != null)
                                        .WithMessage("Merchant not found")
            );
        }
    }
}
