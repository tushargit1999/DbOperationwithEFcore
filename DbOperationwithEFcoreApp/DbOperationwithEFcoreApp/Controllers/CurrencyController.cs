using DbOperationwithEFcoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationwithEFcoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CurrencyControler(AppDbContext _appDbcontext) : ControllerBase
    {
        [HttpGet("Added")]
        public async Task<IActionResult> GetCurrencies()
        {
            //var result = await _appDbcontext.CurrencyTypes.ToListAsync();
            //return Ok(result);

              var result = await (from CurrencyType in _appDbcontext.CurrencyTypes select CurrencyType).AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetcurrencyAsync([FromRoute] int id)
        {
            var result = await _appDbcontext.CurrencyTypes.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrencyByID([FromBody] List<int> ids )
        {
            //var ids = new List<int> { 1, 3,2};//if we wont Hard core code data used this // Just Remove Parameteris value
            var result = await _appDbcontext.CurrencyTypes.Where(x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);

        }

    }
}
