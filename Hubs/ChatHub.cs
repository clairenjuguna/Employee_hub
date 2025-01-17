using Microsoft.AspNetCore.SignalR;
using Employee_hub_new.Models;
using Employee_hub_new.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Employee_hub_new.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SendMessage(string receiverId, string content)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user == null) return;

            var senderId = user.Id;
            
            // Save message to database
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Send to specific user
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content, message.Timestamp);
            
            // Send back to sender
            await Clients.Caller.SendAsync("MessageSent", message.Id);
        }

        public async Task MarkAsRead(int messageId)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user == null) return;

            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && message.ReceiverId == user.Id)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
                await Clients.User(message.SenderId).SendAsync("MessageRead", messageId);
            }
        }
    }
} 