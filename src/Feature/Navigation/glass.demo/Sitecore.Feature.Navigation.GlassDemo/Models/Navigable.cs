namespace Sitecore.Feature.Navigation.GlassDemo.Models
{
  using System;
  using Glass.Mapper.Sc.Configuration.Attributes;

  public class Navigable
  {
    public virtual Guid Id { get; set; }
    public virtual Navigable Parent { get; set; }
    public virtual string Url { get; set; }
    public virtual bool IsActive { get; set; }
    public virtual string NavigationTitle { get; set; }
  }
}