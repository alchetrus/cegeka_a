using FluentValidation;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class DonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IValidator<AddDonationRequest> _donationValidator;
    private readonly IPersonService _personService;

    public DonationService(IDonationRepository donationRepository, IValidator<AddDonationRequest> validator,
        IPersonService personService)
    {
        _donationValidator = validator;
        _donationRepository = donationRepository;
        _personService = personService;
    }

    public async Task AddDonation(AddDonationRequest addDonationRequest)
    {
        var validationResult = await _donationValidator.ValidateAsync(addDonationRequest);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Request validation failed.");
        }

        var person = await _personService.GetPerson(addDonationRequest.Person);

        await _donationRepository.Add(new DataAccessLayer.Models.Donation
        {
            Amount = addDonationRequest.Amount,
            Donor = person
        });
    }
}