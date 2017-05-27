using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exemplo_PDF_e_ZIP.Startup))]
namespace Exemplo_PDF_e_ZIP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
