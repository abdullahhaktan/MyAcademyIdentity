using EmailApp.Context;
using EmailApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.ViewComponents
{
    // ViewComponent to display last four messages for the current user
    public class _LastFourMessagePartial(AppDbContext _context, UserManager<AppUser> _userManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Get current logged-in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Query last four non-deleted messages for the current user
            var lastFourMessage = await _context.Messages
                .Include(m => m.Sender) // Include sender details via navigation property
                .Where(m => (m.ReceiverId == user.Id) && // Messages received by current user
                          (m.situation == true) && // Active messages (not deleted)
                          m.MessageType != "trash") // Exclude messages in trash
                .OrderByDescending(m => m.MessageId) // Get most recent messages first
                .Take(4) // Limit to four messages
                .ToListAsync();

            return View(lastFourMessage);
        }
    }
}