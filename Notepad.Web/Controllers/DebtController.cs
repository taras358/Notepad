using Microsoft.AspNetCore.Mvc;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using System.IO;
using System.Threading.Tasks;

namespace Notepad.Web.Controllers
{
    [Route("api/[controller]")]
    public class DebtController : Controller
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody]CreateDebtRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _debtService.Create(request);
            return Ok();
        }
        [HttpPost("delete")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromBody]DeleteDebtRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _debtService.Delete(request);
            return Ok();
        }
        [HttpPatch("update")]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromBody]UpdateDebtRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _debtService.Update(request);
            return Ok();
        }
    }
}
