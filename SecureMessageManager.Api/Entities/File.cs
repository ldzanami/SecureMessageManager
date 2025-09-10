using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Api.Entities
{
    public class File
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        
        [MaxLength(255)]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileEncKey { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
