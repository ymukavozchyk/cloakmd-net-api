using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CloakMdApi.BusinessLogic;
using CloakMdApi.Models;

namespace CloakMdApi.Controllers
{
    [RoutePrefix("api/notes")]
    public class NoteController : ApiController
    {
        [HttpPost]
        [Route("publish")]
        public async Task<HttpResponseMessage> PublishNote(PublishNoteViewModel model)
        {
            var result = await DataLayer.PublishNote(new NoteModel(model));
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new
            {
                Message = @"Was not able to publish this note"
            });
        }

        [HttpGet]
        [Route("retrieve/{id}")]
        public async Task<HttpResponseMessage> RetrieveNote(string id)
        {
            var result = await DataLayer.GetNoteById(id);
            if (result != null)
            {
                if (result.ExpirationDateTime >= DateTime.Now.ToUniversalTime())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new RetrieveNoteViewModel
                    {
                        Data = result.Data,
                        DestroyAfterReading = result.DestroyAfterReading
                    });
                }
                DataLayer.DestroyNoteById(id);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Message = @"Note expired"
                });
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, new
            {
                Message = @"Was not able to find this note"
            });
        }

        [HttpDelete]
        [Route("destroy/{id}")]
        public async Task<HttpResponseMessage> DestroyNote(string id)
        {
            var result = await DataLayer.DestroyNoteById(id);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
