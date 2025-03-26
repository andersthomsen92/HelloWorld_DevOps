using System.Diagnostics;
using Messages;

namespace API.Services;

public class LanguageService
{
    private static LanguageService? _instance;

    public static LanguageService Instance
    {
        get { return _instance ??= new LanguageService(); }
    }

    private LanguageService() { }

    public LanguageResponse GetLanguages()
    {
        
        using var activity = MonitorService.ActivitySource.StartActivity(ActivityKind.Internal);
        MonitorService.Log.Information("Fetching languages using GreetingService.");

        try
        {
            var languages = GreetingService.Instance.GetLanguages();
            
            MonitorService.Log.Debug("Fetched {Length} languages from GreetingService.", languages.Length);
            
            return new LanguageResponse { Languages = languages };
        }
        catch (Exception ex)
        {
            MonitorService.Log.Error(ex, "Failed to fetch languages from GreetingService.");
            throw;
        }
    }
}