using SecureMessageManager.Api.Data;

namespace SecureMessageManager.Api.Repositories.Interfaces.Helpers
{
    /// <summary>
    /// Интерфейс вспомогательного репозитория, который проверяет наличие объектов в БД.
    /// </summary>
    public interface ICheckExistRepository
    {
        /// <summary>
        /// Асинхронно проверяет наличие пользователя в БД.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <exception cref="KeyNotFoundException">В случае если пользователь в БД не найден.</exception>
        Task IsUserExist(Guid userId);


        /// <summary>
        /// Асинхронно проверяет наличие чата в БД.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <exception cref="KeyNotFoundException">В случае если чат в БД не найден.</exception>
        Task IsChatExist(Guid chatId);


        /// <summary>
        /// Асинхронно проверяет наличие сессии в БД.
        /// </summary>
        /// <param name="sessionId">Id сессии.</param>
        /// <exception cref="KeyNotFoundException">В случае если сессия в БД не найдена.</exception>
        Task IsSessionExist(Guid sessionId);


        /// <summary>
        /// Асинхронно проверяет наличие сообщения в БД.
        /// </summary>
        /// <param name="messageId">Id сообщения.</param>
        /// <exception cref="KeyNotFoundException">В случае если сообщение в БД не найдено.</exception>
        Task IsMessageExist(Guid messageId);


        /// <summary>
        /// Асинхронно проверяет наличие файла в БД.
        /// </summary>
        /// <param name="fileId">Id файла.</param>
        /// <exception cref="KeyNotFoundException">В случае если файл в БД не найден.</exception>
        Task IsFileExist(Guid fileId);


        /// <summary>
        /// Асинхронно проверяет наличие лога в БД.
        /// </summary>
        /// <param name="logId">Id лога.</param>
        /// <exception cref="KeyNotFoundException">В случае если лог в БД не найден.</exception>
        Task IsLogExist(Guid logId);


        /// <summary>
        /// Асинхронно проверяет наличие участника в чате.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="memberId">Id участника.</param>
        /// <exception cref="KeyNotFoundException">В случае если участник в чате не найден.</exception>
        Task IsMemberExist(Guid chatId, Guid memberId);
    }
}
