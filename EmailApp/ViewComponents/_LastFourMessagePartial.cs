using EmailApp.Context;
using EmailApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmailApp.ViewComponents
{
    public class _LastFourMessagePartial(AppDbContext _context,UserManager<AppUser> _userManager):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await  _userManager.FindByNameAsync(User.Identity.Name);

            var lastFourMessage = await _context.Messages
                .Include(m => m.Sender)
                .Where(m => (m.ReceiverId == user.Id) && (m.situation == true) && m.MessageType!="trash")
                .OrderByDescending(m => m.MessageId)
                .Take(4)
                .ToListAsync();
            return View(lastFourMessage);
        }

    }
}
