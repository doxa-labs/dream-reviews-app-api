using RuyaTabircim.Data.Model;
using System.Collections.Generic;

namespace RuyaTabircim.Data.Service.Interface
{
    public interface IDreamService
    {
        Dream Get(string id);
        IEnumerable<DreamUser> GetAll();
        IEnumerable<DreamUser> GetForReply();
        IEnumerable<Dream> GetBySpiritId(string id);
        bool Send(RequestDream value);
        bool Reply(RequestDream value);
        bool Watch(string id, string spiritId);
    }
}
