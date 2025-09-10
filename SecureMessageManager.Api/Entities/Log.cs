namespace SecureMessageManager.Api.Entities
{
    public class Log
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
    }
}
