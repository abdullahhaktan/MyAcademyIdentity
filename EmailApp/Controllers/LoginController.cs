using EmailApp.Context;
using EmailApp.Entities;
using EmailApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.Controllers
{
    [AllowAnonymous]
    public class LoginController(UserManager<AppUser> _userManager, SignInManager<AppUser>
        _signInManager , AppDbContext _context) : Controller
    {

        public async Task getCountsAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            int inComingMessageCount = _context.Messages.
Include(m => m.Receiver).Where(m => m.situation == true && m.Receiver.Id == user.Id && m.MessageType == "normal").Count();

            int sendMessageCount = _context.Messages.
                Include(m => m.Sender).Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "normal").Count();

            int draftCount = _context.Messages.
                Include(m => m.Sender).Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "draft").Count();

            int trashCount = _context.Messages.
                Include(m => m.Sender).Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "trash").Count();


            HttpContext.Session.SetInt32("inComingMessageCount", inComingMessageCount);
            HttpContext.Session.SetInt32("sendMessageCount", sendMessageCount);
            HttpContext.Session.SetInt32("draftCount", draftCount);
            HttpContext.Session.SetInt32("trashCount", trashCount);

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı değil");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false,
                false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email veya şifre hatalı");
                return View(model);
            }

            HttpContext.Session.SetString("user", user.FirstName + " " + user.LastName);
            HttpContext.Session.SetString("userName", user.UserName);
            HttpContext.Session.SetString("userMail", user.Email);

            await getCountsAsync();

            var lastFourMessage = await _context.Messages
            .Include(m => m.Sender)
            .Where(m => (m.ReceiverId == user.Id) && (m.situation == true))
            .OrderByDescending(m => m.MessageId)
            .Take(4)
            .ToListAsync();

            if(lastFourMessage is not null)
            {
                HttpContext.Session.SetString("durum", "var");
            }

            return RedirectToAction("Index","Message");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
