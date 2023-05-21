public class ClaimRepository
{
    protected readonly Queue<ClaimInfo> _claim = new Queue<ClaimInfo>();

    public ClaimRepository()
    {
        Seed();
    }
    public bool AddNewClaim(ClaimInfo claim)
    {
        if (claim is null)
        {
            return false;
        }
        else
        {
            _claim.Enqueue(claim);
            return true;
        }
    }
    public ClaimInfo NextClaim()
    {
        return _claim.Peek();
    }
    public Queue<ClaimInfo> GetAllClaims()
    {
        return _claim;
    }
    public ClaimInfo DeleteClaim()
    {
        return _claim.Dequeue();
    }
    private void Seed()
    {
        ClaimInfo claim1 = new ClaimInfo
        {
            ClaimType = Claim.Car,
            Description = "Giant Cupcake fell onto car.",
            ClaimAmount = 6135.00m,
            DateOfIncident = new DateOnly(2023, 04, 24),
            DateOfClaim = new DateOnly(2023, 05, 09),
        };
        ClaimInfo claim2 = new ClaimInfo
        {
            ClaimType = Claim.Home,
            Description = "House Fire.",
            ClaimAmount = 12065.00m,
            DateOfIncident = new DateOnly(2023, 02, 12),
            DateOfClaim = new DateOnly(2023, 02, 20),
        };
        ClaimInfo claim3 = new ClaimInfo
        {
            ClaimType = Claim.Theft,
            Description = "Jewerly missing from apartment. TV and laptop also stolen.",
            ClaimAmount = 6000.00m,
            DateOfIncident = new DateOnly(2023, 01, 30),
            DateOfClaim = new DateOnly(2023, 03, 25),
        };

        AddNewClaim(claim1);
        AddNewClaim(claim2);
        AddNewClaim(claim3);
    }
}