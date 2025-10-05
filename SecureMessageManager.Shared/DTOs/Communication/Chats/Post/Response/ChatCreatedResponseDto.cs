using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Response
{
    /// <summary>
    /// Ответ на создание чата.
    /// </summary>
    public class ChatCreatedResponseDto
    {
        /// <summary>
        /// Id чата.
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Название чата.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание чата.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Аватар чата.
        /// </summary>
        public byte[] Icon { get; set; }

        /// <summary>
        /// Когда создан чат.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
