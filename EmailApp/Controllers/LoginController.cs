using EmailApp.Context;
using EmailApp.Entities;
using EmailApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.Controllers
{
    [AllowAnonymous] // Allow access without authentication
    public class LoginController(UserManager<AppUser> _userManager, SignInManager<AppUser>
        _signInManager, AppDbContext _context) : Controller
    {
        // Helper method to calculate and store message counts in session
        public async Task getCountsAsync()
        {
            // Find current user by username
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Count incoming messages (normal type, active situation)
            int inComingMessageCount = _context.Messages
                .Include(m => m.Receiver)
                .Where(m => m.situation == true && m.Receiver.Id == user.Id && m.MessageType == "normal")
                .Count();

            // Count sent messages (normal type, active situation)
            int sendMessageCount = _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "normal")
                .Count();

            // Count draft messages (draft type, active situation)
            int draftCount = _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "draft")
                .Count();

            // Count trash messages (trash type, active situation)
            int trashCount = _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.situation == true && m.Sender.Id == user.Id && m.MessageType == "trash")
                .Count();

            // Store all counts in session for quick access in views
            HttpContext.Session.SetInt32("inComingMessageCount", inComingMessageCount);
            HttpContext.Session.SetInt32("sendMessageCount", sendMessageCount);
            HttpContext.Session.SetInt32("draftCount", draftCount);
            HttpContext.Session.SetInt32("trashCount", trashCount);
        }

        // Display login page
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            // Find user by email (using email as username)
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı değil");
                return View(model);
            }

            // Attempt to sign in with password
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false,
                false); // isPersistent: false, lockoutOnFailure: false

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email veya şifre hatalı");
                return View(model);
            }

            // Store user information in session after successful login
            HttpContext.Session.SetString("user", user.FirstName + " " + user.LastName); // Full name
            HttpContext.Session.SetString("userName", user.UserName); // Username
            HttpContext.Session.SetString("userMail", user.Email); // Email

            // Calculate message counts for the logged-in user
            await getCountsAsync();

            // Get last 4 messages for the user to display recent activity
            var lastFourMessage = await _context.Messages
                .Include(m => m.Sender) // Include sender details
                .Where(m => (m.ReceiverId == user.Id) && (m.situation == true)) // User's received active messages
                .OrderByDescending(m => m.MessageId) // Latest first
                .Take(4) // Limit to 4 messages
                .ToListAsync();

            // Set session flag if there are recent messages
            if (lastFourMessage is not null)
            {
                HttpContext.Session.SetString("durum", "var"); // "var" means "exists" in Turkish
            }

            // Redirect to messages page after successful login
            return RedirectToAction("Index", "Message");
        }

        // Logout action - signs out user and redirects to login page
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync(); // Clear authentication cookie
            return RedirectToAction("Index", "Login"); // Redirect to login page
        }
    }
}