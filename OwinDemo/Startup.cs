﻿using Nancy;
using Owin;
using OwinDemo.Middleware;
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

            app.UseNancy(config => { 
                config.PerformPassThrough = (context => context.Response.StatusCode == HttpStatusCode.NotFound);
            });

            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("Hello World");
            });            
        }
    }
}