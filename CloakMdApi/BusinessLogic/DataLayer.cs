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

        public static async Task<NoteModel> GetNoteById(string id)
        {
            var notes = Db.GetCollection<NoteModel>("notes");
            var filter = Builders<NoteModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await notes.Find(filter).ToListAsync();
            return result.FirstOrDefault();
        }

        public static async Task<string> StoreNote(NoteModel model)
        {
            var notes = Db.GetCollection<NoteModel>("notes");
            await notes.InsertOneAsync(model);
            return model.Id.ToString();
        }

        public static async Task<bool> DestroyNoteById(string id)
        {
            var notes = Db.GetCollection<NoteModel>("notes");
            var filter = Builders<NoteModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await notes.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }
    }
}