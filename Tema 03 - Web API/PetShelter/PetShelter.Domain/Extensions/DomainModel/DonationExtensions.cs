namespace PetShelter.Domain.Extensions.DomainModel;

internal static class DonationExtensions
{
    public static DataAccessLayer.Models.Donation FromDomainModel(this Donation donation,
        DataAccessLayer.Models.Person person, DataAccessLayer.Models.Fundraiser fundraiser)
    {
        var entity = new DataAccessLayer.Models.Donation
        {
            Amount = donation.Amount,
            FundraiserId = donation.FundraiserId,
            DonorId = donation.DonorId,
            Donor = person,
            Fundraiser = fundraiser
        };
        return entity;
    }
}