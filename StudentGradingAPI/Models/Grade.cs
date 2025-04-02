using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace StudentGradingAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Range(0, 100)]
        public int Score { get; set; }

        public DateTime GradeDate { get; set; } = DateTime.Now;

        public int StudentId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }
    }
}
