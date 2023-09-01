using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Session_workshop.Models;


namespace Session_workshop.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("counter")]
    public IActionResult Index(string name)
    {

            HttpContext.Session.SetString("name",name);
            HttpContext.Session.SetInt32("sum",22);
            Console.WriteLine("name", name);
            return RedirectToAction("SeeUser");
    }

    [HttpPost("AddOne")]
    public IActionResult AddOne()
    {
        int? Old = HttpContext.Session.GetInt32("sum");
        int? New = Old + 1;
        HttpContext.Session.SetInt32("sum", (int)New);
        Console.WriteLine(New);
        return RedirectToAction("AddedNum");
    }

    [HttpPost("SubtractOne")]
    public IActionResult SubtractOne()
    {
        int? Old = HttpContext.Session.GetInt32("sum");
        int? New = Old - 1;
        HttpContext.Session.SetInt32("sum", (int)New);
        return RedirectToAction("AddedNum");
    }

    [HttpPost("AddTwo")]
    public IActionResult AddTwo()
    {
        int? Old = HttpContext.Session.GetInt32("sum");
        int? New = Old * 2;
        HttpContext.Session.SetInt32("sum", (int)New);
        return RedirectToAction("AddedNum");
    }

    [HttpPost("Random")]
    public IActionResult Random()
    {
        Random rand = new();
        int? Old = HttpContext.Session.GetInt32("sum");
        int? New = Old + rand.Next(1,11);
        HttpContext.Session.SetInt32("sum", (int)New);
        return RedirectToAction("AddedNum");
    }    


    [HttpGet("Added")]
    public ViewResult AddedNum()
    {
        int? newNum = HttpContext.Session.GetInt32("sum");
        return View("Display");
    }



    [HttpGet("users")]
    public ViewResult SeeUser()
    {
        string? name = HttpContext.Session.GetString("name");
        int? num = HttpContext.Session.GetInt32("sum");
        return View("Display");
    }
    

    [HttpGet("logout")]
    public ViewResult Logout()
    {
        HttpContext.Session.Clear();
        return View("Index");
    }

    [HttpGet("privacy")]
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
