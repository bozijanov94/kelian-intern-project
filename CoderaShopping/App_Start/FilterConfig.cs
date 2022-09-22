using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoderaShopping
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomExceptionHandlerFilter());
        }
    }

    public class CustomExceptionHandlerFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.ClearContent();
            filterContext.HttpContext.Response.Write(filterContext.Exception.Message);
        }
    }
}
