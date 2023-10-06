using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public PeopleDto People { get; set; }
        public int PeopleId { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public int Month { get; set; }
    }
}
