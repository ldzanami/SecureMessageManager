using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [MaxLength(50)]
        public required string Username { get; set; }
        public required string UsernameNormalized { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] Salt { get; set; }
        public required string PublicKey { get; set; }
        public required byte[] PrivateKeyEnc { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsOnline { get; set; } = false;
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<File> SentFiles { get; set; } = new List<File>();
        public ICollection<File> ReceivedFiles { get; set; } = new List<File>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
