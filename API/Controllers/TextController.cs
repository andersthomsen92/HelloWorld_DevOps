using System.Diagnostics;
using API.Models;
using API.Services;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(string languageCode)
    {
        
        MonitorService.Log.Debug("Entered Get TextController");
        using var activity = MonitorService.ActivitySource.StartActivity("Text Controller Trace Start",ActivityKind.Server);
        try
        {
            var greeting = GreetingService.Instance.Greet(new GreetingRequest { LanguageCode = languageCode });
            var planet = PlanetService.Instance.GetPlanet();

            var response = new GetGreetingModel.Response
            {
                Greeting = greeting.Greeting,
                Planet = planet.Planet
            };

            MonitorService.Log.Debug("TextController gave response: " + response.Greeting);
            return Ok(response);
        }
        catch (Exception e)
        {

            MonitorService.Log.Error("Entered Get TextController Catch Block with error: " + e.Message);
            return StatusCode(500, "An error occurred");
        }
    }
}