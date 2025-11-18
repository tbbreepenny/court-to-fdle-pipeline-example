using System.ComponentModel.DataAnnotations;

namespace CourtToFdle.Api.Models
{
    public class FdleCase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ExternalCaseId { get; set; } = string.Empty;

        [Required]
        public string SourceSystem { get; set; } = string.Empty;

        public string DefendantFirstName { get; set; } = string.Empty;
        public string DefendantLastName { get; set; } = string.Empty;
        public DateTime? DefendantDob { get; set; }

        public string ChargesJson { get; set; } = "[]";

        public string Status { get; set; } = "OPEN";
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string County { get; set; } = string.Empty;
    }
}

