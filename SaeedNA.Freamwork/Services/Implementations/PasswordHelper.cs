using System;
using System.Security.Cryptography;
using System.Text;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class PasswordHelper:IPasswordHelper
    {
        public string EncodePasswordMd5(string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5 = MD5.Create();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}