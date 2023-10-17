using System.Net;

namespace Medium.Core.Interfaces.ApiResponse
{
    public interface IApiResponse<T> : IApiResponse
    {
        new T? Data { get; set; }

    }
    public interface IApiResponse
    {
        object? Data { get; set; }
        Dictionary<string, List<string>>? Errors { get; set; }
        object? Meta { get; set; }
        HttpStatusCode StatusCode { get; set; }
        bool Succeeded { get; }
    }

}