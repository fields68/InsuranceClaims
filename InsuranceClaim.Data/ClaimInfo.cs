public class ClaimInfo
{
    public ClaimInfo() { }

    public ClaimInfo(Claim claimType, string description, decimal claimAmount, DateOnly dateOfIncident, DateOnly dateOfClaim)
    {
        ClaimType = claimType;
        Description = description;
        ClaimAmount = claimAmount;
        DateOfIncident = dateOfIncident;
        DateOfClaim = dateOfClaim;
    }

    public Claim ClaimType { get; set; }

    public string Description { get; set; }

    public decimal ClaimAmount { get; set; }

    public DateOnly DateOfIncident { get; set; }

    public DateOnly DateOfClaim { get; set; }

    public bool IsValid
    {
        get
        {
            int daysBetween = DateOfClaim.DayNumber - DateOfIncident.DayNumber;
            if (daysBetween <= 30)
            {
                return true;
            }
            else { return false; }
        }
    }
}
