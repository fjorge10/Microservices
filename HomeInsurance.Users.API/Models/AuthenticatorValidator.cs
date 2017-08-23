using HomeInsurance.Users.API.Interfaces;
using SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInsurance.Users.API.Models
{
    public static class AuthenticatorValidator

    {
        public static Users ChangePassword(Users user)
        {
            throw new NotImplementedException();
        }

        public static bool HasPermission(string userName, params string[] permission)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compare MD5 Hasch
        /// </summary>
        /// <param name="userpassword"></param>
        /// <param name="databasepassword"></param>
        /// <returns></returns>
        public static bool MD5HashCompare(string userpassword, string databasepassword)
        {
           MD5 md5 = new MD5();

           byte[] userpas = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(GenerateMD5(userpassword)));

           byte[] datapass = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(databasepassword));

            for (int i = 0; i < userpas.Length; i++)
            {
                if (userpas[i] != datapass[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Generate a MD5 password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GenerateMD5(string password)
        {
            MD5 md5 = new MD5();

            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
