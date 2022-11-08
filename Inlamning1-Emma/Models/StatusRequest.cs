using System.ComponentModel.DataAnnotations;

namespace Inlamning1_Emma.Models
{
    public class StatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
