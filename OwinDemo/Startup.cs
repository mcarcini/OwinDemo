using Nancy;
using Owin;
using OwinDemo.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OwinDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app) {

            app.UseDebugMiddleware(new DebugMiddlewareOptions {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }
            });

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new Microsoft.Owin.PathString("/Auth/Login")
            }); ;

            app.Use(async (ctx, next) => {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                    Debug.WriteLine("User: " + ctx.Authentication.User.Identity.Name);
                else
                    Debug.WriteLine("User not Authenticated");
                await next();
            });

            var configApi = new HttpConfiguration();
            configApi.MapHttpAttributeRoutes();
            app.UseWebApi(configApi);


            app.Map("/nancy", mappedBy => { mappedBy.UseNancy(); });

            /*
            app.UseNancy(config => { 
                config.PerformPassThrough = (context => context.Response.StatusCode == HttpStatusCode.NotFound);
            });
            */
            

            /*
            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("Hello World");
            });            
            */
        }
    }
}