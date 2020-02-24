using Microsoft.AspNetCore.Mvc;

namespace DepositSettlements.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        ///     Redirects to the swagger page.
        /// </summary>
        /// <returns>A 301 Moved Permanently response.</returns>
        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectPermanent("/swagger");
        }
    }
}