using System.Diagnostics;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LanguageController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        using var activity = MonitorService.ActivitySource.StartActivity("Langauges Controller Trace",ActivityKind.Server);
        MonitorService.Log.Information("Entered Language API Controller Get Endpoint, Get Method");

        try
        {
            var language = LanguageService.Instance.GetLanguages();

            MonitorService.Log.Debug("Successfully retrieved languages. Default: {DefaultLanguage}, Count: {LanguageCount}",
                language.DefaultLanguage, language.Languages?.Length ?? 0);
            
            return Ok(new GetLanguageModel.Response { DefaultLanguage = language.DefaultLanguage, Languages = language.Languages });
        }
        catch (Exception ex)
        {
            MonitorService.Log.Error(ex, "Failed to fetch languages in LanguageController.");
            return StatusCode(500, "An error occurred while fetching languages.");
        }
    }
}