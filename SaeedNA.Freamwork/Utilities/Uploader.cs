using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace SaeedNA.Framework.Utilities
{
    public class Uploader
    {
        public async Task<string> FileUpload(IFormFile file,string filePath, int fileSize = 2048000)
        {
            try
            {
                if(file == null || file.Length < 0)
                    return "file invalid!";

                if(file.Length > fileSize)
                    return $"file size to long, file size must be {double.Parse(FormatFileSize(fileSize,false,false))}";

                var ext = Path.GetExtension(file.FileName);

                if(!AllowedFileTypes().Contains(ext))
                    return "file type not support";

                string randomFileName = Path.GetRandomFileName() + file.FileName;

                if(!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                string path = Path.Combine(filePath, randomFileName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return randomFileName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetContentType(string filePath)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".ico","image/x-icon" }
            };
        }

        private List<string> AllowedFileTypes()
        {
            var type = new List<string>();
            type.Add(".txt");
            type.Add(".pdf");
            type.Add(".doc");
            type.Add(".docx");
            type.Add(".xls");
            type.Add(".xlsx");
            type.Add(".png");
            type.Add(".jpg");
            type.Add(".jpeg");
            type.Add(".gif");
            type.Add(".csv");
            type.Add(".ico");
            return type;
        }

        public static string FormatFileSize(double bytes, bool FarsiResult = true, bool ShortResult = false)
        {
            string str;
            string str2;
            string str3;
            string str4;
            double num;
            if(FarsiResult)
            {
                if(ShortResult)
                {
                    str4 = " ب";
                    str2 = " ک.ب";
                    str = " م.ب";
                    str3 = " گ.ب";
                }
                else
                {
                    str4 = " بایت";
                    str2 = " کیلوبایت";
                    str = " مگابایت";
                    str3 = " گیگابایت";
                }
            }
            else
            {
                str4 = " b";
                str2 = " kb";
                str = " mb";
                str3 = " gb";
            }
            if(bytes > 1073741824.0)
            {
                num = bytes / Math.Pow(1024.0, 3.0);
                return ($"{num.ToString("N2", CultureInfo.InvariantCulture):#,#}" + str3);
            }
            if(bytes > 1048576.0)
            {
                num = bytes / Math.Pow(1024.0, 2.0);
                return ($"{num.ToString("N2", CultureInfo.InvariantCulture):#,#}" + str);
            }
            if(bytes > 1024.0)
            {
                num = bytes / 1024.0;
                return ($"{num.ToString("N2", CultureInfo.InvariantCulture):#,#}" + str2);
            }
            return ($"{bytes.ToString("N2", CultureInfo.InvariantCulture):#,#}" + str4);
        }
    }
}
