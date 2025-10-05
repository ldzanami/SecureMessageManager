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
        public string ChatId { get; set; }

        /// <summary>
        /// Id отправителя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Зашифрованное содержимое сообщения.
        /// </summary>
        public string ContentEnc { get; set; }

        /// <summary>
        /// Когда было отправлено.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
