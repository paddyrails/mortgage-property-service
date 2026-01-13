using System.ComponentModel.DataAnnotations;
using Property.API.Models;

namespace Property.API.DTOs;

// Property Response DTO
public record PropertyResponseDto
{
    public Guid Id { get; init; }
    public string Street { get; init; } = string.Empty;
    public string? Unit { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;
    public string County { get; init; } = string.Empty;
    public string FullAddress { get; init; } = string.Empty;
    public string PropertyType { get; init; } = string.Empty;
    public string OccupancyType { get; init; } = string.Empty;
    public int YearBuilt { get; init; }
    public int PropertyAge { get; init; }
    public decimal SquareFeet { get; init; }
    public decimal LotSize { get; init; }
    public int Bedrooms { get; init; }
    public decimal Bathrooms { get; init; }
    public int Stories { get; init; }
    public bool HasGarage { get; init; }
    public int GarageSpaces { get; init; }
    public bool HasPool { get; init; }
    public bool HasBasement { get; init; }
    public decimal ListingPrice { get; init; }
    public decimal EstimatedValue { get; init; }
    public AppraisalResponseDto? Appraisal { get; init; }
    public TitleSearchResponseDto? TitleSearch { get; init; }
    public InsuranceResponseDto? Insurance { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

// Create Property DTO
public record CreatePropertyDto
{
    [Required]
    [StringLength(200)]
    public string Street { get; init; } = string.Empty;

    [StringLength(50)]
    public string? Unit { get; init; }

    [Required]
    [StringLength(100)]
    public string City { get; init; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string State { get; init; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string ZipCode { get; init; } = string.Empty;

    [StringLength(50)]
    public string? County { get; init; }

    [Required]
    public PropertyType PropertyType { get; init; }

    [Required]
    public OccupancyType OccupancyType { get; init; }

    [Range(1800, 2100)]
    public int YearBuilt { get; init; }

    [Range(0, 100000)]
    public decimal SquareFeet { get; init; }

    [Range(0, 1000)]
    public decimal LotSize { get; init; }

    [Range(0, 50)]
    public int Bedrooms { get; init; }

    [Range(0, 20)]
    public decimal Bathrooms { get; init; }

    [Range(1, 10)]
    public int Stories { get; init; } = 1;

    public bool HasGarage { get; init; }
    public int GarageSpaces { get; init; }
    public bool HasPool { get; init; }
    public bool HasBasement { get; init; }

    [Range(0, 100000000)]
    public decimal ListingPrice { get; init; }

    [Range(0, 100000000)]
    public decimal EstimatedValue { get; init; }
}

// Update Property DTO
public record UpdatePropertyDto
{
    public decimal? ListingPrice { get; init; }
    public decimal? EstimatedValue { get; init; }
    public int? Bedrooms { get; init; }
    public decimal? Bathrooms { get; init; }
    public decimal? SquareFeet { get; init; }
    public bool? HasGarage { get; init; }
    public int? GarageSpaces { get; init; }
    public bool? HasPool { get; init; }
    public bool? HasBasement { get; init; }
    public OccupancyType? OccupancyType { get; init; }
}

// Appraisal DTOs
public record AppraisalResponseDto
{
    public Guid Id { get; init; }
    public decimal AppraisedValue { get; init; }
    public DateTime AppraisalDate { get; init; }
    public string AppraiserName { get; init; } = string.Empty;
    public string AppraisalCompany { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public decimal? LandValue { get; init; }
    public decimal? ImprovementValue { get; init; }
    public string? Comments { get; init; }
    public string? ConditionReport { get; init; }
}

public record CreateAppraisalDto
{
    [Required]
    [Range(0, 100000000)]
    public decimal AppraisedValue { get; init; }

    [Required]
    public DateTime AppraisalDate { get; init; }

    [Required]
    [StringLength(100)]
    public string AppraiserName { get; init; } = string.Empty;

    [StringLength(100)]
    public string? AppraisalCompany { get; init; }

    [StringLength(50)]
    public string? LicenseNumber { get; init; }

    public decimal? LandValue { get; init; }
    public decimal? ImprovementValue { get; init; }

    [StringLength(1000)]
    public string? Comments { get; init; }

    [StringLength(500)]
    public string? ConditionReport { get; init; }
}

// Title Search DTOs
public record TitleSearchResponseDto
{
    public Guid Id { get; init; }
    public DateTime SearchDate { get; init; }
    public string TitleCompany { get; init; } = string.Empty;
    public string CaseNumber { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public bool HasLiens { get; init; }
    public string? LienDetails { get; init; }
    public bool HasEasements { get; init; }
    public string? EasementDetails { get; init; }
    public bool HasEncumbrances { get; init; }
    public bool IsClear { get; init; }
    public string? Comments { get; init; }
}

public record CreateTitleSearchDto
{
    [Required]
    public DateTime SearchDate { get; init; }

    [Required]
    [StringLength(100)]
    public string TitleCompany { get; init; } = string.Empty;

    [StringLength(50)]
    public string? CaseNumber { get; init; }

    public bool HasLiens { get; init; }
    public string? LienDetails { get; init; }
    public bool HasEasements { get; init; }
    public string? EasementDetails { get; init; }
    public bool HasEncumbrances { get; init; }
    public string? EncumbranceDetails { get; init; }
    public bool IsClear { get; init; }
    public string? Comments { get; init; }
}

// Insurance DTOs
public record InsuranceResponseDto
{
    public Guid Id { get; init; }
    public string InsuranceCompany { get; init; } = string.Empty;
    public string PolicyNumber { get; init; } = string.Empty;
    public decimal CoverageAmount { get; init; }
    public decimal AnnualPremium { get; init; }
    public decimal MonthlyPremium { get; init; }
    public decimal Deductible { get; init; }
    public DateTime EffectiveDate { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string InsuranceType { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public bool IsExpired { get; init; }
    public bool HasFloodInsurance { get; init; }
    public decimal? FloodCoverage { get; init; }
}

public record CreateInsuranceDto
{
    [Required]
    [StringLength(100)]
    public string InsuranceCompany { get; init; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string PolicyNumber { get; init; } = string.Empty;

    [Required]
    [Range(0, 100000000)]
    public decimal CoverageAmount { get; init; }

    [Required]
    [Range(0, 100000)]
    public decimal AnnualPremium { get; init; }

    [Range(0, 100000)]
    public decimal Deductible { get; init; }

    [Required]
    public DateTime EffectiveDate { get; init; }

    [Required]
    public DateTime ExpirationDate { get; init; }

    public InsuranceType InsuranceType { get; init; }

    public bool HasFloodInsurance { get; init; }
    public decimal? FloodCoverage { get; init; }
}

// API Response wrapper
public record ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> FailResponse(string message, List<string>? errors = null)
        => new() { Success = false, Message = message, Errors = errors };
}
