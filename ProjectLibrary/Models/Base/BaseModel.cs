using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Models.Base
{
    public abstract class BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public BaseModel()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
