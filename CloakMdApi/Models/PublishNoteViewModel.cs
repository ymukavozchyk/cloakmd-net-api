namespace CloakMdApi.Models
{
    public class PublishNoteViewModel
    {
        public string Data { get; set; }
        public bool DestroyAfterReading { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Data);
        }
    }
}