using Microsoft.AspNetCore.Http;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Middwares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; 
            var result = JsonConvert.SerializeObject(
                new ServiceResult
                {
                    Data = new { debMsg = ex.Message,
                    cusMsg = "Có lỗi xảy ra vui lòng liên hệ MISA"
                    },
                    Messenger = "Có lỗi xảy ra vui lòng liên hệ MISA",
                    MISACode = MISACode.Exception
                }) ;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
