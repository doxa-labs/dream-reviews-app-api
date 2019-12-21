using Repository.Mongo;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;

namespace RuyaTabircim.Data.Repository
{
    public class OracleRepository : Repository<Oracle>, IOracleRepository
    {
        public OracleRepository(string connectionString) : base(connectionString, "oracles")
        {
        }
    }
}
