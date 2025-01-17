using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Employee_hub_new.Data;
using Employee_hub_new.Models;
using Employee_hub_new.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Employee_hub_new.Hubs;
using Microsoft.AspNetCore.Authorization;

namespace Employee_hub_new.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public TestController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> ViewMessagesNice()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var users = await _userManager.Users
                .Where(u => u.Id != currentUser.Id)
                .Select(u => new { u.Id, u.Email })
                .ToListAsync();

            ViewBag.Users = users;

            var messages = await _context.Messages
                .Where(m => m.SenderId == currentUser.Id || m.ReceiverId == currentUser.Id)
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .Select(m => new MessageViewModel
                {
                    Id = m.Id,
                    SenderEmail = _context.Users.FirstOrDefault(u => u.Id == m.SenderId).Email,
                    ReceiverEmail = _context.Users.FirstOrDefault(u => u.Id == m.ReceiverId).Email,
                    Content = m.Content,
                    Timestamp = m.Timestamp,
                    IsRead = m.IsRead
                })
                .ToListAsync();

            return View(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetConversation(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
            var messages = await _context.Messages
                .Where(m => 
                    (m.SenderId == currentUser.Id && m.ReceiverId == userId) ||
                    (m.SenderId == userId && m.ReceiverId == currentUser.Id))
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    m.Id,
                    m.SenderId,
                    m.Content,
                    m.Timestamp,
                    m.IsRead
                })
                .ToListAsync();

            // Mark unread messages as read
            var unreadMessages = await _context.Messages
                .Where(m => m.SenderId == userId && m.ReceiverId == currentUser.Id && !m.IsRead)
                .ToListAsync();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.IsRead = true;
                }
                await _context.SaveChangesAsync();

                // Notify sender that messages were read
                foreach (var message in unreadMessages)
                {
                    await _hubContext.Clients.User(message.SenderId)
                        .SendAsync("MessageRead", message.Id);
                }
            }

            return Json(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && message.ReceiverId == currentUser.Id)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
} 