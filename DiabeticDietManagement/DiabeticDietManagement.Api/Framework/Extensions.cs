using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
