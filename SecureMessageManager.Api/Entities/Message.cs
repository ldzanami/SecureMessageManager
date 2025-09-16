using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность сообщения.
    /// </summary>
    public class Message
    {

        /// <summary>
        /// Id сообщения.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id отправителя.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Id получателя.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Зашифрованный текст сообщения.
        /// </summary>
        public byte[] ContentEnc { get; set; }

        /// <summary>
        /// Зашифрованный AES ключ от сообщения.
        /// </summary>
        public byte[] AESKeyEnc { get; set; }

        /// <summary>
        /// Дата отправления.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата изменения сообщения.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Изменялось ли сообщение.
        /// </summary>
        public bool IsUpdated { get; set; }

        /// <summary>
        /// Было ли сообщение прочитано.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Ссылка на отправителя.
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// Ссылка на получателя.
        /// </summary>
        public User Receiver { get; set; }
    }
}
