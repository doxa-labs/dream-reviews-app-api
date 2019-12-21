using Repository.Mongo;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;

namespace RuyaTabircim.Data.Repository
{
    public class DreamRepository : Repository<Dream>, IDreamRepository
    {
        public DreamRepository(string connectionString) : base(connectionString, "dreams")
        {
        }
    }
}
