using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMathService _mathService;
        public HomeController(ILogger<HomeController> logger, 
            IMathService mathService)
        {
            _logger = logger;
            _mathService = mathService;
        }

        public IActionResult Index()
        {
            try
            {
                decimal result = _mathService.Divider(5, 0);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ошибюка при делении числен");
            }

            string userName = "gertsen.e.a";
            _logger.LogTrace(AppLogintEvents.Create, 
                "LogTrace Message {dateException} {userName}", 
                DateTime.Now, userName);

            _logger.LogDebug(AppLogintEvents.Read, 
                "LogDebug Message {dateException} {userName}",
                DateTime.Now, userName);

            _logger.LogInformation(AppLogintEvents.Update, 
                "LogInformation Message {dateException} {userName}",
                DateTime.Now, userName);

            _logger.LogWarning(AppLogintEvents.Delete, "LogWarning Message {dateException} {userName}",
                DateTime.Now, userName);

            _logger.LogError(AppLogintEvents.Error,  
                "LogError Message {dateException} {userName}",
                DateTime.Now, userName);

            _logger.LogCritical(AppLogintEvents.RecodrNotFound, 
                "LogCritical Message {dateException} {userName}",
                DateTime.Now, userName);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}