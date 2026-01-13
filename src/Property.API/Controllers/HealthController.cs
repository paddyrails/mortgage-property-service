using Microsoft.AspNetCore.Mvc;

namespace Property.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Health() => Ok(new { Status = "Healthy", Service = "Property.API", Timestamp = DateTime.UtcNow });

    [HttpGet("live")]
    public IActionResult Live() => Ok(new { Status = "Alive" });

    [HttpGet("ready")]
    public IActionResult Ready() => Ok(new { Status = "Ready" });
}
