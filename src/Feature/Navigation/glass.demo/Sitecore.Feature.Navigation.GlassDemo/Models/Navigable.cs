namespace Sitecore.Feature.Navigation.GlassDemo.Models
{
  public class Navigable
  {
    public Navigable Parent { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public string NavigationTitle { get; set; }
  }
}