using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocketWebApp.Models;

namespace SocketWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string parameter)
    {
        //set parameter session here
        HttpContext.Session.SetString("parameterInfo", JsonConvert.SerializeObject(parameter));
        return View();
    }

    public IActionResult Terms()
    {
        return View();
    }

    public string test() //test for getting url parameter, delete this method once done
    {
        if (HttpContext.Session.GetString("parameterInfo") != null)
        {
            var parameter = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("parameterInfo"));
            return "parameter is:" + parameter;
        }
        else
        {
            return "failed to get parameter";
        }
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

