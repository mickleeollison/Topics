using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Core.Utilities
{
    public class SaltManager
    {
        public static string GetSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[1024];

            rng.GetBytes(buffer);
            return BitConverter.ToString(buffer);
        }
    }
}
