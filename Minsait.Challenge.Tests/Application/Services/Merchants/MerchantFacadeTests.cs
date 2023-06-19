using Minsait.Challenge.Application.Services.MerchantReleases;
using Minsait.Challenge.Application.Services.Merchants;
using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Tests.Application.Services.Merchants
{
    public class MerchantFacadeTests
    {
        private readonly IMerchantCreator _merchantCreatorMock;
        private readonly IMerchantUpdater _merchantUpdaterMock;
        private readonly IMerchantRemover _merchantRemoverMock;
        private readonly IMerchantSearcher _merchantSearcherMock;
        private readonly IMerchantReleaseFacade _merchantReleaseFacade;
        private readonly IPasswordHasher _passwordHasher;
        private readonly Fixture _fixture;
        private readonly MerchantFacade _merchantFacade;

        public MerchantFacadeTests()
        {
            _merchantCreatorMock = Substitute.For<IMerchantCreator>();
            _merchantUpdaterMock = Substitute.For<IMerchantUpdater>();
            _merchantRemoverMock = Substitute.For<IMerchantRemover>();
            _merchantSearcherMock = Substitute.For<IMerchantSearcher>();
            _merchantReleaseFacade = Substitute.For<IMerchantReleaseFacade>();
            _passwordHasher = Substitute.For<IPasswordHasher>();

            _fixture = FixtureHelper.CreateFixture();

            _merchantFacade = new MerchantFacade(_merchantCreatorMock, _merchantUpdaterMock, _merchantRemoverMock, _merchantSearcherMock, _merchantReleaseFacade, _passwordHasher);
        }

        [Fact]
        public async Task Should_Call_Creator_Service_When_Creating_Merchant()
        {
            var dto = _fixture.Create<MerchantDTO>();
            var expected = _fixture.Create<Merchant>();

            _merchantCreatorMock.CreateAsync(dto).Returns(expected);

            var act = await _merchantFacade.CreateAsync(dto);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _merchantCreatorMock.Received(1).CreateAsync(dto);
        }

        [Fact]
        public async Task Should_Call_Updater_Service_When_Updating_Merchant()
        {
            var dto = _fixture.Create<MerchantDTO>();
            var expected = _fixture.Create<Merchant>();

            _merchantUpdaterMock.UpdateAsync(dto).Returns(expected);

            var act = await _merchantFacade.UpdateAsync(dto);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _merchantUpdaterMock.Received(1).UpdateAsync(dto);
        }

        [Fact]
        public async Task Should_Remove_All_Releases_From_Merchant_And_Call_Remover_When_Removing_Merchant()
        {
            var id = _fixture.Create<Guid>();

            await _merchantFacade.DeleteAsync(id);

            await _merchantRemoverMock.Received(1).DeleteAsync(id);
            await _merchantReleaseFacade.Received(1).DeleteAllFromMerchant(id);
        }

        [Fact]
        public async Task Should_Call_Searcher_When_Getting_All_Merchants()
        {
            var merchants = _fixture.CreateMany<Merchant>();

            _merchantSearcherMock.GetAsync().Returns(merchants);

            var act = await _merchantFacade.GetAsync();

            act.Should().BeEquivalentTo(merchants, opt => opt.ExcludingMissingMembers());
            await _merchantSearcherMock.Received(1).GetAsync();
        }

        [Fact]
        public async Task Should_Call_Searcher_When_Getting_A_Specific_Merchant()
        {
            var expected = _fixture.Create<Merchant>();

            _merchantSearcherMock.GetAsync(expected.Id).Returns(expected);

            var act = await _merchantFacade.GetAsync(expected.Id);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _merchantSearcherMock.Received(1).GetAsync(expected.Id);
        }

        [Fact]
        public async Task Should_Update_Merchant_Password()
        {
            var merchant = _fixture.Create<Merchant>();
            var changePasswordDTO = _fixture.Create<ChangePasswordDTO>();
            changePasswordDTO.Email = merchant.Email;

            var oldPassHash = changePasswordDTO.OldPassword + "hash";
            var expectated = changePasswordDTO.NewPassword + "hash";

            _passwordHasher.HashPassword(changePasswordDTO.OldPassword).Returns(oldPassHash);
            _passwordHasher.HashPassword(changePasswordDTO.NewPassword).Returns(expectated);

            _merchantSearcherMock.GetForLoginAsync(changePasswordDTO.Email, oldPassHash).Returns(merchant);

            await _merchantFacade.ChangePasswordAsync(changePasswordDTO);

            merchant.PasswordHash.Should().Be(expectated);
        }

        [Fact]
        public async Task Should_Not_Find_Merchant_To_Change_Password()
        {
            var merchant = _fixture.Create<Merchant>();
            var changePasswordDTO = _fixture.Create<ChangePasswordDTO>();

            var expectated = merchant.PasswordHash;

            await _merchantFacade.ChangePasswordAsync(changePasswordDTO);

            merchant.PasswordHash.Should().Be(expectated);
        }

        [Fact]
        public async Task Should_Create_Merchant_Password()
        {
            var merchant = _fixture.Create<Merchant>();
            var changePasswordDTO = _fixture.Create<CreatePasswordDTO>();
            changePasswordDTO.Email = merchant.Email;

            var expectated = changePasswordDTO.Password + "hash";

            _passwordHasher.HashPassword(changePasswordDTO.Password).Returns(expectated);

            _merchantSearcherMock.GetForLoginAsync(changePasswordDTO.Email, string.Empty).Returns(merchant);

            await _merchantFacade.CreatePasswordAsync(changePasswordDTO);

            merchant.PasswordHash.Should().Be(expectated);
        }

        [Fact]
        public async Task Should_Not_Create_Merchant_Password()
        {
            var merchant = _fixture.Create<Merchant>();
            var changePasswordDTO = _fixture.Create<CreatePasswordDTO>();

            var expectated = merchant.PasswordHash;

            await _merchantFacade.CreatePasswordAsync(changePasswordDTO);

            merchant.PasswordHash.Should().Be(expectated);
        }
    }
}
