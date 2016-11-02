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
        [Route("share")]
        public async Task<HttpResponseMessage> ShareNote(NoteViewModel model)
        {
            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Message = @"Note model is null"
                });
            }
            if (model.Validate())
            {
                var result = await DataLayer.StoreNote(new NoteModel(model));
                if (result.IsSuccessful)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, result.Data);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Message = result.ErrorMessage
                });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new
            {
                Message = @"Note model is invalid: note is empty"
            });
        }

        [HttpGet]
        [Route("retrieve/{id}")]
        public async Task<HttpResponseMessage> RetrieveNote(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = await DataLayer.GetNoteById(id);
                if (result.IsSuccessful)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new NoteViewModel
                    {
                        Data = result.Data.Data,
                        DestroyAfterReading = result.Data.DestroyAfterReading
                    });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Message = result.ErrorMessage
                });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new
            {
                Message = @"Id of the note is either null or empty"
            });
        }

        [HttpDelete]
        [Route("destroy/{id}")]
        public async Task<HttpResponseMessage> DestroyNote(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = await DataLayer.DestroyNoteById(id);
                if (result.IsSuccessful)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Message = result.ErrorMessage
                });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new
            {
                Message = @"Id of the note is either null or empty"
            });
        }
    }
}
