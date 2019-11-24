using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace OwinDemo.Middleware
{
    public class DebugMiddleware
    {
        AppFunc _next;
        public DebugMiddleware(AppFunc next) {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment) {
            var ctx = new OwinContext(environment);

            Debug.WriteLine("Incoming Request: " + ctx.Request.Path);
            await _next(environment);
            Debug.WriteLine("Outgoing Request: " + ctx.Request.Path);
        }

    }
}