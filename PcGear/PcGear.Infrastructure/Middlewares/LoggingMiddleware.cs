﻿using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace PcGear.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        readonly RequestDelegate next;
        private Stopwatch stopwatch { get; set; }

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(context.Request.Path);

            stopwatch = Stopwatch.StartNew();
            await next(context);
            stopwatch.Stop();

            Console.WriteLine(context.Request.Path + " - " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}