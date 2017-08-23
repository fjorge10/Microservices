using HomeInsurance.Users.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using User = HomeInsurance.Users.API.Models.Users;


namespace HomeInsurance.Users.API.Controllers
{
    //Tutorial: http://www.dotnetcurry.com/aspnet-mvc/1267/using-mongodb-nosql-database-with-aspnet-webapi-core


    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersAPIController : Controller
    {
        DataAccess dataAccess;

        public UsersAPIController(DataAccess dataaccess)
        {
            dataAccess = dataaccess;
        }

        //GET: api/UsersAPI
       [HttpGet]
        public IEnumerable<User> Get()
        {
            return dataAccess.GetUsers();
        }


        [HttpGet("GetUser")]
        public User GetUser(int id)
        {
            User user = dataAccess.GetUsers().FirstOrDefault();
            return user;
        }

        //http://localhost:56091/api/Users/Login?username=jorge&password=qwerty
        [HttpGet("Login")]
        public IActionResult Login(string userName,string password)
        {
            User user = dataAccess.Login(userName, password);

            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody]User user)
        {
            User newuser = dataAccess.Create(user);

            return new ObjectResult(newuser);
        }

        [HttpDelete("{Id}")]
        [Route("delete")]
        public IActionResult Delete(string Id)
        {
            var user = dataAccess.GetUserById(Convert.ToInt32(Id));

            if (user == null)
                return NotFound();

            dataAccess.Delete(user.Id);

            return new OkResult();
        }
    }
}
