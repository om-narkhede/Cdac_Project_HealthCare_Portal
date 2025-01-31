using HealthcarePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcarePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly HealthcareContext _context;

        // Constructor to inject HealthcareContext
        public MedicalRecordController(HealthcareContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            return await _context.MedicalRecords
                .Include(m => m.DIdNavigation)  // Include related Doctor
                .Include(m => m.PIdNavigation)  // Include related Patient
                .ToListAsync();
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.DIdNavigation)  // Include related Doctor
                .Include(m => m.PIdNavigation)  // Include related Patient
                .FirstOrDefaultAsync(m => m.RecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.RecordId }, medicalRecord);
        }

        // PUT: api/MedicalRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.RecordId)
            {
                return BadRequest();
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/MedicalRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(int id)
        {
            return _context.MedicalRecords.Any(e => e.RecordId == id);
        }
    }
}
