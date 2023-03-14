namespace PetShelter.Api.Resources;

public class Donation
{
    public decimal Amount { get; set; }
    public int DonorId { get; set; }
    public int FundraiserId { get; set; }
}