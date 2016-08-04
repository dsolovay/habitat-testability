using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Navigation.AutofacModules
{
  using Autofac;
  using Autofac.Integration.Mvc;
  using Sitecore.Feature.Navigation.Controllers;
  using Sitecore.Feature.Navigation.Repositories;
  using Sitecore.Mvc.Presentation;

  public class NavigationModule:Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(c => new NavigationRepository(RenderingContext.Current.Rendering.Item))
        .As<INavigationRepository>();

      builder.RegisterControllers(typeof(NavigationController).Assembly);
    }
  }
}