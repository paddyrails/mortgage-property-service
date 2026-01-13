using Property.API.DTOs;

namespace Property.API.Services;

public interface IPropertyService
{
    Task<IEnumerable<PropertyResponseDto>> GetAllPropertiesAsync();
    Task<PropertyResponseDto?> GetPropertyByIdAsync(Guid id);
    Task<IEnumerable<PropertyResponseDto>> SearchPropertiesAsync(string city, string? state = null);
    Task<PropertyResponseDto> CreatePropertyAsync(CreatePropertyDto dto);
    Task<PropertyResponseDto?> UpdatePropertyAsync(Guid id, UpdatePropertyDto dto);
    Task<bool> DeletePropertyAsync(Guid id);
    
    // Appraisal
    Task<AppraisalResponseDto?> GetAppraisalAsync(Guid propertyId);
    Task<AppraisalResponseDto> CreateAppraisalAsync(Guid propertyId, CreateAppraisalDto dto);
    
    // Title Search
    Task<TitleSearchResponseDto?> GetTitleSearchAsync(Guid propertyId);
    Task<TitleSearchResponseDto> CreateTitleSearchAsync(Guid propertyId, CreateTitleSearchDto dto);
    
    // Insurance
    Task<InsuranceResponseDto?> GetInsuranceAsync(Guid propertyId);
    Task<InsuranceResponseDto> CreateInsuranceAsync(Guid propertyId, CreateInsuranceDto dto);
}
