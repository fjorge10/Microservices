using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using System;
using HomeInsurance.Users.API.Extencions;

namespace HomeInsurance.Users.API.Models
{
    public class DataAccess
    {
        MongoClient mongoClient;

        IMongoDatabase mongoDatabaseBase;

        public DataAccess()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");

            mongoDatabaseBase = mongoClient.GetDatabase("HomeInsurance");
        }

        public IEnumerable<Users> GetUsers()
        {
            return mongoDatabaseBase.GetCollection<Users>("Users").Find(new BsonDocument()).ToList();
        }

        public Users GetUser(ObjectId id)
        {
            return mongoDatabaseBase.GetCollection<Users>("Users").Find(x => x.Id == id).FirstOrDefault();
        }

        public Users GetUserById(int id)
        {
            return mongoDatabaseBase.GetCollection<Users>("Users").Find(x => x.UserId == id).FirstOrDefault();
        }

        public Users Create(Users user)
        {
            Users userModel = new Users();

            userModel.Password = GeneratePass();
            userModel.UserId = GenerateUserId();
            userModel.UserName = GenerateUserName(user.FirstName, user.LastName);

            user.Password = AuthenticatorValidator.GenerateMD5(userModel.Password);
            user.UserName = userModel.UserName;
            user.UserId = userModel.UserId;
            mongoDatabaseBase.GetCollection<Users>("Users").InsertOne(user);

            return userModel;
        }

        public Users Login(string username, string password)
        {
            Users user = mongoDatabaseBase.GetCollection<Users>("Users").Find(x => x.UserName == username).FirstOrDefault();

            if (user != null && !string.IsNullOrEmpty(password))
            {
                if (AuthenticatorValidator.MD5HashCompare(password, user.Password))
                    return user;

                return null;
            }
            return null;
        }

        public void Delete(ObjectId id)
        {
            mongoDatabaseBase.GetCollection<Users>("Users").DeleteOne(a => a.Id == id);
        }

        public string GenerateUserName(string firstName, string lastName)
        {
            Random random = new Random();
            string mystring = string.Concat(firstName.ToLower(), lastName.ToLower());

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string firstRandom = new string( Enumerable.Repeat(chars, random.Next(1, 4))
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string lastRandom = new string(Enumerable.Repeat(chars, random.Next(1, 4))
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return string.Concat(firstRandom, mystring, lastRandom);
        }

        private string GeneratePass()
        {
            return Password.GeneratePassword(8, 2);
        }

        private int GenerateUserId()
        {
            List<Users> users = GetUsers().OrderByDescending(x=>x.UserId).ToList();

            int lastDBId = users.Select(x => x.UserId).FirstOrDefault();

            return lastDBId = lastDBId + 1;
        }
    }
}
