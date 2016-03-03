using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.DeptManagement.Domain.Entity
{
    class MDDept
    {
        public MDDept() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
    }
}
