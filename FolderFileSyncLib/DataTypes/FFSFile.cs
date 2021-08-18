using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FolderFileSyncLib.DataTypes
{
    public class FFSFile
    {
        public FileInfo file_info;
        public byte[] hash_value
        {
            get
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(file_info.FullName))
                    {
                        return md5.ComputeHash(stream);
                    }
                }
            }
        }

        public FFSFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                FileInfo fileinfo = new FileInfo(filepath);
                file_info = fileinfo;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
