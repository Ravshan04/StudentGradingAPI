using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
namespace StudentGradingAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(50)]
        public string Group { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
