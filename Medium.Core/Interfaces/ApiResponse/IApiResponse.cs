using System.Net;

namespace Medium.Core.Interfaces.ApiResponse
{
    public interface IApiResponse<T> //: IApiResponse
    {
        T? Data { get; set; }
        Dictionary<string, List<string>>? Errors { get; set; }
        object? Meta { get; set; }
        HttpStatusCode StatusCode { get; set; }
        bool Succeeded { get; }

    }
    public interface IApiResponse : IApiResponse<object>
    {
    }

}