using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CloakMdApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CloakMdApi.BusinessLogic
{
    public static class DataLayer
    {
        private static readonly IMongoDatabase Db;

        static DataLayer()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["DB_CONNECTION_STRING"]);
            Db = client.GetDatabase(ConfigurationManager.AppSettings["DB_NAME"]);
        }

        public static async Task<ActionResultModel<NoteModel>> GetNoteById(string id)
        {
            ObjectId parsedId;
            var parseResult = ObjectId.TryParse(id, out parsedId);
            if (!parseResult)
            {
                return new ActionResultModel<NoteModel>(false, @"Was not able to parse note Id", null);
            }
            try
            {
                var notes = Db.GetCollection<NoteModel>("notes");
                var filter = Builders<NoteModel>.Filter.Eq("_id", parsedId);
                var result = await notes.Find(filter).ToListAsync();
                var returnedNote = result.FirstOrDefault();
                if (returnedNote == null)
                {
                    return new ActionResultModel<NoteModel>(false, @"Was not able to find note", null);
                }
                return new ActionResultModel<NoteModel>(true, null, returnedNote);
            }
            catch (Exception ex)
            {
                return new ActionResultModel<NoteModel>(false, ex.Message, null);
            }
        }

        public static async Task<ActionResultModel<string>> StoreNote(NoteModel model)
        {
            try
            {
                var notes = Db.GetCollection<NoteModel>("notes");
                await notes.InsertOneAsync(model);
                return new ActionResultModel<string>(true, null, model.Id.ToString());
            }
            catch (Exception ex)
            {
                return new ActionResultModel<string>(false, ex.Message, null);
            }
        }

        public static async Task<ActionResultModel<bool>> DestroyNoteById(string id)
        {
            ObjectId parsedId;
            var parseResult = ObjectId.TryParse(id, out parsedId);
            if (!parseResult)
            {
                return new ActionResultModel<bool>(false, @"Was not able to parse note Id", false);
            }
            try
            {
                var notes = Db.GetCollection<NoteModel>("notes");
                var filter = Builders<NoteModel>.Filter.Eq("_id", parsedId);
                await notes.DeleteOneAsync(filter);
                return new ActionResultModel<bool>(true, null, true);
            }
            catch (Exception ex)
            {
                return new ActionResultModel<bool>(false, ex.Message, false);
            }
        }
    }
}