namespace PetShelter.Domain.Extensions.DomainModel;

internal static class FundraiserExtensions
{
    public static Fundraiser ToDomainModel(this DataAccessLayer.Models.Fundraiser fundraiser)
    {
        if (fundraiser == null)
        {
            return null;
        }

        return new Fundraiser(name: fundraiser.Title, description: fundraiser.Description,
            donationTarget: fundraiser.DonationTarget, collectedAmount: fundraiser.CollectedAmount,
            endDate: fundraiser.EndDate, status: fundraiser.Status);
    }

    public static DataAccessLayer.Models.Fundraiser FromDomainModel(this Fundraiser fundraiser,
        DataAccessLayer.Models.Person person)
    {
        var entity = new DataAccessLayer.Models.Fundraiser
        {
            Title = fundraiser.Name,
            DonationTarget = fundraiser.DonationTarget,
            CollectedAmount = fundraiser.CollectedAmount,
            Description = fundraiser.Description,
            EndDate = fundraiser.EndDate,
            Owner = person,
            OwnerId = person.Id
        };
        return entity;
    }
}