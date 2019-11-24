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
        DebugMiddlewareOptions options;
        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options) {
            _next = next;
            this.options = options;
            if (options.OnIncomingRequest == null)
                this.options.OnIncomingRequest = (ctx) => { Debug.WriteLine("Incoming Request: " + ctx.Request.Path); };
            
            if (options.OnOutgoingRequest == null)
                this.options.OnOutgoingRequest = (ctx) => { Debug.WriteLine("Outgoing Request: " + ctx.Request.Path); };

        }

        public async Task Invoke(IDictionary<string, object> environment) {
            var ctx = new OwinContext(environment);
            options.OnIncomingRequest(ctx);
            await _next(environment);
            options.OnOutgoingRequest(ctx);

        }

    }
}