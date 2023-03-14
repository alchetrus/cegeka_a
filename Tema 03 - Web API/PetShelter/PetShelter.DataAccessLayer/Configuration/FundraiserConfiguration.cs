using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
    public void Configure(EntityTypeBuilder<Fundraiser> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Description).IsRequired().HasMaxLength(600);
        builder.Property(p => p.DonationTarget).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.Status).HasDefaultValue("Active").IsRequired().HasMaxLength(10);
        builder.Property(p => p.StartDate).HasDefaultValue(DateTime.Now).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.CollectedAmount).HasDefaultValue(0.0).IsRequired();

        builder.HasOne(p => p.Owner)
            .WithMany(p => p.FundraisersOwned)
            .HasForeignKey(p => p.OwnerId)
            .IsRequired().OnDelete(DeleteBehavior.ClientSetNull);
    }
}