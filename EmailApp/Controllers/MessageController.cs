using EmailApp.Context;
using EmailApp.Entities;
using EmailApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Message = EmailApp.Entities.Message;

namespace EmailApp.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    public class MessageController(AppDbContext _context, UserManager<AppUser> _userManager)
        : Controller
    {
        // Helper method to calculate and store message counts in session
        public async Task getCountsAsync()
        {
            var userName = User.Identity.Name;

            // Find user by username (alternative to UserManager approach)
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();

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

            // Store all counts in session
            HttpContext.Session.SetInt32("inComingMessageCount", inComingMessageCount);
            HttpContext.Session.SetInt32("sendMessageCount", sendMessageCount);
            HttpContext.Session.SetInt32("draftCount", draftCount);
            HttpContext.Session.SetInt32("trashCount", trashCount);
        }

        // Display inbox - all normal messages received by current user
        public async Task<IActionResult> Index()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Get active normal messages where user is receiver
            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageType == "normal")
                .ToList();

            return View(messages);
        }

        // View message details and mark as read
        public IActionResult MessageDetail(int id)
        {
            getCountsAsync();
            var message = _context.Messages.Include(m => m.Sender)
                .FirstOrDefault(m => m.MessageId == id);

            message.isRead = true; // Mark message as read
            _context.Messages.Update(message);
            _context.SaveChanges();

            return View(message);
        }

        // Display form to send new message or reply to existing one
        public IActionResult SendMessage(int? id)
        {
            getCountsAsync();

            // If ID provided, load existing message for reply
            if (id != null)
            {
                var message = _context.Messages.Include(m => m.Receiver)
                    .Where(m => m.MessageId == id && m.situation == true && m.MessageType == "normal")
                    .FirstOrDefault();

                SendMessageViewModel model = new SendMessageViewModel()
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    ReceiverEmail = message.Receiver.Email,
                    MessageType = message.MessageType,
                };

                if (message != null)
                {
                    TempData["isDraftToMessage"] = "true"; // Flag for view to indicate draft mode
                    return View(model);
                }
            }

            return View(); // Empty form for new message
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel? model, bool isDraft)
        {
            // Get sender and receiver from database
            var sender = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiver = await _userManager.FindByEmailAsync(model.ReceiverEmail);

            // Create new message object
            var message = new Message
            {
                Body = model.Body,
                Subject = model.Subject,
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
                SendDate = DateTime.Now,
                MessageCategory = model.MessageCategory,
                situation = true
            };

            message.MessageType = "normal";

            // Handle draft saving
            if (isDraft)
            {
                message.MessageType = "draft";
                message.ReceiverId = receiver.Id;
                message.SenderId = sender.Id;
                message.Body = model.Body;
                message.Subject = model.Subject;
                message.SendDate = DateTime.Now;

                _context.Messages.Update(message);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Handle normal message sending
                message.MessageType = "normal";
                var result = _context.Messages.Where(m => m.MessageId == message.MessageId).FirstOrDefault();

                // Add new message or replace existing one
                if (result is null)
                {
                    _context.Messages.Add(message);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Messages.Remove(result);
                    _context.Messages.Add(message);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }

        // Display draft messages
        [HttpGet]
        public async Task<IActionResult> Draft()
        {
            getCountsAsync();
            TempData["isDraft"] = "true"; // Flag for view

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.Sender.Id == user.Id && m.MessageType == "draft" && m.situation == true)
                .ToList();

            return View("Index", messages); // Reuse Index view for drafts
        }

        // Display sent messages
        [HttpGet]
        public async Task<IActionResult> SendBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Get messages sent by user (normal type, active)
            var messages = _context.Messages.Include(m => m.Receiver)
                .Where(m => m.SenderId == user.Id && m.MessageType == "normal" && m.situation == true)
                .ToList();

            return View(messages);
        }

        // Move message to trash (soft delete)
        [HttpGet]
        public async Task<IActionResult> SendToTrash(int id)
        {
            var message = _context.Messages.Find(id);
            message.MessageType = "trash"; // Change type to trash
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Display trash messages
        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Get messages where user is either sender or receiver and message is in trash
            var messages = _context.Messages.Include(m => m.Sender).Include(m => m.Receiver)
                .Where(m => (m.ReceiverId == user.Id || m.SenderId == user.Id) &&
                       m.MessageType == "trash" && m.situation == true)
                .ToList();

            return View(messages);
        }

        // Permanently delete message (soft delete by setting situation to false)
        public async Task<IActionResult> MessageDelete(int id)
        {
            var message = _context.Messages.Find(id);
            message.situation = false; // Mark as deleted
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Trash");
        }

        // Display important messages
        public async Task<IActionResult> Important()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "Önemli")
                .ToList();

            return View(messages);
        }

        // Display campaign/offer messages
        public async Task<IActionResult> Offer()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "kampanya")
                .ToList();

            return View(messages);
        }

        // Display social media messages
        public async Task<IActionResult> SocialMedia()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "sosyal medya")
                .ToList();

            return View(messages);
        }

        // Change message category to "Important"
        public async Task<IActionResult> ChangeToImportant(int id)
        {
            var message = _context.Messages.Where(m => m.MessageId == id).FirstOrDefault();
            message.MessageCategory = "Önemli"; // Set category to Important
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}