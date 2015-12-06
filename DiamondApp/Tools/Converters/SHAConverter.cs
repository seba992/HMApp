using System;
using System.Security.Cryptography;
using System.Text;

namespace DiamondApp.Tools
{
    public static class ShaConverter
    {
        public static string sha256_hash(string value) 
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
