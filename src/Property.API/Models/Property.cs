using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.API.Models;

public class Property
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Street { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Unit { get; set; }

    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string ZipCode { get; set; } = string.Empty;

    [StringLength(50)]
    public string County { get; set; } = string.Empty;

    [Required]
    public PropertyType PropertyType { get; set; }

    [Required]
    public OccupancyType OccupancyType { get; set; }

    public int YearBuilt { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal SquareFeet { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal LotSize { get; set; }

    public int Bedrooms { get; set; }

    [Column(TypeName = "decimal(3,1)")]
    public decimal Bathrooms { get; set; }

    public int Stories { get; set; } = 1;

    public bool HasGarage { get; set; }

    public int GarageSpaces { get; set; }

    public bool HasPool { get; set; }

    public bool HasBasement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ListingPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal EstimatedValue { get; set; }

    // Navigation properties
    public Appraisal? Appraisal { get; set; }
    public TitleSearch? TitleSearch { get; set; }
    public PropertyInsurance? Insurance { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    [NotMapped]
    public string FullAddress => string.IsNullOrEmpty(Unit)
        ? $"{Street}, {City}, {State} {ZipCode}"
        : $"{Street} {Unit}, {City}, {State} {ZipCode}";

    [NotMapped]
    public int PropertyAge => DateTime.Today.Year - YearBuilt;
}

public enum PropertyType
{
    SingleFamily = 1,
    Condo = 2,
    Townhouse = 3,
    MultiFamily = 4,
    Manufactured = 5,
    Cooperative = 6
}

public enum OccupancyType
{
    PrimaryResidence = 1,
    SecondHome = 2,
    Investment = 3
}
