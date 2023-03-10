using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Configuration;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer;

public class PetShelterContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Donation> Donations { get; set; }

    public DbSet<Fundraiser> Fundraisers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=172.17.0.2,1433;Database=PetShelter;User=sa;Password=password123;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new DonationConfiguration());
    }
}