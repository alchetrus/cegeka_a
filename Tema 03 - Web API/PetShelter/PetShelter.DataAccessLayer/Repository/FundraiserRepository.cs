using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<decimal?> GetRaisedAmount(int id)
    {
        var donations = await _context.Fundraisers.Include(n => n.Donations).SingleOrDefaultAsync(n => n.Id == id);
        return donations?.Donations.Sum(n => n.Amount);
    }

    public async Task Delete(int id)
    {
        var row = _context.Fundraisers.FirstOrDefault(row => row.Id == id);

        if (row == null)
        {
            return;
        }

        row.Status = "Closed";
        await _context.SaveChangesAsync();
    }
}