using System.ComponentModel.DataAnnotations;

namespace Inlamning1_Emma.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
