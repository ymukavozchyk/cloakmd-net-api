using System;

namespace CloakMdApi.Models
{
    public class PublishNoteViewModel
    {
        public string Data { get; set; }
        public DateTime ExpirationDatetime { get; set; }
        public bool DestroyAfterReading { get; set; }
    }
}