namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность сессии.
    /// </summary>
    public class Session
    {

        /// <summary>
        /// Id Сессии.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Долгоживущий токен.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Дата протухания токена.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Информация об устройстве.
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// Дата создания сессии.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ссылка на пользователя.
        /// </summary>
        public User User { get; set; }
    }
}
