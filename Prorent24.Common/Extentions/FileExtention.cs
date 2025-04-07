using System.IO;

namespace Prorent24.Common.Extentions
{
    public static class FileExtention
    {
        public static string GetMimeType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }

        public static string GetExtentionByType(string type)
        {
            switch (type)
            {
                case "text/plain": return ".txt";
                case "application/pdf": return ".pdf";
                case "application/vnd.ms-word": return ".doc";
                case "application/vnd.ms-excel": return ".xls";
                case "image/png": return ".png";
                case "image/jpeg": return ".jpg";
                case "image/gif": return ".gif";
                case "text/csv": return ".csv";
                default: return "";
            }
        }

    }
}
