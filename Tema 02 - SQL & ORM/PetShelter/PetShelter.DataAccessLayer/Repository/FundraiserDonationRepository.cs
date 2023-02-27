using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserDonationRepository : BaseRepository<FundraiserDonation>, IFundraiserDonationRepository
{
    public FundraiserDonationRepository(PetShelterContext context) : base(context)
    {
    }
}