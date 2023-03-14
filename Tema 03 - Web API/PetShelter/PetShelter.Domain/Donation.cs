namespace PetShelter.Domain;

public class Donation
{
    public decimal Amount { get; set; }
    public int DonorId { get; set; }
    public int FundraiserId { get; set; }

    public Donation(decimal amount, int donorId, int fundraiserId)
    {
        Amount = amount;
        DonorId = donorId;
        FundraiserId = fundraiserId;
    }
}