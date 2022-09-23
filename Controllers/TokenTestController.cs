using System.Web.Http;

namespace TaskManagement.Controllers
{
    public class TokenTestController : ApiController
    {
        [Authorize]
        public IHttpActionResult Authorize()
        {
            return Ok("Authorized");
        }
    }
}
