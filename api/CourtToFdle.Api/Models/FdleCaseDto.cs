namespace CourtToFdle.Api.Models
{
    public class FdleCaseDto
    {
        public string ExternalCaseId { get; set; } = string.Empty;
        public string SourceSystem { get; set; } = "CountyCourt";
        public DefendantDto Defendant { get; set; } = new();
        public List<ChargeDto> Charges { get; set; } = new();
        public string Status { get; set; } = "OPEN";
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string County { get; set; } = string.Empty;
    }

    public class DefendantDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? Dob { get; set; }
    }

    public class ChargeDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
    }
}

