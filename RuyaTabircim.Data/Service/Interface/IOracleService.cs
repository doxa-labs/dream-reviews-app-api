using RuyaTabircim.Data.Model;

namespace RuyaTabircim.Data.Service.Interface
{
    public interface IOracleService
    {
        Oracle Login(RequestLogin value);
        Oracle Register(RequestLogin value);
    }
}
