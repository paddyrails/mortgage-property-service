using Microsoft.EntityFrameworkCore;
using Property.API.Data;
using Property.API.DTOs;
using Property.API.Models;

namespace Property.API.Services;

public class PropertyService : IPropertyService
{
    private readonly PropertyDbContext _context;
    private readonly ILogger<PropertyService> _logger;

    public PropertyService(PropertyDbContext context, ILogger<PropertyService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PropertyResponseDto>> GetAllPropertiesAsync()
    {
        var properties = await _context.Properties
            .Include(p => p.Appraisal)
            .Include(p => p.TitleSearch)
            .Include(p => p.Insurance)
            .Where(p => p.IsActive)
            .ToListAsync();

        return properties.Select(MapToResponseDto);
    }

    public async Task<PropertyResponseDto?> GetPropertyByIdAsync(Guid id)
    {
        var property = await _context.Properties
            .Include(p => p.Appraisal)
            .Include(p => p.TitleSearch)
            .Include(p => p.Insurance)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

        return property == null ? null : MapToResponseDto(property);
    }

    public async Task<IEnumerable<PropertyResponseDto>> SearchPropertiesAsync(string city, string? state = null)
    {
        var query = _context.Properties
            .Include(p => p.Appraisal)
            .Where(p => p.IsActive && p.City.ToLower().Contains(city.ToLower()));

        if (!string.IsNullOrWhiteSpace(state))
        {
            query = query.Where(p => p.State.ToLower() == state.ToLower());
        }

        var properties = await query.ToListAsync();
        return properties.Select(MapToResponseDto);
    }

    public async Task<PropertyResponseDto> CreatePropertyAsync(CreatePropertyDto dto)
    {
        var property = new Models.Property
        {
            Street = dto.Street,
            Unit = dto.Unit,
            City = dto.City,
            State = dto.State,
            ZipCode = dto.ZipCode,
            County = dto.County ?? "",
            PropertyType = dto.PropertyType,
            OccupancyType = dto.OccupancyType,
            YearBuilt = dto.YearBuilt,
            SquareFeet = dto.SquareFeet,
            LotSize = dto.LotSize,
            Bedrooms = dto.Bedrooms,
            Bathrooms = dto.Bathrooms,
            Stories = dto.Stories,
            HasGarage = dto.HasGarage,
            GarageSpaces = dto.GarageSpaces,
            HasPool = dto.HasPool,
            HasBasement = dto.HasBasement,
            ListingPrice = dto.ListingPrice,
            EstimatedValue = dto.EstimatedValue
        };

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created property: {PropertyId}", property.Id);

        return MapToResponseDto(property);
    }

    public async Task<PropertyResponseDto?> UpdatePropertyAsync(Guid id, UpdatePropertyDto dto)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null || !property.IsActive) return null;

        if (dto.ListingPrice.HasValue) property.ListingPrice = dto.ListingPrice.Value;
        if (dto.EstimatedValue.HasValue) property.EstimatedValue = dto.EstimatedValue.Value;
        if (dto.Bedrooms.HasValue) property.Bedrooms = dto.Bedrooms.Value;
        if (dto.Bathrooms.HasValue) property.Bathrooms = dto.Bathrooms.Value;
        if (dto.SquareFeet.HasValue) property.SquareFeet = dto.SquareFeet.Value;
        if (dto.HasGarage.HasValue) property.HasGarage = dto.HasGarage.Value;
        if (dto.GarageSpaces.HasValue) property.GarageSpaces = dto.GarageSpaces.Value;
        if (dto.HasPool.HasValue) property.HasPool = dto.HasPool.Value;
        if (dto.HasBasement.HasValue) property.HasBasement = dto.HasBasement.Value;
        if (dto.OccupancyType.HasValue) property.OccupancyType = dto.OccupancyType.Value;

        property.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated property: {PropertyId}", property.Id);

        return MapToResponseDto(property);
    }

    public async Task<bool> DeletePropertyAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null) return false;

        property.IsActive = false;
        property.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted property: {PropertyId}", id);

        return true;
    }

    public async Task<AppraisalResponseDto?> GetAppraisalAsync(Guid propertyId)
    {
        var appraisal = await _context.Appraisals
            .FirstOrDefaultAsync(a => a.PropertyId == propertyId);
        return appraisal == null ? null : MapAppraisalToDto(appraisal);
    }

    public async Task<AppraisalResponseDto> CreateAppraisalAsync(Guid propertyId, CreateAppraisalDto dto)
    {
        var appraisal = new Appraisal
        {
            PropertyId = propertyId,
            AppraisedValue = dto.AppraisedValue,
            AppraisalDate = dto.AppraisalDate,
            AppraiserName = dto.AppraiserName,
            AppraisalCompany = dto.AppraisalCompany ?? "",
            LicenseNumber = dto.LicenseNumber ?? "",
            Status = AppraisalStatus.Completed,
            LandValue = dto.LandValue,
            ImprovementValue = dto.ImprovementValue,
            Comments = dto.Comments,
            ConditionReport = dto.ConditionReport
        };

        _context.Appraisals.Add(appraisal);
        await _context.SaveChangesAsync();

        return MapAppraisalToDto(appraisal);
    }

    public async Task<TitleSearchResponseDto?> GetTitleSearchAsync(Guid propertyId)
    {
        var titleSearch = await _context.TitleSearches
            .FirstOrDefaultAsync(t => t.PropertyId == propertyId);
        return titleSearch == null ? null : MapTitleSearchToDto(titleSearch);
    }

    public async Task<TitleSearchResponseDto> CreateTitleSearchAsync(Guid propertyId, CreateTitleSearchDto dto)
    {
        var titleSearch = new TitleSearch
        {
            PropertyId = propertyId,
            SearchDate = dto.SearchDate,
            TitleCompany = dto.TitleCompany,
            CaseNumber = dto.CaseNumber ?? "",
            Status = dto.IsClear ? TitleStatus.Clear : TitleStatus.IssuesFound,
            HasLiens = dto.HasLiens,
            LienDetails = dto.LienDetails,
            HasEasements = dto.HasEasements,
            EasementDetails = dto.EasementDetails,
            HasEncumbrances = dto.HasEncumbrances,
            EncumbranceDetails = dto.EncumbranceDetails,
            IsClear = dto.IsClear,
            Comments = dto.Comments
        };

        _context.TitleSearches.Add(titleSearch);
        await _context.SaveChangesAsync();

        return MapTitleSearchToDto(titleSearch);
    }

    public async Task<InsuranceResponseDto?> GetInsuranceAsync(Guid propertyId)
    {
        var insurance = await _context.Insurances
            .FirstOrDefaultAsync(i => i.PropertyId == propertyId);
        return insurance == null ? null : MapInsuranceToDto(insurance);
    }

    public async Task<InsuranceResponseDto> CreateInsuranceAsync(Guid propertyId, CreateInsuranceDto dto)
    {
        var insurance = new PropertyInsurance
        {
            PropertyId = propertyId,
            InsuranceCompany = dto.InsuranceCompany,
            PolicyNumber = dto.PolicyNumber,
            CoverageAmount = dto.CoverageAmount,
            AnnualPremium = dto.AnnualPremium,
            Deductible = dto.Deductible,
            EffectiveDate = dto.EffectiveDate,
            ExpirationDate = dto.ExpirationDate,
            InsuranceType = dto.InsuranceType,
            IsActive = true,
            HasFloodInsurance = dto.HasFloodInsurance,
            FloodCoverage = dto.FloodCoverage
        };

        _context.Insurances.Add(insurance);
        await _context.SaveChangesAsync();

        return MapInsuranceToDto(insurance);
    }

    #region Mapping Methods

    private static PropertyResponseDto MapToResponseDto(Models.Property property)
    {
        return new PropertyResponseDto
        {
            Id = property.Id,
            Street = property.Street,
            Unit = property.Unit,
            City = property.City,
            State = property.State,
            ZipCode = property.ZipCode,
            County = property.County,
            FullAddress = property.FullAddress,
            PropertyType = property.PropertyType.ToString(),
            OccupancyType = property.OccupancyType.ToString(),
            YearBuilt = property.YearBuilt,
            PropertyAge = property.PropertyAge,
            SquareFeet = property.SquareFeet,
            LotSize = property.LotSize,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Stories = property.Stories,
            HasGarage = property.HasGarage,
            GarageSpaces = property.GarageSpaces,
            HasPool = property.HasPool,
            HasBasement = property.HasBasement,
            ListingPrice = property.ListingPrice,
            EstimatedValue = property.EstimatedValue,
            Appraisal = property.Appraisal == null ? null : MapAppraisalToDto(property.Appraisal),
            TitleSearch = property.TitleSearch == null ? null : MapTitleSearchToDto(property.TitleSearch),
            Insurance = property.Insurance == null ? null : MapInsuranceToDto(property.Insurance),
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt
        };
    }

    private static AppraisalResponseDto MapAppraisalToDto(Appraisal appraisal)
    {
        return new AppraisalResponseDto
        {
            Id = appraisal.Id,
            AppraisedValue = appraisal.AppraisedValue,
            AppraisalDate = appraisal.AppraisalDate,
            AppraiserName = appraisal.AppraiserName,
            AppraisalCompany = appraisal.AppraisalCompany,
            Status = appraisal.Status.ToString(),
            LandValue = appraisal.LandValue,
            ImprovementValue = appraisal.ImprovementValue,
            Comments = appraisal.Comments,
            ConditionReport = appraisal.ConditionReport
        };
    }

    private static TitleSearchResponseDto MapTitleSearchToDto(TitleSearch titleSearch)
    {
        return new TitleSearchResponseDto
        {
            Id = titleSearch.Id,
            SearchDate = titleSearch.SearchDate,
            TitleCompany = titleSearch.TitleCompany,
            CaseNumber = titleSearch.CaseNumber,
            Status = titleSearch.Status.ToString(),
            HasLiens = titleSearch.HasLiens,
            LienDetails = titleSearch.LienDetails,
            HasEasements = titleSearch.HasEasements,
            EasementDetails = titleSearch.EasementDetails,
            HasEncumbrances = titleSearch.HasEncumbrances,
            IsClear = titleSearch.IsClear,
            Comments = titleSearch.Comments
        };
    }

    private static InsuranceResponseDto MapInsuranceToDto(PropertyInsurance insurance)
    {
        return new InsuranceResponseDto
        {
            Id = insurance.Id,
            InsuranceCompany = insurance.InsuranceCompany,
            PolicyNumber = insurance.PolicyNumber,
            CoverageAmount = insurance.CoverageAmount,
            AnnualPremium = insurance.AnnualPremium,
            MonthlyPremium = insurance.MonthlyPremium,
            Deductible = insurance.Deductible,
            EffectiveDate = insurance.EffectiveDate,
            ExpirationDate = insurance.ExpirationDate,
            InsuranceType = insurance.InsuranceType.ToString(),
            IsActive = insurance.IsActive,
            IsExpired = insurance.IsExpired,
            HasFloodInsurance = insurance.HasFloodInsurance,
            FloodCoverage = insurance.FloodCoverage
        };
    }

    #endregion
}
