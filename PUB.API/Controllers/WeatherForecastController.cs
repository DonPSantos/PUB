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

        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        IOptions<AppSettings> appSettings,
                                        IConfiguration configuration)
        {
            _logger = logger;
            _appSettings = appSettings;
            _configuration = configuration;
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
        public AppSettings Appsettings()
        {
            return _configuration.Get<AppSettings>();
             


        }

        [HttpGet("teste1")]
        public string Teste1()
        {
            return _configuration.GetValue("Appsettings:Teste", "");

        }

        [HttpGet("mensagem")]
        public string Mensagem()
        {
            return _configuration.GetValue("MENSAGEM","");

        }

        [HttpGet("env")]
        public string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    }
}