using Minsait.Challenge.Application.Services.MerchantReleases;
using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsait.Challenge.Tests.Application.Services.MerchantReleases
{
    public class MerchantReleaseFacadeTests
    {
        private readonly IMerchantReleaseCreator _releaseCreatorMock;
        private readonly IMerchantReleaseUpdater _releaseUpdaterMock;
        private readonly IMerchantReleaseRemover _releaseRemoverMock;
        private readonly IMerchantReleaseSearcher _releaseSearcherMock;
        private readonly Fixture _fixture;
        private readonly MerchantReleaseFacade _releaseFacade;

        public MerchantReleaseFacadeTests()
        {
            _releaseCreatorMock = Substitute.For<IMerchantReleaseCreator>();
            _releaseUpdaterMock = Substitute.For<IMerchantReleaseUpdater>();
            _releaseRemoverMock = Substitute.For<IMerchantReleaseRemover>();
            _releaseSearcherMock = Substitute.For<IMerchantReleaseSearcher>();

            _fixture = FixtureHelper.CreateFixture();

            _releaseFacade = new MerchantReleaseFacade(_releaseCreatorMock, _releaseUpdaterMock, _releaseRemoverMock, _releaseSearcherMock);
        }

        [Fact]
        public async Task Should_Call_Creator_Service_When_Creating_Release()
        {
            var dto = _fixture.Create<ReleaseDTO>();
            var expected = _fixture.Create<Release>();

            _releaseCreatorMock.CreateAsync(dto).Returns(expected);

            var act = await _releaseFacade.CreateAsync(dto);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _releaseCreatorMock.Received(1).CreateAsync(dto);
        }

        [Fact]
        public async Task Should_Call_Updater_Service_When_Updating_Release()
        {
            var dto = _fixture.Create<ReleaseDTO>();
            var expected = _fixture.Create<Release>();

            _releaseUpdaterMock.UpdateAsync(dto).Returns(expected);

            var act = await _releaseFacade.UpdateAsync(dto);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _releaseUpdaterMock.Received(1).UpdateAsync(dto);
        }

        [Fact]
        public async Task Should_Remove_All_Releases_From_Release_And_Call_Remover_When_Removing_Release()
        {
            var id = _fixture.Create<Guid>();

            await _releaseFacade.DeleteAsync(id);

            await _releaseRemoverMock.Received(1).DeleteAsync(id);
        }

        [Fact]
        public async Task Should_Call_Searcher_When_Getting_All_Releases_From_Specific_Merchant()
        {
            var releases = _fixture.CreateMany<Release>();
            var employeeId = _fixture.Create<Guid>();

            _releaseSearcherMock.GetAllFromMerchantAsync(employeeId).Returns(releases);

            var act = await _releaseFacade.GetAllFromMerchant(employeeId);

            act.Should().BeEquivalentTo(releases, opt => opt.ExcludingMissingMembers());
            await _releaseSearcherMock.Received(1).GetAllFromMerchantAsync(employeeId);
        }

        [Fact]
        public async Task Should_Call_Searcher_When_Getting_A_Specific_Release()
        {
            var expected = _fixture.Create<Release>();

            _releaseSearcherMock.GetAsync(expected.Id).Returns(expected);

            var act = await _releaseFacade.GetAsync(expected.Id);

            act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            await _releaseSearcherMock.Received(1).GetAsync(expected.Id);
        }
    }
}
