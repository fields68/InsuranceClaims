using static System.Console;
public class ProgramUI
{

    private ClaimRepository _crepo = new ClaimRepository();

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Clear();

            WriteLine("Please select from the following options:\n" +
                "1. Add a new claim \n" +
                "2. View the next claim \n" +
                "3. See a list of all claims \n" +
                "4. Process (remove) the next claim \n" +
                "5. Exit");

            string input = ReadLine()!;

            switch (input)
            {
                case "1":
                    CreateNewClaim();
                    break;
                case "2":
                    ViewNextClaim();
                    break;
                case "3":
                    ViewAllClaims();
                    break;
                case "4":
                    DeleteNextClaim();
                    break;
                case "5":
                    Clear();
                    WriteLine("See you next time");
                    keepRunning = false;
                    break;
                default:
                    RedColor();
                    WriteLine("Not a valid option. Please Input and option from list above.");
                    ResetColor();
                    break;
            }
            KeyPress();
        }
    }

    //* Create
    private void CreateNewClaim()
    {
        Clear();

        ClaimInfo newClaim = new ClaimInfo();

        AddClaimType(newClaim);
        AddClaimDescription(newClaim);
        AddClaimAmount(newClaim);
        AddDateOfIncident(newClaim);
        AddDateOfClaim(newClaim);

        try
        {
            if (_crepo.AddNewClaim(newClaim))
            {
                GreenColor();
                WriteLine("Successfully added");
                ResetColor();
                DisplayClaimInfo(newClaim);
                GreenColor();
                WriteLine("to the Queue!");
                ResetColor();
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            RedColor();
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private void AddDateOfClaim(ClaimInfo newClaim)
    {
        Clear();
        bool properClaimDate = false;

        while (!properClaimDate)
        {
            WriteLine("Please enter the date when the Claim was filed: format yyyy, mm, dd");
            string dClaimString = ReadLine()!;

            try
            {
                newClaim.DateOfClaim = DateOnly.Parse(dClaimString);
                properClaimDate = true;
            }
            catch (Exception ex)
            {
                RedColor();
                WriteLine(ex.Message);
                WriteLine("Please enter a valid date. Ex. — 2022,06,25");
                SomethingWentWrong();
            }
        }
    }

    private void AddDateOfIncident(ClaimInfo newClaim)
    {
        Clear();
        bool properInciDate = false;

        while (!properInciDate)
        {
            WriteLine("Please enter the date when the incident occured: format yyyy,mm,dd");
            string dIncidentString = ReadLine()!;

            try
            {
                newClaim.DateOfIncident = DateOnly.Parse(dIncidentString);
                properInciDate = true;
            }
            catch (Exception ex)
            {
                RedColor();
                WriteLine(ex.Message);
                WriteLine("Please enter a valid date. Ex. — 2022,06,25");
                SomethingWentWrong();
            }

        }
    }

    private void AddClaimAmount(ClaimInfo newClaim)
    {
        Clear();
        bool properMoneyAmount = false;

        while (!properMoneyAmount)
        {
            WriteLine("Please enter claim amount:");
            string amountString = ReadLine()!;
            try
            {
                newClaim.ClaimAmount = Math.Round(decimal.Parse(amountString), 2);
                properMoneyAmount = true;
            }
            catch (Exception ex)
            {
                RedColor();
                WriteLine(ex.Message);
                WriteLine("Please enter a valid number amount. (i.e. 1200.34)");
                SomethingWentWrong();
            }
        }
    }

    private void AddClaimDescription(ClaimInfo newClaim)
    {
        Clear();
        WriteLine("Please write a description of the incident:");
        newClaim.Description = ReadLine()!;
    }

    private void AddClaimType(ClaimInfo newClaim)
    {
        Clear();
        bool claimTypeSelected = false;
        while (!claimTypeSelected)
        {
            WriteLine("Please select the type of claim that you wish to submit: \n" +
                        "1. Car \n" +
                        "2. Home \n" +
                        "3. Theft");

            string claimType = ReadLine()!;
            switch (claimType)
            {
                case "1":
                    newClaim.ClaimType = (Claim.Car);
                    claimTypeSelected = true;
                    break;
                case "2":
                    newClaim.ClaimType = (Claim.Home);
                    claimTypeSelected = true;
                    break;
                case "3":
                    newClaim.ClaimType = (Claim.Theft);
                    claimTypeSelected = true;
                    break;
                default:
                    RedColor();
                    WriteLine("Please enter a valid input. i.e. 1, 2, or 3");
                    SomethingWentWrong();
                    ResetColor();
                    break;
            }
        }
    }

    //* View Next Claim
    private void ViewNextClaim()
    {
        Clear();

        Queue<ClaimInfo> claimQueue = _crepo.GetAllClaims();

        if (claimQueue.Count > 0)
        {
            ClaimInfo viewNext = _crepo.NextClaim();
            DisplayClaimInfo(viewNext);
        }
        else
        {
            WriteLine("There are no claims to be viewed at this moment.");
        }
    }

    //* View All Claims
    private void ViewAllClaims()
    {
        Clear();
        Queue<ClaimInfo> claimQueue = _crepo.GetAllClaims();

        if (claimQueue.Count > 0)
        {
            foreach (ClaimInfo claim in claimQueue)
            {
                DisplayClaimInfo(claim);
            }
        }
        else
        {
            WriteLine("There are no claims to be viewed at this moment.");
        }
    }

    //* Delete Next Claim
    private void DeleteNextClaim()
    {
        Clear();
        ViewNextClaim();
        WriteLine(@"Are you sure you want to process this claim?
                    1. Yes
                    2. No");

        string yesNo = ReadLine()!;

        switch (yesNo)
        {
            case "1":

                if (_crepo.GetAllClaims().Count > 0)
                {
                    Clear();
                    ClaimInfo delNext = _crepo.DeleteClaim();
                    GreenColor();
                    WriteLine("Claim — ");
                    ResetColor();
                    DisplayClaimInfo(delNext);
                    GreenColor();
                    WriteLine("has been processed you may proceed on to the next claim.");
                    ResetColor();
                }
                else
                {
                    Clear();
                    CyanColor();
                    WriteLine("No claim to process.");
                    ResetColor();
                }
                break;
            case "2":
                RedColor();
                WriteLine("Claim NOT processed.");
                ResetColor();
                break;
            default:
                RedColor();
                WriteLine("Please enter valid option. i.e 1 or 2");
                ResetColor();
                break;
        }
    }

    private void DisplayClaimInfo(ClaimInfo claim)
    {
        CyanColor();
        WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        ResetColor();
        WriteLine($"Claim Type: {claim.ClaimType} \n" +
                    "Descritption:\n" +
                    $"  {claim.Description}\n" +
                    $"Claim Amount: ${claim.ClaimAmount} \n" +
                    $"Incident Date: {claim.DateOfIncident} \n" +
                    $"Claim Date: {claim.DateOfClaim} \n" +
                    $"{(claim.IsValid ? "This claim was made within the 30 day time frame." : "This claim was NOT made within the 30 day time frame")} ");
        CyanColor();
        WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        ResetColor();
    }

    private void RedColor()
    {
        ForegroundColor = ConsoleColor.DarkRed;
    }

    private void GreenColor()
    {
        ForegroundColor = ConsoleColor.Green;
    }

    private void CyanColor()
    {
        ForegroundColor = ConsoleColor.Cyan;
    }

    private void SomethingWentWrong()
    {
        RedColor();
        WriteLine("Something went wrong. \n" +
                    "Please Try Again");
        ResetColor();
        KeyPress();
        Clear();
    }

    private void KeyPress()
    {
        WriteLine("Press any key to continue.");
        ReadKey();
    }
}