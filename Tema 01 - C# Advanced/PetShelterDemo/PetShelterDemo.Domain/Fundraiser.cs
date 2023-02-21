namespace PetShelterDemo.Domain;

public class Fundraiser : INamedEntity
{
    public string Name { get; }
    private string Description { get; }
    private Donation DonationTarget { get; }
    
    private readonly List<Person> donors = new();
    public List<Person> Donors => donors;
    
    private readonly Dictionary<string, List<Donation>> donations = new();
    public Dictionary<string, List<Donation>> Donations => donations;

    public Fundraiser(string name, string description, Donation donationTarget)
    {
        Name = name;
        Description = description;
        DonationTarget = donationTarget;
    }

    public void Donate(Person donor, Donation donation)
    {
        try
        {
            donors.Add(donor);
            if (!donations.TryAdd(donor.IdNumber, new List<Donation> { donation }))
            {
                donations[donor.IdNumber].Add(donation);
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}