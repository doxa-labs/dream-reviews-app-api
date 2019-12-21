using RuyaTabircim.Data.Helpers;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Data.Service
{
    public class OracleService : IOracleService
    {
        IOracleRepository Repository { get; }
        public OracleService(IOracleRepository repository)
        {
            Repository = repository;
        }

        public Oracle Login(RequestLogin value)
        {
            var oracle = Repository.First(s => s.Username == value.Username);
            if (oracle == null)
            {
                return Register(value);
            }
            else
            {
                if (oracle.IsActive == true && oracle.Password == Cryptor.MD5Hash(value.Password))
                {
                    oracle.Password = null;
                    return oracle;
                }
                else
                {
                    return new Oracle();
                }
            }
        }

        public Oracle Register(RequestLogin value)
        {
            Oracle s = new Oracle();
            s.Username = value.Username;
            s.Password = Cryptor.MD5Hash(value.Password);
            s.IsActive = false;

            Repository.Insert(s);

            return Login(value);
        }
    }
}
