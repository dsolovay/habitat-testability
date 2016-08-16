namespace Sitecore.Habitat.Website
{
  using System.Web.Mvc;
  using Autofac;
  using Autofac.Configuration;
  using Autofac.Configuration.Core;
  using Autofac.Integration.Mvc;
  using Microsoft.Extensions.Configuration;

  public partial class Global:Sitecore.Web.Application
  {
    protected void Application_Start()
    {
      var builder = new ContainerBuilder();
      builder.RegisterModule(new ConfigurationSettingsReader("autofac", @"App_Config\autofac.config"));
      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}