using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Client.Services.Helpers
{
    public class SecureDataService
    {
        public static async Task WriteSecureDataAsync(string token, string storagePath, string fileName)
        {
            Directory.CreateDirectory(storagePath);
            var path = Path.Combine(storagePath, fileName);

            var plain = Encoding.UTF8.GetBytes(token);
            var protectedBytes = ProtectedData.Protect(plain, null, DataProtectionScope.CurrentUser);
            await System.IO.File.WriteAllBytesAsync(path, protectedBytes);
        }

        public static async Task<string> TryGetSecureDataAsync(string storagePath, string fileName)
        {
            Directory.CreateDirectory(storagePath);
            var path = Path.Combine(storagePath, fileName);

            if (System.IO.File.Exists(path))
            {
                try
                {
                    var enc = await System.IO.File.ReadAllBytesAsync(path);
                    var dec = ProtectedData.Unprotect(enc, null, DataProtectionScope.CurrentUser);
                    return Encoding.UTF8.GetString(dec);
                }
                catch
                {

                }
            }
            return null;
        }
    }
}
