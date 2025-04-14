using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BudgetManagementSystemNew.Model;
using BudgetManagementSystemNew.Services;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddExpenseController : ControllerBase
    {
        private readonly AddExpenseService _service;

        public AddExpenseController(AddExpenseService service)
        {
            _service = service;
        }

        [HttpGet("GetExpenses")]
        public ActionResult<List<AddExpense>> GetExpenses([FromQuery] int year, [FromQuery] int month)
        {
            var expenses = _service.GetExpenses(year, month);
            return Ok(expenses);
        }

        // New PUT endpoint to update multiple ActualExpense
        [HttpPut("UpdateMultipleActualExpenses")]
        public ActionResult UpdateMultipleActualExpenses([FromBody] List<UpdateExpenseRequest> updateRequests)
        {
            bool success = _service.UpdateMultipleActualExpenses(updateRequests);

            if (success)
            {
                return Ok("Expenses updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update expenses.");
            }
        }
    }

    public class UpdateExpenseRequest
    {
        public int Id { get; set; }
        public decimal ActualExpense { get; set; }
    }
}
