using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Tests;
namespace PetShelter.BusinessLayer.Validators;

public class AddDonationRequestValidator: AbstractValidator<AddDonationRequest>
{
	public AddDonationRequestValidator()
	{
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(1);
        RuleFor(x => x.Person.DateOfBirth).LessThan(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
	        .WithMessage("Adopter should be an adult.");
	}
}
