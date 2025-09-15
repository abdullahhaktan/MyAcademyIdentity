using EmailApp.Context;
using EmailApp.Entities;
using EmailApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Message = EmailApp.Entities.Message;

namespace EmailApp.Controllers
{
    [Authorize]
    public class MessageController(AppDbContext _context, UserManager<AppUser> _userManager)
        : Controller
    {
        public async Task getCountsAsync()
        {
            var userName = User.Identity.Name;

            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();

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


        public async Task<IActionResult> Index()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageType=="normal").ToList();

            return View(messages);
        }

        public IActionResult MessageDetail(int id)
        {
            getCountsAsync();
            var message = _context.Messages.Include(m => m.Sender).FirstOrDefault(m => m.MessageId == id);
            message.isRead = true;
            _context.Messages.Update(message);
            _context.SaveChanges();
            return View(message);
        }

        public IActionResult SendMessage(int? id)
        {
            getCountsAsync();

            if (id != null)
            {
                var message = _context.Messages.Include(m => m.Receiver)
                .Where(m => m.MessageId == id && m.situation == true && m.MessageType == "normal").FirstOrDefault();

                SendMessageViewModel model = new SendMessageViewModel()
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    ReceiverEmail = message.Receiver.Email,
                    MessageType = message.MessageType,
                };

                if (message != null)
                {
                    TempData["isDraftToMessage"] = "true";
                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel? model, bool isDraft)
        {
            var sender = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiver = await _userManager.FindByEmailAsync(model.ReceiverEmail);

            var message = new Message
            {
                Body = model.Body,
                Subject = model.Subject,
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
                SendDate = DateTime.Now,
                MessageCategory = model.MessageCategory,
                situation=true
            };

            message.MessageType = "normal";
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
                message.MessageType = "normal";
                var result = _context.Messages.Where(m => m.MessageId == message.MessageId).FirstOrDefault();
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

        [HttpGet]
        public async Task<IActionResult> Draft()
        {
            getCountsAsync();
            TempData["isDraft"] = "true";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.Sender.Id == user.Id && m.MessageType == "draft" && m.situation == true).ToList();

            return View("Index", messages);
        }

        [HttpGet]
        public async Task<IActionResult> SendBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _context.Messages.Include(m => m.Receiver)
                .Where(m => m.SenderId == user.Id && m.MessageType == "normal" && m.situation == true).ToList();

            return View(messages);
        }

        [HttpGet]
        public async Task<IActionResult> SendToTrash(int id)
        {
            var message = _context.Messages.Find(id);
            message.MessageType = "trash";
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages.Include(m => m.Sender).Include(m => m.Receiver)
            .Where(m => (m.ReceiverId == user.Id || m.SenderId == user.Id) && m.MessageType == "trash" && m.situation == true).ToList();

            return View(messages);
        }

        public async Task<IActionResult> MessageDelete(int id)
        {
            var message = _context.Messages.Find(id);
            message.situation = false;
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Trash");
        }

        public async Task<IActionResult> Important()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "Önemli").ToList();

            return View(messages);
        }

        public async Task<IActionResult> Offer()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "kampanya").ToList();

            return View(messages);
        }

        public async Task<IActionResult> SocialMedia()
        {
            getCountsAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _context.Messages.Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && m.situation == true && m.MessageCategory == "sosyal medya").ToList();

            return View(messages);
        }

        public async Task<IActionResult> ChangeToImportant(int id)
        {
            var message = _context.Messages.Where(m=>m.MessageId == id).FirstOrDefault();
            message.MessageCategory = "Önemli";
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
