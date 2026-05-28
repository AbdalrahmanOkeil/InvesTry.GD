using Investry.API.Common;

namespace Investry.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Errors = new List<ApiError>
                    {
                        new ApiError
                        {
                            Code = "Server.Error",
                            Message = ex.Message,
                            Details = ex.StackTrace
                        }
                    }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
