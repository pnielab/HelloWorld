using Autofac;
using Autofac.Integration.Mef;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace HelloWorld
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // import Autofac
            ContainerBuilder builder = new ContainerBuilder();

            string targetDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Register the Web API controllers in external assemblies (extensions)
            Assembly[] assemblies = new DirectoryInfo(targetDir)
                .GetFiles("*.dll")
                .Select(r => Assembly.LoadFrom(r.FullName)).ToArray();

            builder.RegisterApiControllers(assemblies);

            
            // Register MEF-exported and imported dependencies
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(targetDir, "*.dll"));
            catalog.Catalogs.Add(new DirectoryCatalog(targetDir, "*.exe"));


            builder.RegisterComposablePartCatalog(catalog);


            builder.RegisterControllers(typeof(ApiController).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());


            IContainer container = builder.Build();

            //assign a dependency resolver for Web API to use.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);

            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);


        }
    }
}
