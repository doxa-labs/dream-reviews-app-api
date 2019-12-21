using RuyaTabircim.Data.Model;
using System.Collections.Generic;

namespace RuyaTabircim.Data.Service.Interface
{
    public interface ISpiritService
    {
        IEnumerable<Spirit> Get();
        Spirit Get(string id);
        Spirit Login(RequestLogin value);
        Spirit Register(RequestLogin value);
        bool SaveDeviceToken(string id, string token);
    }
}
