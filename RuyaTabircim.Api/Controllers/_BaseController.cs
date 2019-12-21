using RuyaTabircim.Api.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RuyaTabircim.Api.Controllers
{
    public class BaseController : Controller
    {
        protected Return Invoke<T>(Task<T> action)
        {
            Return response = new Return();
            try
            {
                action.RunSynchronously();
                response.Data = action.Result;

                if (response.Data == null)
                {
                    response.SetMessage(Level.MissingData, "MissingData");
                } else
                {
                    response.SetMessage(Level.Success, "Success");
                }
            }
            catch
            {
                response.SetMessage(Level.Error, "SystemFailure");
            }
            return response;

        }

        protected ReturnLogin InvokeLogin<T>(Task<T> action)
        {
            ReturnLogin response = new ReturnLogin();
            try
            {
                action.RunSynchronously();
                response.SetMessage(Level.Success, "Success");
            }
            catch
            {
                response.SetMessage(Level.Error, "SystemFailure");
            }
            return response;
        }
    }
}