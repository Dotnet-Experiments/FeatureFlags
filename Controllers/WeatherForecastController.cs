using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace FeatureFlags.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new []
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFeatureManager _featureManager;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        private async Task LogFeatureFlags()
        {
            await
            foreach (var f in _featureManager.GetFeatureNamesAsync())
            {
                _logger.LogInformation($"Feature flags: {f} = {await _featureManager.IsEnabledAsync(f)}");
            }
        }

        [HttpGet("all")]
        public IEnumerable<WeatherForecast> GetAll()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpGet("local")]
        public async Task<IEnumerable<WeatherForecast>> GetLocalWeather()
        {
            await LogFeatureFlags();
            if (!await _featureManager.IsEnabledAsync(nameof(FeatureManagement.EnableLocalWeather)))
            {
                throw new NotImplementedException($"{nameof(GetLocalWeather)} is not enabled");
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}