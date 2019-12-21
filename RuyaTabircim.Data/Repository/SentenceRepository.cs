using Repository.Mongo;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;

namespace RuyaTabircim.Data.Repository
{
    public class SentenceRepository : Repository<Sentence>, ISentenceRepository
    {
        public SentenceRepository(string connectionString) : base(connectionString, "sentences")
        {
        }
    }
}
