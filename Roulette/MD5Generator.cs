using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Security;

namespace Roulette
{
    public class MD5Generator
    {

        public string GenerateMd5(SecureString value)
        {
            var simpliString = value.ConvertSecureStringToNormalString();

            try
            {
                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(simpliString);
                var hash = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}

