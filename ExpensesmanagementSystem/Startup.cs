using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpensesmanagementSystem.Startup))]
namespace ExpensesmanagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
