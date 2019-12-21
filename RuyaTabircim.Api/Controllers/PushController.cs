using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RuyaTabircim.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Api.Controllers
{
    [Produces("application/json")]
    public class PushController : BaseController
    {
        ISpiritService SpiritService { get; }
        public PushController(ISpiritService spiritService)
        {
            SpiritService = spiritService;
        }

        private static readonly HttpClient client = new HttpClient();

        [Authorize(Policy = "Spirit")]
        [HttpPost("api/v1/push")]
        public async Task<IActionResult> PushAsync([FromBody]Push value)
        {
            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            Spirit user = SpiritService.Get(dict["SpiritId"]);
            if (user != null && user.DeviceToken != null && value.Title != null && value.Body != null)
            {
                try
                {
                    var values = new Dictionary<string, string> {
                                    { "to", user.DeviceToken },
                                    { "title", value.Title },
                                    { "body", value.Body }};

                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://exp.host/--/api/v2/push/send", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Ok(responseString);
                }
                catch
                {
                    return NotFound("ex");
                }
            } else
            {
                return NotFound();
            }
        }
    }
}