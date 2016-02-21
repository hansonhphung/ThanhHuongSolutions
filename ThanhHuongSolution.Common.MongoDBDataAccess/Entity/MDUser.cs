using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Entity
{
    public class MDUser
    {
        public MDUser() { }

        public MDUser(string id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
