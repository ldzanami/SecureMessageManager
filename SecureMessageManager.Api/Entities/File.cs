using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Api.Entities
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Id файла.
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
        /// Имя файла.
        /// </summary>
        /// <remarks>Максимум 255 символов.</remarks>
        [MaxLength(255)]
        public string FileName { get; set; }

        /// <summary>
        /// Путь к файлу на сервере.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Зашифрованный AES ключ от файла.
        /// </summary>
        public byte[] FileEncKey { get; set; }

        /// <summary>
        /// Дата загрузки.
        /// </summary>
        /// <remarks>Автозаполняется.</remarks>
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

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
