using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondApp.Tools
{
    class Validations
    {
        public static string FirstLetterToLowerCase(string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("Błąd!");
            return value.First().ToString().ToLower() + value.Substring(1);
        }
    }
}
