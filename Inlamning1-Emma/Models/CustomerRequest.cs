using System.ComponentModel.DataAnnotations;

namespace Inlamning1_Emma.Models
{
    public class CustomerRequest
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
