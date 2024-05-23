namespace IdentityService.Business.Dtos.BaseResponses
{
    public class ErrorResponse : Response
    {
        public string Message { get; set; }
        public ErrorResponse(string message) : base(false)
        {
            Message = message;
        }
    }
}
