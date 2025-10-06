using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming
{
    /// <summary>
    /// Отправленное сообщение.
    /// </summary>
    public class SendMessageDto
    {
        /// <summary>
        /// Id чата.
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Id отправителя.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Зашифрованное содержимое сообщения.
        /// </summary>
        public byte[] ContentEnc { get; set; }

        /// <summary>
        /// Зашифрованный AES ключ от сообщения.
        /// </summary>
        public byte[] AESKeyEnc { get; set; }

        /// <summary>
        /// Когда было отправлено.
        /// </summary>
        public DateTime SentAt { get; set; }
    }
}
