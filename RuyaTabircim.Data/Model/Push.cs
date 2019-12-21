using MongoDB.Bson.Serialization.Attributes;

namespace RuyaTabircim.Data.Model
{
    public class Push
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class RequestToken
    {
        public string Token { get; set; }
    }
}
