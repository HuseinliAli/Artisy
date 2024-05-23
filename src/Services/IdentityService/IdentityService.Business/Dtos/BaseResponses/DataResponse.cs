namespace IdentityService.Business.Dtos.BaseResponses
{
    public class DataResponse<T> : SuccessResponse
    {
        public T Data { get; set; }
        public DataResponse(T data)
        {
            Data = data;
        }
    }
}
