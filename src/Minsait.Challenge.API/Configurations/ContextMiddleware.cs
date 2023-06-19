using Minsait.Challenge.Infra;

namespace Minsait.Challenge.API.Configurations
{
    public class ContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var dbContext = context.RequestServices.GetRequiredService<MerchantContext>();

            await next.Invoke(context);

            await dbContext.SaveChangesAsync();
        }
    }
}
