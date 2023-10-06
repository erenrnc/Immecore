using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class PeopleDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DepartmentDto Department { get; set; }
        public List<DepartmentDto> DepartmentList { get; set; }
        public int DepartmentId { get; set; }
        public PeopleDto()
        {
            DepartmentList = new List<DepartmentDto>();
            Department = new DepartmentDto();
        }
    }
}
