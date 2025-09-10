namespace SecureMessageManager.Api.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public string DeviceInfo { get; set; }
        public User User { get; set; }
    }
}
