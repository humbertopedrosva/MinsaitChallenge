using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minsait.Challenge.Application.Services.Merchants;
using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.API.Controllers
{
    [ApiController]
    [Route("api/v1/merchant/{merchantId}/password")]
    public class MerchantPasswordController : ControllerBase
    {
        private readonly IMerchantFacade _merchantFacade;

        public MerchantPasswordController(IMerchantFacade merchantFacade)
        {
            _merchantFacade = merchantFacade;
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _merchantFacade.ChangePasswordAsync(changePasswordDTO);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreatePasswordAsync([FromBody] CreatePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _merchantFacade.CreatePasswordAsync(changePasswordDTO);

            return NoContent();
        }
    }
}
