using Microsoft.AspNetCore.Mvc;
using Property.API.DTOs;
using Property.API.Services;

namespace Property.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly ILogger<PropertiesController> _logger;

    public PropertiesController(IPropertyService propertyService, ILogger<PropertiesController> logger)
    {
        _propertyService = propertyService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PropertyResponseDto>>>> GetAll()
    {
        var properties = await _propertyService.GetAllPropertiesAsync();
        return Ok(ApiResponse<IEnumerable<PropertyResponseDto>>.SuccessResponse(properties));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> GetById(Guid id)
    {
        var property = await _propertyService.GetPropertyByIdAsync(id);
        if (property == null)
            return NotFound(ApiResponse<PropertyResponseDto>.FailResponse($"Property {id} not found"));
        return Ok(ApiResponse<PropertyResponseDto>.SuccessResponse(property));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PropertyResponseDto>>>> Search(
        [FromQuery] string city, [FromQuery] string? state = null)
    {
        var properties = await _propertyService.SearchPropertiesAsync(city, state);
        return Ok(ApiResponse<IEnumerable<PropertyResponseDto>>.SuccessResponse(properties));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> Create([FromBody] CreatePropertyDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<PropertyResponseDto>.FailResponse("Validation failed", errors));
        }

        var property = await _propertyService.CreatePropertyAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = property.Id },
            ApiResponse<PropertyResponseDto>.SuccessResponse(property, "Property created"));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> Update(Guid id, [FromBody] UpdatePropertyDto dto)
    {
        var property = await _propertyService.UpdatePropertyAsync(id, dto);
        if (property == null)
            return NotFound(ApiResponse<PropertyResponseDto>.FailResponse($"Property {id} not found"));
        return Ok(ApiResponse<PropertyResponseDto>.SuccessResponse(property, "Property updated"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var result = await _propertyService.DeletePropertyAsync(id);
        if (!result)
            return NotFound(ApiResponse<object>.FailResponse($"Property {id} not found"));
        return Ok(ApiResponse<object>.SuccessResponse(new { Id = id }, "Property deleted"));
    }

    // Appraisal endpoints
    [HttpGet("{id:guid}/appraisal")]
    public async Task<ActionResult<ApiResponse<AppraisalResponseDto>>> GetAppraisal(Guid id)
    {
        var appraisal = await _propertyService.GetAppraisalAsync(id);
        if (appraisal == null)
            return NotFound(ApiResponse<AppraisalResponseDto>.FailResponse("Appraisal not found"));
        return Ok(ApiResponse<AppraisalResponseDto>.SuccessResponse(appraisal));
    }

    [HttpPost("{id:guid}/appraisal")]
    public async Task<ActionResult<ApiResponse<AppraisalResponseDto>>> CreateAppraisal(Guid id, [FromBody] CreateAppraisalDto dto)
    {
        var appraisal = await _propertyService.CreateAppraisalAsync(id, dto);
        return CreatedAtAction(nameof(GetAppraisal), new { id },
            ApiResponse<AppraisalResponseDto>.SuccessResponse(appraisal, "Appraisal created"));
    }

    // Title Search endpoints
    [HttpGet("{id:guid}/title")]
    public async Task<ActionResult<ApiResponse<TitleSearchResponseDto>>> GetTitleSearch(Guid id)
    {
        var title = await _propertyService.GetTitleSearchAsync(id);
        if (title == null)
            return NotFound(ApiResponse<TitleSearchResponseDto>.FailResponse("Title search not found"));
        return Ok(ApiResponse<TitleSearchResponseDto>.SuccessResponse(title));
    }

    [HttpPost("{id:guid}/title")]
    public async Task<ActionResult<ApiResponse<TitleSearchResponseDto>>> CreateTitleSearch(Guid id, [FromBody] CreateTitleSearchDto dto)
    {
        var title = await _propertyService.CreateTitleSearchAsync(id, dto);
        return CreatedAtAction(nameof(GetTitleSearch), new { id },
            ApiResponse<TitleSearchResponseDto>.SuccessResponse(title, "Title search created"));
    }

    // Insurance endpoints
    [HttpGet("{id:guid}/insurance")]
    public async Task<ActionResult<ApiResponse<InsuranceResponseDto>>> GetInsurance(Guid id)
    {
        var insurance = await _propertyService.GetInsuranceAsync(id);
        if (insurance == null)
            return NotFound(ApiResponse<InsuranceResponseDto>.FailResponse("Insurance not found"));
        return Ok(ApiResponse<InsuranceResponseDto>.SuccessResponse(insurance));
    }

    [HttpPost("{id:guid}/insurance")]
    public async Task<ActionResult<ApiResponse<InsuranceResponseDto>>> CreateInsurance(Guid id, [FromBody] CreateInsuranceDto dto)
    {
        var insurance = await _propertyService.CreateInsuranceAsync(id, dto);
        return CreatedAtAction(nameof(GetInsurance), new { id },
            ApiResponse<InsuranceResponseDto>.SuccessResponse(insurance, "Insurance created"));
    }
}
