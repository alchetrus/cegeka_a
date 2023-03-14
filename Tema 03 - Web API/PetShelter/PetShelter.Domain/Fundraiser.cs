using PetShelter.DataAccessLayer.Models;

namespace PetShelter.Domain;

public class Fundraiser : INamedEntity
{
    public string Name { get; }

    public string Description { get; set; }

    public decimal DonationTarget { get; set; }
    public decimal CollectedAmount { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; }

    public ICollection<Person> Donors { get; set; }

    public Fundraiser(string name, string description,  decimal donationTarget, DateTime endDate)
    {
        Name = name;
        Description = description;
        DonationTarget = donationTarget;
        EndDate = endDate;
        Donors = new List<Person>();
    }
    
    public Fundraiser(string name, string description, decimal donationTarget, decimal collectedAmount,
        DateTime endDate, string status)
    {
        Name = name;
        Description = description;
        DonationTarget = donationTarget;
        CollectedAmount = collectedAmount;
        EndDate = endDate;
        Status = status;
        Donors = new List<Person>();
    }
}