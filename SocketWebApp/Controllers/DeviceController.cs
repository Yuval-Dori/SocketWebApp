using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocketWebApp.Models;

namespace SocketWebApp
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activation() 
        {
            if (HttpContext.Session.GetString("guestInfo") == null && HttpContext.Session.GetString("userInfo") == null)
            {
                return View("EntryRequest");
            }
            else
            {
                if(HttpContext.Session.GetString("parameterInfo") != null)
                {
                    //get parameter session object
                    var parameterInfo = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("parameterInfo"));

                    //Hadas please add device activation action here using parameterInfo as device id

                    return View();
                }
                else
                {
                    return View("ScanAgain");

                }

            }
        }

        public IActionResult De_Activation() //Hadas please add device de-activation action here
        {

            if (HttpContext.Session.GetString("parameterInfo") != null)
            {
                //get parameter session object
                var parameterInfo = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("parameterInfo"));

                //Hadas please add device de-activation action here using parameterInfo as device id

                return View();
            }
            else
            {
                return View();

            }
            
        }

    }
}

