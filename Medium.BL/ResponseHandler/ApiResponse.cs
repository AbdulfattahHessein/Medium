using Medium.Core.Interfaces.ApiResponse;
using System.Net;

namespace Medium.BL.ResponseHandler
{
    public class ApiResponse<T> : ApiResponse, IApiResponse<T>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(T data, string? meta = null)
        {
            Data = data;
            Meta = meta;
        }

        public new T? Data { get; set; }
    }
    public class ApiResponse : IApiResponse
    {
        public object? Data { get; set; }
        public bool Succeeded => (int)StatusCode >= 200 && (int)StatusCode <= 290;
        public HttpStatusCode StatusCode { get; set; }
        public object? Meta { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }

    }

}
