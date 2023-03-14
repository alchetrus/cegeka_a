namespace PetShelter.Api.Resources.Extensions;

public static class DonationExtension
{
    public static Domain.Donation AsDomainModel(this Donation donation)
    {
        return new Domain.Donation(donation.Amount, donation.DonorId, donation.FundraiserId);
    }
}