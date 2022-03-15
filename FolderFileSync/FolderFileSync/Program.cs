using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FoldeFileSync
{
    class Program
    {
        static void Main(string[] args)
        {

#if DEBUG
            string dir1 = @"D:\Videos\Shows\";
            string dir2 = @"E:\Videos\Shows\";
#else
            string dir1;
            string dir2;
#endif
            Console.Write("Enter Directory 1: ");
#if DEBUG
#else
            dir1 = Console.ReadLine();
#endif
            Console.Write("\nEnter Directory 2: ");
#if DEBUG
#else
            dir2 = Console.ReadLine();
#endif
            List<string> dir1files = new List<string>();
            List<string> dir2files = new List<string>();

            if (!Directory.Exists(dir1)) throw new FileNotFoundException(dir1);
            if (!Directory.Exists(dir2)) throw new FileNotFoundException(dir2);

            dir1files = ProcessDirectory(dir1).ToList();
            dir2files = ProcessDirectory(dir2).ToList();

            Console.WriteLine("\nNot found on dir2");
            foreach (var file in dir1files)
            {
                string filename = Path.GetFileName(file);
                //Console.WriteLine($"comparing {filename}");
                bool found =
                    (from similar_file in dir2files
                     where Path.GetFileName(similar_file) == filename
                     select similar_file).ToList().Count() >= 1;
                if (!found) Console.WriteLine(file);
            }

            Console.WriteLine("\nNot found on dir1");
            foreach (var file in dir2files)
            {
                string filename = Path.GetFileName(file);
                //Console.WriteLine($"comparing {filename}");
                bool found =
                    (from similar_file in dir1files
                     where Path.GetFileName(similar_file) == filename
                     select similar_file).ToList().Count() >= 1;
                if (!found) Console.WriteLine(file);
            }
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static string[] ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            List<string> fileEntries = Directory.GetFiles(targetDirectory).ToList();

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (var subdir in subdirectoryEntries)
            {
                string[] more_deep = ProcessDirectory(subdir);
                fileEntries.AddRange(more_deep);
            }
            return fileEntries.ToArray();
        }
    }
}