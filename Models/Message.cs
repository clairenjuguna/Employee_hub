using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_hub_new.Models
{
    public class Message
    {
        public int Id { get; set; }
        
        [Required]
        public string SenderId { get; set; }
        
        [Required]
        public string ReceiverId { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
        public bool IsRead { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
        
        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }
    }
} 