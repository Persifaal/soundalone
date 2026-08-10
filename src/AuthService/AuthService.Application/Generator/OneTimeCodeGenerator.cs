using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Generator
{
    public static class OneTimeCodeGenerator
    {
        public static string Generate() 
        {
            return RandomNumberGenerator.GetInt32(0, 1_000_000).ToString();
        }
    }
}
