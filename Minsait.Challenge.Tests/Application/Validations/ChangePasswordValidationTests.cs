using Minsait.Challenge.Application.Validations;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Tests.Application.Validations
{
    public class ChangePasswordValidationTests
    {
        private readonly IMerchantSearcher _merchantSearcher;
        private readonly IPasswordHasher _passwordHasher;
        private readonly Fixture _fixture;
        private readonly ChangePasswordValidation _validator;

        public ChangePasswordValidationTests()
        {
            _merchantSearcher = Substitute.For<IMerchantSearcher>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _fixture = FixtureHelper.CreateFixture();
            _validator = new ChangePasswordValidation(_merchantSearcher, _passwordHasher);
        }

        [Fact]
        public async Task Should_Be_Valid()
        {
            var merchant = _fixture.Create<Merchant>();
            var changePassDto = new ChangePasswordDTO { Email = merchant.Email, NewPassword = Guid.NewGuid().ToString(), OldPassword = merchant.PasswordHash };

            _passwordHasher.HashPassword(changePassDto.OldPassword).Returns("hash");

            _merchantSearcher.GetForLoginAsync(merchant.Email, "hash").Returns(merchant);

            var act = await _validator.ValidateAsync(changePassDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEmpty();
        }

        [Fact]
        public async Task Should_Validate_Email_Or_Password_Not_Found()
        {
            var changePassDto = _fixture.Create<ChangePasswordDTO>();

            var act = await _validator.ValidateAsync(changePassDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[] { "Email or password not found" });
        }
    }
}
