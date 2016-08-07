namespace Sitecore.Feature.Navigation.GlassDemo.Controllers
{
  using System.Collections.Generic;
  using System.Web.Mvc;
  using Glass.Mapper.Sc;
  using Sitecore.Feature.Navigation.GlassDemo.Models;

  public class NavigationGlassController : Controller
  {
    private readonly ISitecoreContext context;

    public NavigationGlassController(ISitecoreContext context)
    {
      this.context = context;
    }

    public ViewResult GetBreadcrumb()
    {
      var breadcrumbModel = new BreadcrumbModel() {Elements = this.GetBreadcrumbElements()};
      return this.View("BreadcrumbGlass", breadcrumbModel);
    }

    private List<Navigable> GetBreadcrumbElements()
    {
      List<Navigable> items = new List<Navigable>();
      Navigable current = this.context.GetCurrentItem<Navigable>();
      Navigable home = this.context.GetHomeItem<Navigable>();
      while (current != home)
      {
        items.Add(current);
        current = current.Parent;
      }
      items.Add(home);
      items.Reverse();
      return items;
    }
  }
}