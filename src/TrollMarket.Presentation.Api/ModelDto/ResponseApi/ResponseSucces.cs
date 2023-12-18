namespace TrollMarket.Presentation.Api.ModelDto.ResponseApi
{
    public class ResponseSucces
    {
        public int SatatusCode { get; set; }
        public string SatatusText { get; set; } = "Succes";
        public string Message { get; set; }
        public object? Detail { get; set; }
    }
}
