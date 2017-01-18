using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleManagerContact.portal.Startup))]
namespace SimpleManagerContact.portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
