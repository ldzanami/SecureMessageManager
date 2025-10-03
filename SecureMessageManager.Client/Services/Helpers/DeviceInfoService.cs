using SecureMessageManager.Shared.DTOs.Auxiliary.DeviceInfo;
using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecureMessageManager.Client.Services.Helpers
{
    public class DeviceInfoService
    {
        private const string DeviceIdFileName = "device.id.dat";
        private static readonly string StoragePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SMMClient");

        public static async Task<DeviceInfoDto> GetDeviceInfoAsync()
        {
            var id = await GetOrCreateDeviceIdAsync();

            var di = new DeviceInfoDto
            {
                DeviceId = id,
                MachineName = Environment.MachineName,
                UserName = Environment.UserName,
                OSDescription = RuntimeInformation.OSDescription,
                OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
                AppVersion = GetAppVersion(),
                ProcessorCount = Environment.ProcessorCount,
                PrimaryMac = GetPrimaryMac(),
                ScreenResolution = GetScreenResolution(),
                CollectedAtUtc = DateTime.UtcNow
            };

            try
            {
                using var mos = new ManagementObjectSearcher("select Name from Win32_Processor");
                var cpu = mos.Get().Cast<ManagementObject>().FirstOrDefault();
                if (cpu != null) di.CpuName = cpu["Name"]?.ToString()?.Trim();

                using var cs = new ManagementObjectSearcher("select TotalPhysicalMemory from Win32_ComputerSystem");
                var mem = cs.Get().Cast<ManagementObject>().FirstOrDefault();
                if (mem != null && ulong.TryParse(mem["TotalPhysicalMemory"]?.ToString(), out var m))
                    di.TotalPhysicalMemoryBytes = m;
            }
            catch
            {

            }

            return di;
        }

        private static async Task<string> GetOrCreateDeviceIdAsync()
        {
            Directory.CreateDirectory(StoragePath);
            var path = Path.Combine(StoragePath, DeviceIdFileName);

            if (File.Exists(path))
            {
                try
                {
                    var enc = await File.ReadAllBytesAsync(path);
                    var dec = ProtectedData.Unprotect(enc, null, DataProtectionScope.CurrentUser);
                    return Encoding.UTF8.GetString(dec);
                }
                catch
                {

                }
            }

            var newId = Guid.NewGuid().ToString();
            var plain = Encoding.UTF8.GetBytes(newId);
            var protectedBytes = ProtectedData.Protect(plain, null, DataProtectionScope.CurrentUser);
            await File.WriteAllBytesAsync(path, protectedBytes);
            return newId;
        }

        private static string GetPrimaryMac()
        {
            try
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up
                                && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .OrderByDescending(n => n.Speed)
                    .FirstOrDefault();

                if (nic == null) return null;
                return string.Join(":", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2")));
            }
            catch
            {
                return null;
            }
        }

        private static ScreenResolutionDto? GetScreenResolution()
        {
            try
            {
                return new ScreenResolutionDto
                {
                    Width = (int)SystemParameters.PrimaryScreenWidth,
                    Height = (int)SystemParameters.PrimaryScreenHeight
                };
            }
            catch
            {
                return null;
            }
        }

        private static string GetAppVersion()
        {
            var asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var v = asm.GetName().Version;
            return v?.ToString() ?? "unknown";
        }
    }
}