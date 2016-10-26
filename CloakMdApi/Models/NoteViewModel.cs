namespace CloakMdApi.Models
{
    public class NoteViewModel
    {
        public string Data { get; set; }
        public bool DestroyAfterReading { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Data);
        }
    }
}