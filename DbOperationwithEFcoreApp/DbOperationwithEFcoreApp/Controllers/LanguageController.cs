using DbOperationwithEFcoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationwithEFcoreApp.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;

        public LanguageController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            var result = await _appDbcontext.Languages.ToListAsync();
            // return Ok(result);
            //var result = from Language in _appDbcontext.Languages
            //             select Language;

            return Ok(result);

        }

    }
}
