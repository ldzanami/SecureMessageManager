using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        /// <remarks>Максимум 50 символов.</remarks>
        [MaxLength(50)]
        public required string Username { get; set; }

        /// <summary>
        /// Аватар пользователя.
        /// </summary>
        public byte[] Icon { get; set; }

        /// <summary>
        /// Имя пользователя в верхнем регистре.
        /// </summary>
        public required string UsernameNormalized { get; set; }

        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        public required byte[] PasswordHash { get; set; }

        /// <summary>
        /// Соль для унификации пароля.
        /// </summary>
        public required byte[] Salt { get; set; }

        /// <summary>
        /// Публичный RSA ключ пользователя.
        /// </summary>
        public required string PublicKey { get; set; }

        /// <summary>
        /// Закрытый RSA ключ пользователя.
        /// </summary>
        public required byte[] PrivateKeyEnc { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Онлайн ли пользователь.
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Отправленные сообщения пользователя.
        /// </summary>
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        /// <summary>
        /// Отправленные файлы пользователя.
        /// </summary>
        public ICollection<File> SentFiles { get; set; } = new List<File>();

        /// <summary>
        /// Активные сессии пользователя.
        /// </summary>
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        /// <summary>
        /// Коллекция чатов пользователя.
        /// </summary>
        public ICollection<ChatMember> Chats { get; set; } = new List<ChatMember>();
    }
}
