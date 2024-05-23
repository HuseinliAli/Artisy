using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Utils
{
    public static class HashHelper
    {
        public static byte[] GetMD5HashBytes(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                return md5.ComputeHash(inputBytes);
            }
        }

        public static bool VerifyMD5Hash(string input, byte[] hash)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] inputHash = md5.ComputeHash(inputBytes);

                for (int i = 0; i < inputHash.Length; i++)
                {
                    if (inputHash[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
