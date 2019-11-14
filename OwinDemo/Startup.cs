using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OwinDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app) {

            app.Use(async (ctx, next) =>
            {
                Debug.WriteLine("Incoming Request: " + ctx.Request.Path);
                await next();
                Debug.WriteLine("Outgoing Request: " + ctx.Request.Path);
            });

            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("Hello World");
            });            
        }
    }
}