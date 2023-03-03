using System.Runtime.InteropServices;
using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.Exceptions;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests
{
    public class RescuePetTests
    {
        private readonly IPersonService _personService;
        private readonly PetService _petServiceSut;

        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

        private RescuePetRequest _request;
        private AdoptPetRequest _adoptPetRequest;
        private UpdatePetRequest _updatePetRequest;

        public RescuePetTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockPetRepository = new Mock<IPetRepository>();
            _mockIdNumberValidator = new Mock<IIdNumberValidator>();

            _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object,
                new PersonValidator());
            _petServiceSut = new PetService(_personService, _mockPetRepository.Object, new RescuePetRequestValidator(),
                new AdoptPetRequestValidator());
        }

        private void SetupHappyPath()
        {
            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);

            _request = new RescuePetRequest
            {
                PetName = "Max",
                Type = Constants.PetType.Dog,
                Description = "Nice dog",
                IsHealthy = true,
                ImageUrl = "test",
                WeightInKg = 10,
                Person = new Models.Person
                {
                    DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                    IdNumber = "1111222233334",
                    Name = "TestName"
                }
            };

            _adoptPetRequest = new AdoptPetRequest
            {
                PetId = 1,
                Person = new Models.Person
                {
                    DateOfBirth = new DateTime(1990, 9, 13),
                    IdNumber = "1111222233334",
                    Name = "TestName"
                }
            };

            _updatePetRequest = new UpdatePetRequest
            {
                PetId = 1,
                NewPetName = "Danny"
            };
        }

        [Fact]
        public async void GivenValidData_WhenRescuePet_PetIsAdded()
        {
            //Arrange
            SetupHappyPath();

            //Act
            await _petServiceSut.RescuePet(_request);

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(-5)]
        [InlineData(0)]
        public async Task GiventWeightIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(
            decimal weight)
        {
            // Arrange
            SetupHappyPath();
            _request.WeightInKg = weight;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenRescuePet_ThenThrowsArgumentException()
        {
            //Arrange
            SetupHappyPath();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenValidData_WhenAdoptPet_PetIsAdopted()
        {
            //Arrange
            SetupHappyPath();

            _mockPetRepository.Setup(x => x.GetById(_adoptPetRequest.PetId))
                .ReturnsAsync(new Pet
                {
                    Id = _adoptPetRequest.PetId,
                    IsSheltered = true
                });

            //Act
            await _petServiceSut.AdoptPet(_adoptPetRequest);

            //Assert
            _mockPetRepository.Verify(x => x.Update(It.Is<Pet>(p => p.IsSheltered == false)), Times.Once);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(999)]
        public async Task GivenInvalidPetId_WhenAdoptPet_ThenThrowsNotFoundException(int id)
        {
            // Arrange
            SetupHappyPath();
            _adoptPetRequest.PetId = id;

            _mockPetRepository.Setup(x => x.GetById(_adoptPetRequest.PetId)).ReturnsAsync((Pet)null);

            // Act & Assert
            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _petServiceSut.AdoptPet(_adoptPetRequest));
            
            _mockPetRepository.Verify(x => x.Add(It.IsAny<Pet>()), Times.Never);
        }

        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenAdoptPet_ThenThrowsArgumentException()
        {
            //Arrange
            SetupHappyPath();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);
            _mockPetRepository.Setup(x => x.GetById(_adoptPetRequest.PetId))
                .ReturnsAsync(new Pet
                {
                    Id = _adoptPetRequest.PetId,
                    IsSheltered = true
                });
            
            //Act
            var exception =
                await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.AdoptPet(_adoptPetRequest));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenValidData_WhenUpdatePet_PetIsUpdated()
        {
            //Arrange
            SetupHappyPath();
            
            _mockPetRepository.Setup(x => x.GetById(_updatePetRequest.PetId)).ReturnsAsync(new Pet
            {
                Id = _updatePetRequest.PetId,
                Name = _request.PetName
            });
            
            //Act
            await _petServiceSut.UpdatePet(_updatePetRequest);
            
            _mockPetRepository.Verify(x => x.GetById(_adoptPetRequest.PetId), Times.Once);
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _updatePetRequest.NewPetName)), Times.Never);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(999)]
        public async Task GivenInvalidId_WhenUpdatePet_ThenThrowsNotFoundException(int id)
        {
            //Arrange
            SetupHappyPath();

            _updatePetRequest.PetId = id;
            _mockPetRepository.Setup(x => x.GetById(_adoptPetRequest.PetId)).ReturnsAsync((Pet)null);

            //Act
            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _petServiceSut.AdoptPet(_adoptPetRequest));
            
            //Assert
            _mockPetRepository.Verify(x => x.Add(It.IsAny<Pet>()), Times.Never);
        }
    }
}