namespace Medium.BL.ResponseHandler
{
    public static class ApiResponseHandler
    {
        public static ApiResponse<T> Deleted<T>(T entity)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        public static ApiResponse<T> Success<T>(T data, object? Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = data,
                StatusCode = System.Net.HttpStatusCode.OK,
                Meta = Meta
            };
        }
        public static ApiResponse<T> UnAuthorized<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
            };
        }
        public static ApiResponse<T> BadRequest<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }

        public static ApiResponse<T> UnprocessableEntity<T>(T entity)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Data = entity,
            };
        }

        public static ApiResponse<T> NotFound<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }

        public static ApiResponse<T> Created<T>(T entity, object? Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Meta = Meta
            };
        }
        public static ApiResponse<T> NoContent<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
            };
        }
    }

}
