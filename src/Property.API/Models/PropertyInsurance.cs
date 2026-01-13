using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.API.Models;

public class PropertyInsurance
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    [StringLength(100)]
    public string InsuranceCompany { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string PolicyNumber { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal CoverageAmount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal AnnualPremium { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Deductible { get; set; }

    [Required]
    public DateTime EffectiveDate { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    public InsuranceType InsuranceType { get; set; }

    public bool IsActive { get; set; } = true;

    public bool HasFloodInsurance { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? FloodCoverage { get; set; }

    // Navigation
    public Property? Property { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public decimal MonthlyPremium => Math.Round(AnnualPremium / 12, 2);

    [NotMapped]
    public bool IsExpired => ExpirationDate < DateTime.UtcNow;
}

public enum InsuranceType
{
    Homeowners = 1,
    Hazard = 2,
    Flood = 3,
    Earthquake = 4,
    Umbrella = 5
}
