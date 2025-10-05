using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response
{
    /// <summary>
    /// Ответ на создание сообщения.
    /// </summary>
    public class MessageCreatedResponseDto
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
        /// Id чата.
        /// </summary>
        public Guid ChatId { get; set; }

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
    }
}
