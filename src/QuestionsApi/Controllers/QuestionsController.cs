using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionsApi.Models;
using QuestionsApi.Data;

namespace QuestionsApi.Controllers

{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("today")]
        public async Task<ActionResult<Question>> GetToday()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var todayQuestion = await _context.Questions
                .Where(q => q.UsedAsDaily == today)
                .FirstOrDefaultAsync();
            
            if (todayQuestion != null)
            {
                return Ok(todayQuestion);
            }
            
            var count = await _context.Questions.CountAsync();
            if (count == 0)
            {
                return NotFound("No questions found");
            }
            
            var random = new Random();
            var skip = random.Next(count);

            var randomQuestion = await _context.Questions
                .OrderBy(q => q.Id)
                .Skip(skip)
                .FirstAsync();
            
            randomQuestion.UsedAsDaily = today;
            await _context.SaveChangesAsync();
            return Ok(randomQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!question.Text.Trim().EndsWith("?"))
                return BadRequest("Question must end with a question mark");
            
            question.DateAdded = DateTime.UtcNow;
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetToday), new { id = question.Id }, question);
        }
    }
}