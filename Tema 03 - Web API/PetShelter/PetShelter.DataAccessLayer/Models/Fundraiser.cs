namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal DonationTarget { get; set; }
    public decimal CollectedAmount { get; set; }

    public string Status { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int OwnerId { get; set; }
    public Person Owner { get; set; }

    public ICollection<Donation> Donations { get; set; }
    
    
}