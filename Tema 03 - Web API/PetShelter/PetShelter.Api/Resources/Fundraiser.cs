namespace PetShelter.Api.Resources;

public class Fundraiser
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal DonationTarget { get; set; }
    public DateTime EndDate { get; set; }
}