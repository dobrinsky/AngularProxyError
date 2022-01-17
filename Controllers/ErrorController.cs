using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project4.Data;
using Project4.Models.Errors;
using Project4.Models.Errors.Resources;
using System.Linq.Expressions;

namespace Project4.Controllers
{
    //[Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment hostingEnvironment;

        public ErrorController(IHostEnvironment host, ApplicationDbContext _context, ILogger<WeatherForecastController> logger)
        {
            hostingEnvironment = host;
            this._context = _context;
            _logger = logger;
        }

        //[HttpGet("/list-error")]
        [HttpGet("error/list-errors")]
        public IEnumerable<ErrorResource> Get()
        {
            try
            {
                var result = new List<ErrorResource> { new ErrorResource { Description = "Rapid"}, new ErrorResource { Description = "Real" } };

                return result;
            }
            catch (Exception e)
            {
                Error.Add(e);

                return new List<ErrorResource>();
            }
        }

    }
}
