using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PUB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public IOptions<AppSettings> _appSettings { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("appsettings")]
        public string Appsettings()
        {
            var teste = _appSettings.Value.Teste;
            return teste;
        }

        [HttpGet("env")]
        public string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    }
}