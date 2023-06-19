using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Domain.Merchants.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsait.Challenge.Tests.Domain.Merchants.Services
{
    public class MerchantCreatorTests
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly Fixture _fixture;

        private readonly MerchantCreator _merchantCreator;

        public MerchantCreatorTests()
        {
            _merchantRepository = Substitute.For<IMerchantRepository>();
            _fixture = FixtureHelper.CreateFixture();
            _merchantCreator = new MerchantCreator(_merchantRepository);
        }

        [Fact]
        public async Task Should_Call_Create_From_Repository_With_Created_Entity_Mapped()
        {
            Merchant? mappedEntity = null;
            var merchant = _fixture.Create<Merchant>();
            var merchantDto = _fixture.Create<MerchantDTO>();

            await _merchantRepository.CreateAsync(Arg.Do<Merchant>(x => mappedEntity = x));
            _merchantRepository.CreateAsync(Arg.Any<Merchant>()).Returns(merchant);

            var act = await _merchantCreator.CreateAsync(merchantDto);

            mappedEntity.Should().BeEquivalentTo(merchantDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());
            act.Should().Be(merchant);
        }

        [Fact]
        public async Task Should_Call_Create_From_Repository_With_Created_Entity_Mapped_But_Without_Leader()
        {
            Merchant? mappedEntity = null;
            var merchant = _fixture.Create<Merchant>();
            var merchantDto = _fixture.Create<MerchantDTO>();

            await _merchantRepository.CreateAsync(Arg.Do<Merchant>(x => mappedEntity = x));
            _merchantRepository.CreateAsync(Arg.Any<Merchant>()).Returns(merchant);

            var act = await _merchantCreator.CreateAsync(merchantDto);

            mappedEntity.Should().BeEquivalentTo(merchantDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());

            act.Should().Be(merchant);
        }
    }
}
