namespace PetShelterDemo.Domain;

public enum Currency
{
    EUR,
    RON,
    MDL
}

public struct Donation
{
    public decimal DonationValue;
    public Currency Currency;
}