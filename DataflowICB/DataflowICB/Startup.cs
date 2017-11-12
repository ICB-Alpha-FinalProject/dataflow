using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataflowICB.Startup))]
namespace DataflowICB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}