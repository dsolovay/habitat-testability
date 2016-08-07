namespace Sitecore.Feature.Navigation.GlassDemo.Modules
{
  using Autofac;
  using Autofac.Integration.Mvc;
  using Glass.Mapper.Sc;
  using Sitecore.Feature.Navigation.GlassDemo.Controllers;

  public class AutofacModule:Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SitecoreContext>().As<ISitecoreContext>();

      builder.RegisterControllers(typeof(NavigationGlassController).Assembly);
    }
  }
}