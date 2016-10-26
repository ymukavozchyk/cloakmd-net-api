using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CloakMdApi.Models
{
    public class NoteModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Data { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool DestroyAfterReading { get; set; }

        public NoteModel(NoteViewModel viewModel)
        {
            Id = ObjectId.GenerateNewId();
            Data = viewModel.Data;
            CreationDateTime = DateTime.Now;
            DestroyAfterReading = viewModel.DestroyAfterReading;
        }
    }
}