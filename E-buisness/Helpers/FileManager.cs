﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Primitives;

namespace E_buisness.Helpers
{
    public static class FileManager
    {
        public static string SaveFile(IFormFile imageFile, string rootPath,string folderName)
        {
            string filename = imageFile.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }

            filename = Guid.NewGuid() + filename;

            string path = Path.Combine(rootPath, folderName,filename);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            return filename;
        }
        public static void DeleteFile(string rootPath, string folderName,string filename)
        {
            string path = Path.Combine(rootPath, folderName, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
