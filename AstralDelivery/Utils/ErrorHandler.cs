using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AstralDelivery.Utils
{
    public class ErrorHandler : IExceptionFilter
    {

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

    public class ResponseMessage
    {
        public bool Сompleted { get; set; }
        public string Message { get; set; }
        public string Raw { get; set; }

        public ResponseMessage()
        {

        }

        public ResponseMessage(bool complited, string message, string raw)
        {
            Сompleted = complited;
            Message = message;
            Raw = raw;
        }
    }
}