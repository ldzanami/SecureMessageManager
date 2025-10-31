using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Messages.Patch
{
    /// <summary>
    /// Запрос на изменение сообщения.
    /// </summary>
    public class PatchMessageDto
    {
        /// <summary>
        /// Id изменяющего.
        /// </summary>
        public Guid PatcherId { get; set; }

        /// <summary>
        /// Зашифрованный текст сообщения.
        /// </summary>
        public byte[] ContentEnc { get; set; }
    }
}
