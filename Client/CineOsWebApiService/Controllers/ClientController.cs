using System.Web.Http;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class ClientController : CineOsController
    {
        [Route("api/auth/exsits/{email}/{password}")]
        [HttpGet]
        public int Exists(string email, string password)
        {
            return CineOsServices.DoesClientExist(email, password);
        }
    }
}