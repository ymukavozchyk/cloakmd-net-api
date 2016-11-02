namespace CloakMdApi.Models
{
    public class ActionResultModel<T>
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public ActionResultModel(bool isSuccessful, string errorMessage, T data)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}