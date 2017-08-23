using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeInsurance.Users.API.Models
{
    public class Users
    {
        public ObjectId Id { get; set; } //To mapp the CLR object 

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("FirstName")] //BsonElement - represents the mapped property with the MongoDB collection
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("NIF")]
        public int NIF { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Role")]
        public int Role { get; set; }
    }
}
