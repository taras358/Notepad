using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtorController : ControllerBase
    {
        private readonly IDeptorService _deptorService;

        public DebtorController(IDeptorService deptorService)
        {
            _deptorService = deptorService;
        }

        [HttpGet("find")]
        [ProducesResponseType(typeof(DebtorsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Find([FromQuery]string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest();
            }
            DebtorsResponse deptors = await _deptorService.FindByFullName(query);
            return Ok(deptors);
        }

        [HttpGet("getDebtor")]
        [ProducesResponseType(typeof(DebtorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGebtor([FromQuery]string debtorId)
        {
            if (string.IsNullOrWhiteSpace(debtorId))
            {
                return BadRequest();
            }
            DebtorResponse debtor = await _deptorService.GetById(debtorId);
            return Ok(debtor);
        }

        [HttpGet("getDebtors")]
        [ProducesResponseType(typeof(DebtorsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGebtors()
        {
            DebtorsResponse deptors = await _deptorService.GetAll();
            return Ok(deptors);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody]CreateDebtorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string id = await _deptorService.Create(request);
            return Ok(id);
        }

        [HttpPost("update")]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromBody]UpdateDebtorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _deptorService.Update(request);
            return Ok();
        }
    }
}
