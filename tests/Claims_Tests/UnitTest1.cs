namespace Claims_Tests;

public class UnitTest1
{
    [Fact]
    public void SetDescription_ShouldSetCorrectSrting()
    {
        //* Arrange
        ClaimInfo claim = new ClaimInfo();
        claim.Description = "Stolen computer.";

        //* Act
        string expected = "Stolen computer.";
        string actual = claim.Description;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestName()
    {
        //* Arrange
        ClaimInfo claim = new ClaimInfo
        {
            ClaimType = Claim.Car,
            Description = "Giant Cupcake fell onto car.",
            ClaimAmount = 6135.00m,
            DateOfIncident = new DateOnly(2023, 04, 24),
            DateOfClaim = new DateOnly(2023, 05, 09),
        };
        bool actual = false;
        ClaimRepository _crepo = new ClaimRepository();

        //* Act
        _crepo.AddNewClaim(claim);
        ClaimInfo nextClaim = _crepo.NextClaim();
        bool expected = true;
        if (nextClaim != null)
        {
            actual = true;
        }
        else
        {
            actual = false;
        }

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShowAllClaims_ShouldShowClaims()
    {
        //* Arrange
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
        bool actual = false;
        ClaimRepository _crepo = new ClaimRepository();

        //* Act
        _crepo.AddNewClaim(claim1);
        _crepo.AddNewClaim(claim2);
        Queue<ClaimInfo> allClaims = _crepo.GetAllClaims();
        bool expected = true;
        if (allClaims.Count() > 0)
        {
            actual = true;
        }
        else
        {
            actual = false;
        }

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RemoveClaim_ShouldDeleteClaim()
    {
        //* Arrange
        ClaimInfo claim = new ClaimInfo
        {
            ClaimType = Claim.Theft,
            Description = "Jewerly missing from apartment. TV and laptop also stolen.",
            ClaimAmount = 6000.00m,
            DateOfIncident = new DateOnly(2023, 01, 30),
            DateOfClaim = new DateOnly(2023, 03, 25),
        };
        bool actual = false;
        ClaimRepository _crepo = new ClaimRepository();

        //* Act
        _crepo.AddNewClaim(claim);
        ClaimInfo delNext = _crepo.DeleteClaim();
        bool expected = true;
        if (delNext != null)
        {
            actual = true;
        }
        else
        {
            actual = false;
        }

        //* Assert
        Assert.Equal(expected, actual);
    }
}