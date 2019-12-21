using MongoDB.Bson.Serialization.Attributes;

namespace RuyaTabircim.Data.Model
{
    public class Spirit : Person
    {
        [BsonElement("deviceToken")]
        public string DeviceToken { get; set; }
    }
}
