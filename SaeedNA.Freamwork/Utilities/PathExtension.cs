﻿using System.IO;

namespace SaeedNA.Application.Utilities
{
    public static class PathExtension
    {
        public static string UploadPath = "/uploads/";
        public static string UploadPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/");
    }
}