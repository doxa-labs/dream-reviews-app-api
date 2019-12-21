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
    public class SpiritsController : BaseController
    {
        ISpiritService SpiritService { get; }
        public SpiritsController(ISpiritService spiritService)
        {
            SpiritService = spiritService;
        }

        [Authorize]
        [HttpGet("api/v1/spirits")]
        public Return Get()
        {
            return Invoke(new Task<IEnumerable<Spirit>>(() => SpiritService.Get()));
        }

        [Authorize(Policy = "Spirit")]
        [HttpGet("api/v1/spirits/profile")]
        public Return Profile()
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            return Invoke(new Task<Spirit>(() => SpiritService.Get(dict["SpiritId"])));
        }

        [Authorize(Policy = "Spirit")]
        [HttpPut("api/v1/spirits/savetoken")]
        public Return SaveToken([FromBody]RequestToken value)
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            return Invoke(new Task<bool>(() => SpiritService.SaveDeviceToken(dict["SpiritId"], value.Token)));
        }
    }
}