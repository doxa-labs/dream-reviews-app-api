using MongoDB.Bson.Serialization.Attributes;
using Repository.Mongo;

namespace RuyaTabircim.Data.Model
{
    public class Dream : Entity
    {
        [BsonElement("spiritId")]
        public string SpiritId { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("reply")]
        public string Reply { get; set; }
        [BsonElement("isReplied")]
        public bool IsReplied { get; set; }
        [BsonElement("isWatched")]
        public bool IsWatched { get; set; }
    }

    public class RequestDream
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }

    public class DreamUser : Dream
    {
        [BsonElement("username")]
        public string Username { get; set; }
    }
}
