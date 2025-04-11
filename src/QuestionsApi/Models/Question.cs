using System.ComponentModel.DataAnnotations;

namespace QuestionsApi.Models;

public class Question
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Question text is required")]
    [MinLength(10, ErrorMessage = "Question text must be at least 10 characters")]
    [MaxLength(250, ErrorMessage = "Question text must be between 10 and 250 characters")]
    public string Text { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}