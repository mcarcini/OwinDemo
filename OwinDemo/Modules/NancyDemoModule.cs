using Nancy;
using Nancy.Owin;

namespace OwinDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule() {
            Get( "/nancy", x =>
            {
                var env = Context.GetOwinEnvironment();
                return "Hello from Nancy! you requested: " + env["owin.RequestPath"];
            });
        }
    }
}