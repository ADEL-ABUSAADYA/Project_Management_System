using Project_Management_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Project_Management_System.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Context _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public string WeatherForecast()
    {
        var proj = _context.Projects.ToList();
        return proj.ToString().ToLower();
    }
}