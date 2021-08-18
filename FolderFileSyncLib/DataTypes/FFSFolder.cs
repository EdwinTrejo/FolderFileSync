using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FolderFileSyncLib.DataTypes
{
    public class FFSFolder
    {
        public DirectoryInfo folder_info;
        public List<FFSFile> files;
        public List<FFSFolder> folders;

        public FFSFolder(string folderpath)
        {
            if (Directory.Exists(folderpath))
            {
                DirectoryInfo folderinfo = new DirectoryInfo(folderpath);
                folder_info = folderinfo;

                //log stuff
                files = new List<FFSFile>();
                folders = new List<FFSFolder>();

                foreach (var folder in folder_info.GetDirectories())
                {
                    folders.Add(new FFSFolder(folder.FullName));
                }

                foreach (var file in folder_info.GetFiles())
                {
                    files.Add(new FFSFile(file.FullName));
                }
            }
            else
            {
                throw new DirectoryNotFoundException();
            }
        }
    }
}
