using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auxiliary
{
    /// <summary>
    /// Данные об устройстве.
    /// </summary>
    public class DeviceInfoDto
    {
        /// <summary>
        /// Локально сгенерированный GUID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Имя устройства.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Информация об ОС.
        /// </summary>
        public string OSDescription { get; set; }

        /// <summary>
        /// Архитекрута ОС.
        /// </summary>
        public string OSArchitecture { get; set; }

        /// <summary>
        /// Архитектура процесса.
        /// </summary>
        public string ProcessArchitecture { get; set; }

        /// <summary>
        /// Версия клиента.
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// Название CPU.
        /// </summary>
        public string CpuName { get; set; }

        /// <summary>
        /// Количество логических процессоров.
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        /// Объём RAM.
        /// </summary>
        public ulong? TotalPhysicalMemoryBytes { get; set; }

        /// <summary>
        /// MAC-адрес сетевого интерфейса.
        /// </summary>
        public string PrimaryMac { get; set; }

        /// <summary>
        /// разрешение основного экрана.
        /// </summary>
        public (int Width, int Height)? ScreenResolution { get; set; }

        /// <summary>
        /// Дата сбора данных.
        /// </summary>
        public DateTime CollectedAtUtc { get; set; }

        /// <summary>
        /// Неявное преобразование из string json в DeviceInfoDto.
        /// </summary>
        /// <param name="json">string json DeviceInfoDto</param>
        public static implicit operator DeviceInfoDto(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            return JsonSerializer.Deserialize<DeviceInfoDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        /// <summary>
        /// Неявное преобразование из DeviceInfoDto в string json.
        /// </summary>
        /// <param name="info">DeviceInfoDto</param>
        public static implicit operator string(DeviceInfoDto info)
        {
            if (info == null) return null;

            return JsonSerializer.Serialize(info, new JsonSerializerOptions
            {
                WriteIndented = false
            });
        }
    }
}
