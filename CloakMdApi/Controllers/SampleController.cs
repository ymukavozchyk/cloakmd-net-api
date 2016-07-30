using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloakMdApi.Controllers
{
    public class SampleController : ApiController
    {
        [HttpGet]
        [Route("api/test/hello")]
        public HttpResponseMessage Hello()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Hello"
            });
        }
    }
}
