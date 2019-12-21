using MongoDB.Driver;
using RuyaTabircim.Data.Helpers;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;
using RuyaTabircim.Data.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace RuyaTabircim.Data.Service
{
    public class SpiritService : ISpiritService
    {
        ISpiritRepository Repository { get; }
        public SpiritService(ISpiritRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<Spirit> Get()
        {
            var list = Repository.FindAll().ToList();
            list.Reverse();
            return list;
        }

        public Spirit Get(string id)
        {
            Spirit spirit = Repository.First(s => s.Id == id);
            spirit.Password = null;
            return spirit;
        }

        //todo not completed
        public Spirit Login(RequestLogin value)
        {
            var spirit = Repository.First(s => s.Username == value.Username);

            if (spirit == null)
            {
                return Register(value);
            }
            else
            {
                if (spirit.IsActive == true && spirit.Password == Cryptor.MD5Hash(value.Password))
                {
                    spirit.Password = null;
                    return spirit;
                }
                else
                {
                    return new Spirit();
                }
            }
        }

        public Spirit Register(RequestLogin value)
        {
            Spirit s = new Spirit();
            s.Username = value.Username;
            s.Password = Cryptor.MD5Hash(value.Password);
            s.IsActive = true;

            Repository.Insert(s);

            return Login(value);
        }

        public bool SaveDeviceToken(string id, string token)
        {
            var filter = Builders<Spirit>.Filter.Where(s => s.Id == id);
            var update = Builders<Spirit>.Update.Set(d => d.DeviceToken, token);
            return Repository.Update(filter, update);
        }
    }
}
