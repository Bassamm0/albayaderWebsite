using System.Net;
using static DAL.DALException;

namespace API
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainNotFundException e)
            {

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.WriteAsync(e.Message);
            }
            catch (DomainValidationFundException e)
            {

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.WriteAsync(e.Message);
            }
            catch (DomainExpiredException e)
            {

                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                context.Response.WriteAsync(e.Message);
            }
            catch (DomainInternalException e)
            {

                context.Response.StatusCode = 500;
                context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {

                context.Response.StatusCode = 500;
                context.Response.WriteAsync(e.Message);
            }
        }
    }
}
