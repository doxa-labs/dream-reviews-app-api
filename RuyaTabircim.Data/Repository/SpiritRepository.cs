using Repository.Mongo;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;

namespace RuyaTabircim.Data.Repository
{
    public class SpiritRepository : Repository<Spirit>, ISpiritRepository
    {
        public SpiritRepository(string connectionString) : base(connectionString, "spirits")
        {
        }
    }
}
