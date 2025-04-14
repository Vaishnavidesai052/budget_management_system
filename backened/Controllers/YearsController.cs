using BudgetManagementSystemNew.Services;
using BudgetManagementSystemNew.Model;
using Microsoft.AspNetCore.Mvc;


namespace BudgetManagementSystemNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearController : ControllerBase
    {
        private readonly YearService _service;

        public YearController(YearService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllYears()
        {
            var years = _service.GetAllYears();
            if (years == null || years.Count == 0)
            {
                return NotFound("No years found.");
            }
            return Ok(years);
        }
    }
}