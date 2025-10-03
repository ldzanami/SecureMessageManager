using Microsoft.EntityFrameworkCore;

namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность участника чата.
    /// </summary>
    [PrimaryKey(nameof(UserId), nameof(ChatId))]
    public class ChatMember
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id чата.
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Роль в чате.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Когда присоединился.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Когда обновлялись данные.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Ссылка на пользователя.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Ссылка на чат.
        /// </summary>
        public Chat? Chat { get; set; }
    }
}
