using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [MaxLength(50)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string PublicKey { get; set; }
        public byte[] PrivateKeyEnc { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsOnline { get; set; }
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<File> SentFiles { get; set; } = new List<File>();
        public ICollection<File> ReceivedFiles { get; set; } = new List<File>();
    }
}
