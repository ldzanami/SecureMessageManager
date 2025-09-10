using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace SecureMessageManager.Api.Entities
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public byte[] ContentEnc { get; set; }
        public byte[] AESKeyEnc { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
