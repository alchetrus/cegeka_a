using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.Domain.Services;

public interface IFundraiserService
{
    Task<Fundraiser?> GetFundraiser(int fundraiserId);
    Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers();
    Task CreateFundraiserAsync(Fundraiser fundraiser, int ownerId);
    Task DeleteFundraiserAsync(int id);

    Task<string?> DonateToFundraiserAsync(Donation donation);
}