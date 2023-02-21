using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundraiserRegistry;
    private readonly Dictionary<Currency, decimal> donations = new();

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundraiserRegistry = new Registry<Fundraiser>(new Database());
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, decimal amount, Currency currency)
    {
        try
        {
            donorRegistry.Register(donor);
            if (!donations.TryAdd(currency, amount))
            {
                donations[currency] += amount;
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public decimal GetTotalDonationsInRON()
    {
        decimal total = 0;
        donations.TryGetValue(Currency.RON, out total);
        return total;
    }

    public decimal GetTotalDonationsInEUR()
    {
        decimal total = 0;
        donations.TryGetValue(Currency.EUR, out total);
        return total;
    }

    public decimal GetTotalDonationsInMDL()
    {
        decimal total = 0;
        donations.TryGetValue(Currency.MDL, out total);
        return total;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRegistry.Register(fundraiser);
    }

    public Fundraiser GetFundraiser(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }

    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result;
    }
}