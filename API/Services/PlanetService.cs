using System.Diagnostics;

namespace API.Services;

public class PlanetService
{
    private static PlanetService? _instance;
    
    public static PlanetService Instance
    {
        get { return _instance ??= new PlanetService(); }
    }
    
    public PlanetResponse GetPlanet()
    {
        using var activity = MonitorService.ActivitySource.StartActivity(ActivityKind.Internal);
        MonitorService.Log.Information("PlanetService.GetPlanet() called to fetch a random planet.");

        
        var planets = new[]
        {
            "Mercury",
            "Venus",
            "Earth",
            "Mars",
            "Jupiter",
            "Saturn",
            "Uranus",
            "Neptune"
        };

        // This Randomizer will be out of bounds for the array index
        // Entered Get TextController Catch Block with error: Index was outside the bounds of the array. 
        var index = new Random(DateTime.Now.Millisecond).Next(1, planets.Length+1);
        
        var selectedPlanet = planets[index];
        MonitorService.Log.Debug("Selected planet: {Planet}", selectedPlanet);
        
        return new PlanetResponse
        {
            Planet = planets[index]
        };
    }
}