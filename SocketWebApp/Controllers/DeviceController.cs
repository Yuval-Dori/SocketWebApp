using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SocketWebApp.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activation() //Hadas please add device activation action here
        {
            return View();
        }
    }
}

