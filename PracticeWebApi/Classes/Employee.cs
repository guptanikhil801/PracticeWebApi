using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticeWebApi.Classes
{
    public class Employee
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public int Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [NotMapped]
        public Address Address { get; set; }
    }
}