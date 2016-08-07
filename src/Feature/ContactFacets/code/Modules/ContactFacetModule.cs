namespace Sitecore.Feature.ContactFacets.Modules
{
  using Autofac;
  using Autofac.Integration.Mvc;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.ContactFacets.Controllers;


  public class ContactFacetModule:Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(context =>
      {
        if (Sitecore.Analytics.Tracker.Enabled)
        {
          return Sitecore.Analytics.Tracker.Current?.Contact;
        }
        return null;
      }).As<Contact>();

      builder.RegisterControllers(typeof(ContactFacetController).Assembly);
    }
  }
}