using System.ComponentModel.DataAnnotations;

namespace Inlamning1_Emma.Models
{
    public class IssueResponse
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public StatusResponse Status { get; set; }
        public CustomerResponse Customer { get; set; }
    }
}
