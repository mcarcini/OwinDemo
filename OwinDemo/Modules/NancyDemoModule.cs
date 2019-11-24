using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace OwinDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule() {

            this.RequiresMSOwinAuthentication();

            Get( "/nancy", x =>
            {
                var env = Context.GetOwinEnvironment();
                var user = Context.GetMSOwinUser();
                return "Hello from Nancy! you requested: " + env["owin.RequestPath"] + "<br/><br/> User: " + user.Identity.Name;
            });
        }
    }
}