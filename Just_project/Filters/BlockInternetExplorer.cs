using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Just_project.Filters
{
    public class BlockInternetExplorerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();

            if (userAgent.Contains("Trident") || userAgent.Contains("MSIE"))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Internet Explorer is not supported. Please use a modern browser."
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
