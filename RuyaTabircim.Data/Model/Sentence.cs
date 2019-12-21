using Repository.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace RuyaTabircim.Data.Model
{
    public class Sentence : Entity
    {
        [BsonElement("tr")]
        public string Tr { get; set; }
        [BsonElement("en")]
        public string En { get; set; }
        [BsonElement("no")]
        public int No { get; set; }
        [BsonElement("isOk")]
        public bool IsOk { get; set; }
    }

    public class SentenceSummary
    {
        public string tr { get; set; }
        public string en { get; set; }
        public string id { get; set; }
    }

    public class Stats
    {
        public int Done { get; set; }
        public int Left { get; set; }
        public int Total { get; set; }
    }
}
