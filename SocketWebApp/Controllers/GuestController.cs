using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocketWebApp.Models;

namespace SocketWebApp.Controllers
{
    public class GuestController : Controller
    {
        private readonly ICosmosDbGuestService _cosmosDbService;

        public GuestController(ICosmosDbGuestService cosmosDbUserService)
        {
            _cosmosDbService = cosmosDbUserService;
        }

        public IActionResult AfterGuestEntry()
        {
            return View();
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,FirstName,LastName,Email,Mobile,CardNumber,Expiry,CVV")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.AddGuestAsync(guest);
                return RedirectToAction("AfterGuestEntry"); 
            }

            return View(guest);
        }

    }
}

