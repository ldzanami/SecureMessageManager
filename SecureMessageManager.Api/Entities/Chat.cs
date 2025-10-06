namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность чата.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Id чата.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название чата.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание чата.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Является ли чат группой.
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// Путь к аватару чата на сервере.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Когда создан чат.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Когда последний раз обновлялись данные чата.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Id создателя чата.
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Ссылка на создателя чата.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// Список пользователей чата.
        /// </summary>
        public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();

        /// <summary>
        /// Коллекция сообщений чата.
        /// </summary>
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Коллекция файлов чата.
        /// </summary>
        public ICollection<File> Files { get; set; } = new List<File>();
    }
}
