using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProtracV1.Models;
using ProtracV1.Services;
namespace ProtracV1.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailService _emailService;

//// added for email

    public HomeController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task<IActionResult> SendEmail()
    {
        await _emailService.SendEmailAsync("shubhangi.kelkar12@gmail.com", "Protrac", "<p>Hello ProTrac Message 1 Project Form Created</p>");
       return View();
    }

    /// end added

/// chnged commented below
 //   public HomeController(ILogger<HomeController> logger)
 //   {
 //       _logger = logger;
 //   }

    public IActionResult Index()
    {
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
