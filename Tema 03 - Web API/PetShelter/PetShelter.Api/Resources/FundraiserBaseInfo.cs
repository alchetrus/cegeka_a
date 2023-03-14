namespace PetShelter.Api.Resources;

public class FundraiserBaseInfo
{
    public string Title { get; set; }
    public string Status { get; set; }

    public FundraiserBaseInfo(string title, string status)
    {
        Title = title;
        Status = status;
    }
}