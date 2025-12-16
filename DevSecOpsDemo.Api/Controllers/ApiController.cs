using Microsoft.AspNetCore.Mvc;

namespace DevSecOpsDemo.Api.Controllers;

public record SumaRequest(int A, int B);

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "ok" });
    }

    [HttpPost("suma")]
    public IActionResult Suma([FromBody] SumaRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid body");
        }
        
        // Retornar código de éxito y resultado
        return Ok(new { result = request.A + request.B });
    }
}
