namespace Sitecore.Feature.Navigation.Modules
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