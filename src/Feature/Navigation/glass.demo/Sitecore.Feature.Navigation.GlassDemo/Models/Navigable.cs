namespace Sitecore.Feature.Navigation.GlassDemo.Models
{
  using System;
  using Glass.Mapper.Sc.Configuration.Attributes;

  public class Navigable
  {
    [SitecoreParent]
    public virtual Navigable Parent { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public string NavigationTitle { get; set; }
    public Guid Id { get; set; }
  }
}