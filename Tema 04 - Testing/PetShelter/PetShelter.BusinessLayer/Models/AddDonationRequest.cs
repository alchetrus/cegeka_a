using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationRequest
{
    public decimal Amount { get; set; }
    
    public Person Person { get; set; }
}