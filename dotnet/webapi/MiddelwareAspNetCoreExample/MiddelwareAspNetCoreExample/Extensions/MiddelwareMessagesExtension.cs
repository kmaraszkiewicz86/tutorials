using System;
using Microsoft.AspNetCore.Http;
using MiddelwareAspNetCoreExample.Models;

namespace MiddelwareAspNetCoreExample.Extensions
{
    public static class MiddelwareMessagesExtension
    {
        public static MiddlewareModel GetMiddlewareModel (this HttpContext httpContext)
        {
            if (httpContext.Items.ContainsKey(nameof(MiddlewareModel)))
            {
                return httpContext.Items[nameof(MiddlewareModel)] as MiddlewareModel;
            }

            throw new Exception("No middelware model found!");
        }

        public static void AddNewMiddlewareMessage(this HttpContext httpContext, string newMessage)
        {
            MiddlewareModel middlewareModel = httpContext.GetMiddlewareModelFromHttpContextOrReturnDefault();

            middlewareModel.AddNewMessageWithStepNumberIncrementations(newMessage);

            httpContext.Items[nameof(MiddlewareModel)] = middlewareModel;
        }

        private static MiddlewareModel GetMiddlewareModelFromHttpContextOrReturnDefault(this HttpContext httpContext)
        {
            if (!httpContext.Items.ContainsKey(nameof(MiddlewareModel)))
            {
                return new MiddlewareModel();
            }

            return (MiddlewareModel)httpContext.Items[nameof(MiddlewareModel)];
        }
    }
}
