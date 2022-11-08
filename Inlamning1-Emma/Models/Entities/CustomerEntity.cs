using System.ComponentModel.DataAnnotations;

namespace Inlamning1_Emma.Models.Entities
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<IssueEntity> Issues { get; set; }
    }
}
