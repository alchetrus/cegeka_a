using System.Collections.Immutable;

namespace PetShelter.Api.Resources;

public class FundraiserFullInfo : Fundraiser
{
    public string Status { get; set; }
    public ImmutableArray<Person> Donors { get; set; }
    public decimal CollectedAmount { get; set; }

    public FundraiserFullInfo(string title, DateTime endDate, decimal donationTarget, ICollection<Person> donors,
        string status, decimal collectedAmount, string description)
    {
        Title = title;
        EndDate = endDate;
        DonationTarget = donationTarget;
        Description = description;
        Donors = donors.ToImmutableArray();
        Status = status;
        CollectedAmount = collectedAmount;
    }
}