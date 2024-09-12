using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticeWebApi.Classes
{
    public class Address
    {
        [Key]
        [JsonIgnore]
        public int EmployeeID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}
