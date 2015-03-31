using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(busca_de_alunos.Startup))]
namespace busca_de_alunos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
       
        }
    }
}
