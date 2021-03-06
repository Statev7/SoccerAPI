namespace SoccerAPI.Infrastructure.Middlewares
{
	using System;
    using System.Linq;
    using System.Net;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;

	using SoccerAPI.Common.ErrorDetails;
    using SoccerAPI.Common.Exeptions;

    public class ExceptionMiddleware 
    {
		private readonly RequestDelegate next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			ErrorDetails errorDetails = new ErrorDetails();
			errorDetails.Message = exception.Message;

			switch (exception)
            {
				case ModelException:
					ModelException modelException = exception as ModelException;
					errorDetails.Message = modelException.ErrorsMessage.Select(e => e.ErrorMessage);
                    break;
				case EntityDoesNotExistException:
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				case InvalidPropertyDateException:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				case UnauthorizedAccessCustomException:
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					break;
			}

			errorDetails.StatusCode = context.Response.StatusCode;

			await context.Response.WriteAsync(errorDetails.ToString());
		}
	}
}
