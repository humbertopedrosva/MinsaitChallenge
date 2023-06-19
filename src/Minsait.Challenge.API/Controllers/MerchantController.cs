using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minsait.Challenge.Application.Services.Merchants;
using Minsait.Challenge.Domain.DTOs;

namespace Minsait.Challenge.API.Controllers
{
    [ApiController]
    [Route("api/v1/merchant")]
    [Authorize]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantFacade _merchantFacade;

        public MerchantController(IMerchantFacade merchantFacade)
        {
            _merchantFacade= merchantFacade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantDTO[]))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _merchantFacade.GetAsync());
        }
        

        [HttpGet("{id}", Name = nameof(MerchantController) + nameof(Details))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _merchantFacade.GetAsync(id);

            return employee == null
                    ? NotFound()
                    : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MerchantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(MerchantDTO createEmployeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = await _merchantFacade.CreateAsync(createEmployeeDTO);

            return CreatedAtRoute(nameof(MerchantController) + nameof(Details), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Edit(Guid id, MerchantDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            employeeDTO.Id = id;
            await _merchantFacade.UpdateAsync(employeeDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _merchantFacade.DeleteAsync(id);
            return NoContent();
        }
    }
}
