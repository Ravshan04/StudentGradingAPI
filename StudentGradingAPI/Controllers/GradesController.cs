using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradingApi.Data;

using StudentGradingAPI.Models;

namespace StudentGradingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<GradesController> _logger;

    public GradesController(AppDbContext context, ILogger<GradesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Grades
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
    {
        return await _context.Grades.ToListAsync();
    }

    // GET: api/Grades/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Grade>> GetGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);

        if (grade == null)
        {
            return NotFound();
        }

        return grade;
    }

    // GET: api/Grades/Student/5
    [HttpGet("Student/{studentId}")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGradesByStudent(int studentId)
    {
        return await _context.Grades
            .Where(g => g.StudentId == studentId)
            .ToListAsync();
    }

    // POST: api/Grades
    [HttpPost]
    public async Task<ActionResult<Grade>> CreateGrade(Grade grade)
    {
        // Tekshirish: student mavjudmi?
        var student = await _context.Students.FindAsync(grade.StudentId);
        if (student == null)
        {
            return BadRequest("Student topilmadi");
        }

        grade.GradeDate = DateTime.Now;
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
    }

    // PUT: api/Grades/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGrade(int id, Grade grade)
    {
        if (id != grade.Id)
        {
            return BadRequest();
        }

        _context.Entry(grade).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GradeExists(id))
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

    // DELETE: api/Grades/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null)
        {
            return NotFound();
        }

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GradeExists(int id)
    {
        return _context.Grades.Any(e => e.Id == id);
    }
}