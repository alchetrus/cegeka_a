using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;
    private readonly Mock<IDonationRepository> _mockDonationRepository = new Mock<IDonationRepository>();
    private readonly Mock<IPersonRepository> _mockPersonRepository;

    private readonly IPersonService _personService;
    private readonly DonationService _donationServiceSut;

    private AddDonationRequest _donationRequest;

    public AddDonationTests()
    {
        _mockIdNumberValidator = new Mock<IIdNumberValidator>();
        _mockPersonRepository = new Mock<IPersonRepository>();

        _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object,
            new PersonValidator());
        _donationServiceSut = new DonationService(_mockDonationRepository.Object, new AddDonationRequestValidator(),
            _personService);
    }


    private void SetupHappyPath()
    {
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);

        _donationRequest = new AddDonationRequest
        {
            Amount = 10,
            Person = new Models.Person
            {
                Name = "aa",
                IdNumber = "1111222233337"
            }
        };
    }

    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        SetupHappyPath();

        await _donationServiceSut.AddDonation(_donationRequest);

        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _donationRequest.Amount)),
            Times.Once);
    }

    [Fact]
    public async Task GivenRequestWithMissingAmount_WhenAddDonation_DonationIsNotAdded()
    {
        SetupHappyPath();
        _donationRequest.Amount = 0;
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_donationRequest));

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GivenRequestWithInvalidAmount_WhenAddDonation_DonationIsNotAdded(decimal amount)
    {
        SetupHappyPath();

        _donationRequest.Amount = amount;
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_donationRequest));

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("")]
    public async Task GivenRequestWithInvalidName_WhenAddDonation_DonationIsNotAdded(string name)
    {
        SetupHappyPath();

        _donationRequest.Person.Name = name;
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_donationRequest));

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithInvalidCNP_WhenAddDonation_DonationIsNotAdded()
    {
        SetupHappyPath();
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

        var exception =
            await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_donationRequest));
        exception.Message.Should().Be("CNP format is invalid");

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithInvalidAge_WhenAddDonation_DonationIsNotAdded()
    {
        SetupHappyPath();

        _donationRequest.Person.DateOfBirth = new DateTime(2009, 10, 03);

        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_donationRequest));

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }
}