using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response
{
    /// <summary>
    /// Ответ на изменение сообщения.
    /// </summary>
    public class PatchMessageResponseDto
    {
        /// <summary>
        /// Зашифрованный текст сообщения.
        /// </summary>
        public byte[] ContentEnc { get; set; }

        /// <summary>
        /// Дата изменения сообщения.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
