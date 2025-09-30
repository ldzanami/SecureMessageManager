using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureMessageManager.Client.Data
{
    public static class FileData
    {
        public static readonly string StoragePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SMMClient");
        public const string RefreshTokenFileName = "refresh.token.dat";
        public const string AccessTokenFileName = "access.token.dat";
    }
}
