using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace SaeedNA.Application.Utilities
{
    public static class UploaderExtension
    {
        public static async Task<FileUploadResult> UploadToServer(this IFormFile file,string fileName, string originalPath, 
            int? width, int? height, int fileSize = 2048000, string thumbPath = null, string deleteFileName = null)
        {
            try
            {
                if(file == null || file.Length < 0) return FileUploadResult.Invalid;
                if(file.Length > fileSize) return FileUploadResult.ToLongFileSize;
                if (!AllowedFileTypes().Contains(Path.GetExtension(file.FileName))) return FileUploadResult.NotSupport;

                if(!Directory.Exists(originalPath)) Directory.CreateDirectory(originalPath);

                if(!string.IsNullOrEmpty(deleteFileName))
                {
                    if(File.Exists(originalPath + deleteFileName)) File.Delete(originalPath + deleteFileName);

                    if(!string.IsNullOrEmpty(thumbPath))
                    {
                        if(File.Exists(thumbPath + deleteFileName)) File.Delete(thumbPath + deleteFileName);
                    }
                }

                string uploadPath = originalPath + fileName;

                using(var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    if(!Directory.Exists(uploadPath)) await file.CopyToAsync(stream);
                }

                if(!string.IsNullOrEmpty(thumbPath))
                {
                    if(!Directory.Exists(thumbPath)) Directory.CreateDirectory(thumbPath);

                    ImageOptimizer resizer = new ImageOptimizer();

                    if(width != null && height != null)
                        resizer.ImageResizer(uploadPath + fileName, thumbPath + fileName, width, height);
                }

                return FileUploadResult.Success;
            }
            catch
            {
                return FileUploadResult.Error;
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

        private static List<string> AllowedFileTypes()
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
        
        public enum FileUploadResult
        {
            Success,
            Error,
            NotSupport,
            ToLongFileSize,
            Invalid
        }
    }
}
