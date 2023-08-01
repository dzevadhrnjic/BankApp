using BankApp.Analytics.Models;
using BankApp.Analytics.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Analytics.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<Analytic>> AnalitycsList(int id, string type = "days")
        {
            try
            {
                var result = _analyticsService.Analytics(id, type);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}