using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Extentions
{
    public static class GeneratorExtention
    {
        public static int GenerateUniqueId(this int minValue)
        {
            int result = new Random().Next(minValue, 999999999);
            return result;
        }

        public static string GenerateUniqueCode()
        {
            Random generator = new Random();
            string res = generator.Next(0, 999999).ToString("D6");
            return res;
        }
    }
}
