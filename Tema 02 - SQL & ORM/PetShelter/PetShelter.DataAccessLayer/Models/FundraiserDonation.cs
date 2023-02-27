namespace PetShelter.DataAccessLayer.Models;

public class FundraiserDonation : IEntity
{
    public int Id { get; set; }
    
    public decimal Amount { get; set; }

    /// <summary>
    /// FK to Person
    /// </summary>
    public int DonorId { get; set; }

    public Person Donor { get; set; }

    /// <summary>
    /// FK to Fundraiser
    /// </summary>
    public int FundraiserId { get; set; }

    public Fundraiser Fundraiser { get; set; }
}