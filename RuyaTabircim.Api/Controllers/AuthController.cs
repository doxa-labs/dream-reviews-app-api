using JwtHelpers;
using System.Threading.Tasks;
using RuyaTabircim.Api.Model;
using RuyaTabircim.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    public class AuthController : BaseController
    {
        ISpiritService SpiritService { get; }
        IOracleService OracleService { get; }
        public AuthController(ISpiritService spiritService, IOracleService oracleService)
        {
            SpiritService = spiritService;
            OracleService = oracleService;
        }

        [HttpPost("api/v1/login/spirit")]
        public ReturnLogin LoginSpirit([FromBody]RequestLogin value)
        {
            var spirit = SpiritService.Login(value);
            if (spirit.Username == null)
            {
                return new ReturnLogin() { Token = null };
            }

            var result = InvokeLogin(new Task<Spirit>(() => spirit));
            if (result.Result == 0)
            {
                result.Token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create("same key on startup"))
                                    .AddIssuer("Tabircim.Security.Bearer")
                                    .AddAudience("Tabircim.Security.Bearer")
                                    .AddClaim("SpiritId", spirit.Id)
                                    .Build().Value;
            }
            else
            {
                result.Token = null;
            }

            return result;
        }

        [HttpPost("api/v1/login/oracle")]
        public ReturnLogin LoginOracle([FromBody]RequestLogin value)
        {
            var oracle = OracleService.Login(value);
            if (oracle.Username == null)
            {
                return new ReturnLogin() { Token = null };
            }

            var result = InvokeLogin(new Task<Oracle>(() => oracle));
            if (result.Result == 0)
            {
                result.Token = new JwtTokenBuilder()
                                   .AddSecurityKey(JwtSecurityKey.Create("same key on startup"))
                                   .AddIssuer("Tabircim.Security.Bearer")
                                   .AddAudience("Tabircim.Security.Bearer")
                                   .AddClaim("OracleId", oracle.Id)
                                   .Build().Value;
            }
            else
            {
                result.Token = null;
            }

            return result;
        }
    }
}