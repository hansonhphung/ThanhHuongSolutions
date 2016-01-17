using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Entity
{
    public class MDTrackingNumberGenerator
    {
        public MDTrackingNumberGenerator() { }

        public MDTrackingNumberGenerator(string id, ObjectType objectType, string prefix, int ordinal)
        {
            Id = id;
            ObjectType = objectType;
            Prefix = prefix;
            Ordinal = ordinal;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public ObjectType ObjectType { get; set; }

        public string Prefix { get; set; }

        public int Ordinal { get; set; }
    }
}
