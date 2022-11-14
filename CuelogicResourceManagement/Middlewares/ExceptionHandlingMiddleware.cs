using Dbmodel;
using Entities;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using NLog;
using System.Net;
using System.Text.Json;

namespace CuelogicResourceManagement.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
       



        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger )
        {
            _next = next;
            _logger = logger;
            
        }

       

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }



        async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var errorResponse = new ApiResponse<bool>
            {
                Success = false
            };
            switch (exception)
            {
                case ApplicationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;

                case MySqlException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Db Error";
                    break;


                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal server error!";
                    break;
            }
            var result = JsonSerializer.Serialize(errorResponse);
            _logger.LogError(exception,exception.Message);
            await context.Response.WriteAsync(result);
        }
    }
}
