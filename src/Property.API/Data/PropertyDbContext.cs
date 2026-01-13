using Microsoft.EntityFrameworkCore;
using Property.API.Models;

namespace Property.API.Data;

public class PropertyDbContext : DbContext
{
    public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Property> Properties => Set<Models.Property>();
    public DbSet<Appraisal> Appraisals => Set<Appraisal>();
    public DbSet<TitleSearch> TitleSearches => Set<TitleSearch>();
    public DbSet<PropertyInsurance> Insurances => Set<PropertyInsurance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Property configuration
        modelBuilder.Entity<Models.Property>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.Street, e.City, e.State, e.ZipCode });

            entity.HasOne(e => e.Appraisal)
                .WithOne(a => a.Property)
                .HasForeignKey<Appraisal>(a => a.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.TitleSearch)
                .WithOne(t => t.Property)
                .HasForeignKey<TitleSearch>(t => t.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Insurance)
                .WithOne(i => i.Property)
                .HasForeignKey<PropertyInsurance>(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var propertyId1 = Guid.Parse("aaaa1111-1111-1111-1111-111111111111");
        var propertyId2 = Guid.Parse("aaaa2222-2222-2222-2222-222222222222");

        modelBuilder.Entity<Models.Property>().HasData(
            new Models.Property
            {
                Id = propertyId1,
                Street = "123 Oak Street",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94102",
                County = "San Francisco",
                PropertyType = PropertyType.SingleFamily,
                OccupancyType = OccupancyType.PrimaryResidence,
                YearBuilt = 1995,
                SquareFeet = 2200,
                LotSize = 0.25m,
                Bedrooms = 4,
                Bathrooms = 2.5m,
                Stories = 2,
                HasGarage = true,
                GarageSpaces = 2,
                HasPool = false,
                HasBasement = true,
                ListingPrice = 850000,
                EstimatedValue = 825000,
                CreatedAt = DateTime.UtcNow
            },
            new Models.Property
            {
                Id = propertyId2,
                Street = "456 Pine Avenue",
                Unit = "Unit 5B",
                City = "Seattle",
                State = "WA",
                ZipCode = "98101",
                County = "King",
                PropertyType = PropertyType.Condo,
                OccupancyType = OccupancyType.Investment,
                YearBuilt = 2010,
                SquareFeet = 1100,
                LotSize = 0,
                Bedrooms = 2,
                Bathrooms = 2,
                Stories = 1,
                HasGarage = true,
                GarageSpaces = 1,
                HasPool = false,
                HasBasement = false,
                ListingPrice = 425000,
                EstimatedValue = 415000,
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Appraisal>().HasData(
            new Appraisal
            {
                Id = Guid.Parse("bbbb1111-1111-1111-1111-111111111111"),
                PropertyId = propertyId1,
                AppraisedValue = 830000,
                AppraisalDate = DateTime.UtcNow.AddDays(-15),
                AppraiserName = "Robert Johnson",
                AppraisalCompany = "Bay Area Appraisals",
                LicenseNumber = "CA-12345",
                Status = AppraisalStatus.Completed,
                LandValue = 400000,
                ImprovementValue = 430000,
                ConditionReport = "Good condition, well maintained",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<TitleSearch>().HasData(
            new TitleSearch
            {
                Id = Guid.Parse("cccc1111-1111-1111-1111-111111111111"),
                PropertyId = propertyId1,
                SearchDate = DateTime.UtcNow.AddDays(-20),
                TitleCompany = "First American Title",
                CaseNumber = "FA-2024-001234",
                Status = TitleStatus.Clear,
                HasLiens = false,
                HasEasements = true,
                EasementDetails = "Standard utility easement",
                HasEncumbrances = false,
                IsClear = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<PropertyInsurance>().HasData(
            new PropertyInsurance
            {
                Id = Guid.Parse("dddd1111-1111-1111-1111-111111111111"),
                PropertyId = propertyId1,
                InsuranceCompany = "State Farm",
                PolicyNumber = "SF-HO-123456789",
                CoverageAmount = 850000,
                AnnualPremium = 2400,
                Deductible = 1000,
                EffectiveDate = DateTime.UtcNow.AddMonths(-6),
                ExpirationDate = DateTime.UtcNow.AddMonths(6),
                InsuranceType = InsuranceType.Homeowners,
                IsActive = true,
                HasFloodInsurance = false,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
