namespace TrollMarket.Presentation.Api.ModelDto.ResponseApi
{
    public class ResponseError
    {
        public int SatatusCode { get; set; }
        public string SatatusText { get; set; } = "Succes";
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
