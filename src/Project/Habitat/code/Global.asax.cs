namespace Sitecore.Habitat.Website
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;
  using System.Web.Mvc;
  using Autofac;
  using Autofac.Integration.Mvc;
  using Sitecore.Feature.Navigation.AutofacModules;
  using Sitecore.Mvc.Controllers;
  public partial class Global:Sitecore.Web.Application
  {
    protected void Application_Start()
    { 
      ContainerBuilder builder = new ContainerBuilder();

      builder.RegisterModule(new NavigationModule());
      
      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }

    
  }
}