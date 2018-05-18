using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TreeAttendance.Startup))]
namespace TreeAttendance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
