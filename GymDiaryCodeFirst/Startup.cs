using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymDiaryCodeFirst.Startup))]
namespace GymDiaryCodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
