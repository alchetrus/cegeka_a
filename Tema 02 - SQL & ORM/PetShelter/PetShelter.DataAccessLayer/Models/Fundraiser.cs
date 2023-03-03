namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal DonationTarget { get; set; }

    public ICollection<Donation> Donations { get; set; }
}