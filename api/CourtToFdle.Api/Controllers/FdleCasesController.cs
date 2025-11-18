using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using CourtToFdle.Api.Data;
using CourtToFdle.Api.Models;

namespace CourtToFdle.Api.Controllers
{
    [ApiController]
    [Route("fdle/cases")]
    public class FdleCasesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public FdleCasesController(AppDbContext db)
        {
            _db = db;
        }

        // POST /fdle/cases/batch
        [HttpPost("batch")]
        public async Task<IActionResult> IngestBatch([FromBody] 
List<FdleCaseDto> cases)
        {
            if (cases == null || cases.Count == 0)
                return BadRequest("No cases provided.");

            foreach (var dto in cases)
            {
                var entity = new FdleCase
                {
                    ExternalCaseId = dto.ExternalCaseId,
                    SourceSystem = dto.SourceSystem,
                    DefendantFirstName = dto.Defendant.FirstName,
                    DefendantLastName = dto.Defendant.LastName,
                    DefendantDob = dto.Defendant.Dob,
                    ChargesJson = JsonSerializer.Serialize(dto.Charges),
                    Status = dto.Status,
                    LastUpdated = dto.LastUpdated,
                    County = dto.County
                };

                _db.FdleCases.Add(entity);
            }

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), null);
        }

        // GET /fdle/cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FdleCase>>> GetAll(
            [FromQuery] string? status,
            [FromQuery] string? county)
        {
            var query = _db.FdleCases.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(c => c.Status == status);

            if (!string.IsNullOrWhiteSpace(county))
                query = query.Where(c => c.County == county);

            return Ok(await query.ToListAsync());
        }

        // GET /fdle/cases/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FdleCase>> GetById(int id)
        {
            var entity = await _db.FdleCases.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }
    }
}

