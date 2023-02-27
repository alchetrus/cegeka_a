using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class FundraiserDonationConfiguration : IEntityTypeConfiguration<FundraiserDonation>
{
    public void Configure(EntityTypeBuilder<FundraiserDonation> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.DonorId).IsRequired();
        builder.Property(p => p.FundraiserId).IsRequired();

        builder.HasOne(p => p.Donor)
            .WithMany(p => p.FundraiserDonations)
            .HasForeignKey(p => p.DonorId)
            .IsRequired();
        
        builder.HasOne(p => p.Fundraiser)
            .WithMany(p => p.FundraiserDonations)
            .HasForeignKey(p => p.FundraiserId)
            .IsRequired();
    }
}