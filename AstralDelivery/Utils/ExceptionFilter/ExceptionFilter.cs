using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
    
namespace AstralDelivery.Utils.ExceptionFilter
{
    /// <inheritdoc />
    public class ExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            var responseMessage = new ResponseMessage(false, context.Exception.Message, context.Exception.ToString());
            if (context.Exception is DbUpdateConcurrencyException || context.Exception is DbUpdateException)
            {
                responseMessage.Message = "Ошибка при запросе к БД";
            }
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(responseMessage);
        }
    }
}