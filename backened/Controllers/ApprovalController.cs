
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/approval")]
[ApiController]
public class ApprovalController : ControllerBase
{
    private readonly ApprovalRepository _repository;

    public ApprovalController(IConfiguration configuration)
    {
        _repository = new ApprovalRepository(configuration.GetConnectionString("DefaultConnection"));
    }

    // GET: api/approval
    [HttpGet]
    public ActionResult<IEnumerable<ApprovalResponse>> GetApprovals()
    {
        var approvals = _repository.GetApprovals();
        if (approvals == null || !approvals.Any())
        {
            return NotFound("No approval records found.");
        }
        return Ok(approvals);
    }

    // GET: api/approval/5
    [HttpGet("{id}")]
    public ActionResult<ApprovalResponse> GetApprovalById(int id)
    {
        var approval = _repository.GetApprovalById(id);
        if (approval == null)
        {
            return NotFound($"Approval request with ID {id} not found.");
        }
        return Ok(approval);
    }

    // POST: api/approval
    [HttpPost]
    public ActionResult CreateApproval([FromBody] Approval approval)
    {
        if (approval == null)
        {
            return BadRequest("Approval data is invalid.");
        }

        _repository.CreateApproval(approval);
        return CreatedAtAction(nameof(GetApprovalById), new { id = approval.Req_Id }, approval);
    }

    // PUT: api/approval/5
    [HttpPut("{id}")]
    public ActionResult UpdateApproval(int id, [FromBody] Approval approval)
    {
        var existingApproval = _repository.GetApprovalById(id);
        if (existingApproval == null)
        {
            return NotFound($"Approval request with ID {id} not found.");
        }

        approval.Req_Id = id;
        _repository.UpdateApproval(approval);
        return NoContent();
    }

    // DELETE: api/approval/5
    [HttpDelete("{id}")]
    public ActionResult DeleteApproval(int id)
    {
        var existingApproval = _repository.GetApprovalById(id);
        if (existingApproval == null)
        {
            return NotFound($"Approval request with ID {id} not found.");
        }

        _repository.DeleteApproval(id);
        return NoContent();
    }

    // GET: api/approval/year/{yearId}
    [HttpGet("year/{yearId}")]
    public ActionResult<string> GetYearById(int yearId)
    {
        var year = _repository.GetYearById(yearId);
        if (year == null)
        {
            return NotFound($"Year with ID {yearId} not found.");
        }
        return Ok(year);
    }
}
