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
        public DateTime ExpirationDateTime { get; set; }
        public bool DestroyAfterReading { get; set; }

        public NoteModel(PublishNoteViewModel viewModel)
        {
            Id = ObjectId.GenerateNewId();
            Data = viewModel.Data;
            ExpirationDateTime = viewModel.ExpirationDatetime;
            DestroyAfterReading = viewModel.DestroyAfterReading;
        }
    }
}