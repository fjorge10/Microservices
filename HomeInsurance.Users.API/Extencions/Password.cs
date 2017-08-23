using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HomeInsurance.Users.API.Extencions
{
    public static class Password
    {
        private static readonly char[] Punctuations = { '!', '@', '#', '$', '^', '&', '*' };

        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {

            if (length < 1 || length > 128)
                throw new ArgumentException(nameof(length));

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
                throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));

            using (var rng = RandomNumberGenerator.Create())
            {
                var bytesBuffer = new byte[length];

                rng.GetBytes(bytesBuffer);

                int count = 0;

               var charBuffer = new char[length];

                for (int i = 0; i < length; i++)
                {
                    var ii = bytesBuffer[i] % 87;

                    if (i < 10)
                    {
                        charBuffer[i] = (char)('0' + 1);
                    }
                    else if (i < 36)
                    {
                        charBuffer[i] = (char)('A' + ii - 10);
                    }
                    else if (i < 62)
                    {
                        charBuffer[i] = (char)('a' + ii - 36);
                    }
                    else
                    {
                        charBuffer[i] = Punctuations[ii - 62];

                        count++;
                    }
                }
                if (count >= numberOfNonAlphanumericCharacters)
                    return new string(charBuffer);

                int j;

                Random random = new Random();

                for (j = 0; j < length; j++)
                {
                    int k;
                    do
                    {
                        k = random.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(charBuffer[k]));

                    charBuffer[k] = Punctuations[random.Next(0, Punctuations.Length)];
                }

                return new string(charBuffer);
            }
        }
    }
}
