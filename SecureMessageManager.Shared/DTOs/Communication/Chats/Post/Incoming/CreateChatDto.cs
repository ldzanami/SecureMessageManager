using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming
{
    /// <summary>
    /// Запрос на создание чата.
    /// </summary>
    public class CreateChatDto
    {
        /// <summary>
        /// Имя чата.
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
        /// Является ли чат группой.
        /// </summary>
        public bool IsGroup { get; set; }
    }
}
