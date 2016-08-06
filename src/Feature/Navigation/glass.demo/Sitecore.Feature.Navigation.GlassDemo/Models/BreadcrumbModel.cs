namespace Sitecore.Feature.Navigation.GlassDemo.Models
{
  using System.Collections.Generic;

  public class BreadcrumbModel
  {
    public IEnumerable<Navigable> Elements { get; set; }
  }
}