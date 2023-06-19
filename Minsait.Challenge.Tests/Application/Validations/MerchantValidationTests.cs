using Microsoft.AspNetCore.Http;
using Minsait.Challenge.Application.Validations;
using Minsait.Challenge.Domain.DTOs;
using System.Globalization;

namespace Minsait.Challenge.Tests.Application.Validations
{
    public class MerchantValidationTests
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Fixture _fixture;

        private MerchantValidation _merchantValidation;

        public MerchantValidationTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            _fixture = FixtureHelper.CreateFixture();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

            CreateValidator();
        }

        private void CreateValidator()
        {
            _merchantValidation = new MerchantValidation(_httpContextAccessor);
        }

        [Fact]
        public async Task Should_Get_Default_Errors()
        {
            var merchantDto = new MerchantDTO();

            var act = await _merchantValidation.ValidateAsync(merchantDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Surname' must not be empty.",
            "'Surname' must not be empty.",
            "'Name' must not be empty.",
            "'Name' must not be empty.",
            "'Email' must not be empty.",
            "'Email' must not be empty.",
        });
        }

        [Fact]
        public async Task Should_Be_Totally_Valid()
        {
            var merchantDto = CreateAValidScenario();

            var act = await _merchantValidation.ValidateAsync(merchantDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEmpty();
        }

        [Fact]
        public async Task Should_Validade_Id_Empty_When_Using_Post_Method()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = HttpMethods.Post;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var merchantDto = CreateAValidScenario();

            CreateValidator();

            var act = await _merchantValidation.ValidateAsync(merchantDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Id' must be empty."
        });
        }

        [Fact]
        public async Task Should_Validade_Id_Not_Empty_When_Using_Put_Method()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = HttpMethods.Put;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var merchantDto = CreateAValidScenario();
            merchantDto.Id = Guid.Empty;
            CreateValidator();

            var act = await _merchantValidation.ValidateAsync(merchantDto);

            act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Id' must not be empty."
        });
        }

        private MerchantDTO CreateAValidScenario()
        {
            var merchantDto = _fixture.Create<MerchantDTO>();
            merchantDto.Email = "teste@teste.com";
            return merchantDto;
        }
    }
}

