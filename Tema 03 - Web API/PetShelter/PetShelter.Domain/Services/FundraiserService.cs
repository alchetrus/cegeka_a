using System.Collections.Immutable;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Exceptions;
using PetShelter.Domain.Extensions.DomainModel;

namespace PetShelter.Domain.Services;

public class FundraiserService : IFundraiserService
{
    private readonly IFundraiserRepository _fundraiserRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IDonationRepository _donationRepository;

    public FundraiserService(IFundraiserRepository fundraiserRepository, IPersonRepository personRepository,
        IDonationRepository donationRepository)
    {
        _fundraiserRepository = fundraiserRepository;
        _personRepository = personRepository;
        _donationRepository = donationRepository;
    }

    public async Task<Fundraiser?> GetFundraiser(int fundraiserId)
    {
        var fundraiser = await _fundraiserRepository.GetById(fundraiserId);
        if (fundraiser == null)
        {
            return null;
        }

        fundraiser.Donations = _donationRepository.GetAll().Result.Where(x => x.FundraiserId == fundraiserId).ToList();

        return fundraiser.ToDomainModel();
    }

    public async Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers()
    {
        var fundraisers = await _fundraiserRepository.GetAll();
        return fundraisers
            .Select(f => f.ToDomainModel())
            .ToImmutableArray();
    }
    

    public async Task CreateFundraiserAsync(Fundraiser fundraiser, int ownerId)
    {
        var owner = await _personRepository.GetById(ownerId);

        if (owner == null)
        {
            throw new NotFoundException($"Owner with id {ownerId} not found for the fundraiser.");
        }

        await _fundraiserRepository.Add(fundraiser.FromDomainModel(owner));
    }

    public async Task DeleteFundraiserAsync(int id)
    {
        var fundraiser = await _fundraiserRepository.GetById(id);
        if (fundraiser == null)
        {
            throw new NotFoundException($"Fundraiser with id {id} not found.");
        }

        await _fundraiserRepository.Delete(id);
    }

    public async Task<string?> DonateToFundraiserAsync(Donation donation)
    {
        var donor = await _personRepository.GetById(donation.DonorId);
        var fundraiser = await _fundraiserRepository.GetById(donation.FundraiserId);

        if (donor == null)
        {
            throw new NotFoundException($"Donor with id {donation.DonorId} not found.");
        }

        if (fundraiser == null)
        {
            throw new NotFoundException($"Fundraiser with id {donation.FundraiserId} not found.");
        }

        if (fundraiser.CollectedAmount >= fundraiser.DonationTarget || fundraiser.Status == "Closed")
        {
            return
                $" Fundraiser with id {donation.FundraiserId} either reached the due date or reached its target! Look into other Fundraiser!";
        }

        fundraiser.CollectedAmount += donation.Amount;
        if (fundraiser.CollectedAmount >= fundraiser.DonationTarget || DateTime.Now >= fundraiser.EndDate)
        {
            fundraiser.Status = "Closed";
            await _fundraiserRepository.Update(fundraiser);
        }

        await _donationRepository.Add(donation.FromDomainModel(donor, fundraiser));
        return null;
    }
}