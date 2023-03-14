using System.Collections.Immutable;

namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtension
{
    public static Domain.Fundraiser AsDomainModel(this Fundraiser fundraiser)
    {
        return new Domain.Fundraiser(fundraiser.Title, fundraiser.Description, fundraiser.DonationTarget,
            fundraiser.EndDate);
    }

    public static FundraiserBaseInfo AsBaseInfo(this Domain.Fundraiser fundraiser)
    {
        return new FundraiserBaseInfo(
            fundraiser.Name,
            fundraiser.Status
        );
    }

    public static FundraiserFullInfo AsResource(this Domain.Fundraiser domainFundraiser)
    {
        return new FundraiserFullInfo(
            domainFundraiser.Name,
            domainFundraiser.EndDate,
            domainFundraiser.DonationTarget,
            domainFundraiser.Donors.Select(d => d.AsResource()).ToImmutableArray(),
            domainFundraiser.Status,
            domainFundraiser.CollectedAmount,
            domainFundraiser.Description
        );
    }
}