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
            var question = await _context.Questions
                .OrderByDescending(q => q.DateAdded)
                .FirstOrDefaultAsync();
            
            if (question == null)
                return NotFound("Question not found");
            
            return Ok(question);
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