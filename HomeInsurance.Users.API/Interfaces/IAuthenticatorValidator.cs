using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = HomeInsurance.Users.API.Models.Users;

namespace HomeInsurance.Users.API.Interfaces
{
   public interface IAuthenticatorValidator
    {
        bool ValidateUser(string userName, string password);

        bool MD5HashCompare(string userpass, string databasepass);

        User ChangePassword(User user);

        bool HasPermission(string userName, params string[] permission);
    }
}
