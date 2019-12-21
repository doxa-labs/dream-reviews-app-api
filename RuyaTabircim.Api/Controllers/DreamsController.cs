using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuyaTabircim.Api.Model;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Api.Controllers
{
    [Produces("application/json")]
    public class DreamsController : BaseController
    {
        IDreamService DreamService { get; }
        public DreamsController(IDreamService dreamService)
        {
            DreamService = dreamService;
        }

        [Authorize(Policy = "Oracle")]
        [HttpGet("api/v1/dreams")]
        public Return GetForReply()
        {
            return Invoke(new Task<IEnumerable<DreamUser>>(() => DreamService.GetForReply()));
        }

        [Authorize(Policy = "Spirit")]
        [HttpGet("api/v1/dreams/byspirit")]
        public Return GetBySpiritId()
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            return Invoke(new Task<IEnumerable<Dream>>(() => DreamService.GetBySpiritId(dict["SpiritId"])));
        }

        [Authorize]
        [HttpGet("api/v1/dreams/{id}")]
        public Return Get(string id)
        {
            return Invoke(new Task<Dream>(() => DreamService.Get(id)));
        }

        [Authorize(Policy = "Oracle")]
        [HttpGet("api/v1/dreams/all")]
        public Return GetAll()
        {
            return Invoke(new Task<IEnumerable<DreamUser>>(() => DreamService.GetAll()));
        }

        [Authorize(Policy = "Spirit")]
        [HttpPost("api/v1/dreams/send")]
        public Return Send([FromBody]RequestDream value)
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));
            value.Id = dict["SpiritId"];

            return Invoke(new Task<bool>(() => DreamService.Send(value)));
        }

        [Authorize(Policy = "Oracle")]
        [HttpPut("api/v1/dreams/reply")]
        public Return Reply([FromBody]RequestDream value)
        {
            return Invoke(new Task<bool>(() => DreamService.Reply(value)));
        }

        [Authorize(Policy = "Spirit")]
        [HttpPut("api/v1/dreams/watch")]
        public Return Watch([FromBody]RequestDream value)
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            return Invoke(new Task<bool>(() => DreamService.Watch(value.Id, dict["SpiritId"])));
        }
    }
}