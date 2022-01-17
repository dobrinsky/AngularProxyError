using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project4.Data;
using Project4.Models;
using System.Security.Claims;

namespace Project4.Controllers
{
    [Authorize]
    [ApiController]
    //[Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("weatherforecast/list-weatherForecasts")]
        //[ActionName()]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user2 = _context.Users.Find(userId);
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}