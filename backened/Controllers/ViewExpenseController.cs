using Microsoft.AspNetCore.Mvc;
using BudgetManagementSystemNew.Model;
using System.Collections.Generic;
using BudgetManagementSystemNew.Services;

namespace BudgetManagementSystemNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewExpenseController : ControllerBase
    {
        private readonly ViewExpenseService _service;

        public ViewExpenseController(ViewExpenseService service)
        {
            _service = service;
        }

        [HttpGet("{yearId}")]
        public IActionResult GetViewExpenses(int yearId)
        {
            var expenses = _service.GetViewExpenses(yearId);
            return Ok(expenses);
        }
    }
}