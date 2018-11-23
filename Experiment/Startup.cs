using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Experiment.Startup))]
namespace Experiment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
