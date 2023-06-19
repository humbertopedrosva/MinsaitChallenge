using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Minsait.Challenge.Application.Services.MerchantReleases;
using Minsait.Challenge.Domain.DTOs;

namespace Minsait.Challenge.API.Controllers
{
    [ApiController]
    [Route("api/v1/merchant/{merchantId}/release")]
    [Authorize]
    public class MerchantReleaseController : ControllerBase
    {
        private readonly IMerchantReleaseFacade _releaseFacade;

        public MerchantReleaseController(IMerchantReleaseFacade merchantReleaseFacade)
        {
            _releaseFacade = merchantReleaseFacade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReleaseDTO[]))]
        public async Task<IActionResult> GetAll(Guid merchantId)
        {
            return Ok(await _releaseFacade.GetAllFromMerchant(merchantId));
        }

        [HttpGet("consolidate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsolidateReleasesDTO))]
        public async Task<IActionResult> GetConsolidateByPeriod(Guid merchantId, [FromQuery] DateTime beginDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _releaseFacade.GetConsolidateByPeriodFromMerchantAsync(merchantId, beginDate, endDate));
        }

        [HttpGet("{id}", Name = nameof(MerchantReleaseController) + nameof(Details))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReleaseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid id)
        {
            var release = await _releaseFacade.GetAsync(id);

            return release == null
                    ? NotFound()
                    : Ok(release);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReleaseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Guid merchantId, ReleaseDTO createReleaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            createReleaseDTO.MerchantId = merchantId;

            var release = await _releaseFacade.CreateAsync(createReleaseDTO);

            return CreatedAtRoute(nameof(MerchantReleaseController) + nameof(Details), new { merchantId, id = release.Id }, release);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Edit(Guid merchantId, Guid id, ReleaseDTO releaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            releaseDTO.MerchantId = merchantId;
            releaseDTO.Id = id;

            var release = await _releaseFacade.UpdateAsync(releaseDTO);

            return release == null
                    ? NotFound()
                    : NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _releaseFacade.DeleteAsync(id);

            return NoContent();
        }
    }
}
