using System;
using System.Security.Cryptography;
using System.Text;

namespace Security
{
    public static class L2Security
    {
        public static string HashPassword(string password)
        {
            using (SHA1Managed crypt = new SHA1Managed())
            {
                return Convert.ToBase64String(crypt.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}