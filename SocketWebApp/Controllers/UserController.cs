using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocketWebApp.Models;

namespace SocketWebApp.Controllers
{

    public class UserController : Controller
    {
        private readonly ICosmosDbUserService _cosmosDbService;

        public UserController(ICosmosDbUserService cosmosDbUserService)
        {
            _cosmosDbService = cosmosDbUserService;
        }

        public IActionResult LoginPage()
        {
            return View("Login");
        }

        public async Task<string> Login(Credentials credentials)
        {
            var user =  await _cosmosDbService.GetUserAsync(credentials.Id);
            if(user == null)
            {
                return "This user Id does not exits in the system, please sign in first";
            }
            else
            {
                if(user.UserName == credentials.UserName && user.Password == credentials.Password)
                {
                    return "Succesful Login";
                }
                else
                {
                    return "Login Failed, incorrect username or password";
                }
            }
        }

        public IActionResult AfterSignUp()
        {
            return View();
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetUsersAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,FirstName,LastName,Email,Mobile,CardNumber,Expiry,CVV,UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.AddUserAsync(user);
                return RedirectToAction("AfterSignUp");
            }

            return View(user);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,FirstName,LastName,Email,Mobile,CardNumber,Expiry,CVV,UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateUserAsync(user.Id, user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            User user = await _cosmosDbService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            User user = await _cosmosDbService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetUserAsync(id));
        }
    }
}