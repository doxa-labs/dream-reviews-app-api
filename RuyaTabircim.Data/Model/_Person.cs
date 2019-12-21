using MongoDB.Bson.Serialization.Attributes;
using Repository.Mongo;

namespace RuyaTabircim.Data.Model
{
    public class Person : Entity
    {
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }

    public class RequestLogin
    {
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
