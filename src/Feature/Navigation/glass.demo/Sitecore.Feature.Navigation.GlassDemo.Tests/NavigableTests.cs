namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using System;
  using System.Linq;
  using System.Reflection;
  using FluentAssertions;
  using Sitecore.Feature.Navigation.GlassDemo.Models;
  using Xunit;

  public class NavigableTests { 

    [Theory, GlassNavData]
    public void Navigable_RequiredFields_Present(Navigable navItem)
    {
      navItem.Url.GetType().Should().Be<string>("because this stores the link");
      navItem.IsActive.GetType().Should().Be<bool>("because this toggles a CSS class");
      navItem.NavigationTitle.GetType().Should().Be<string>("because this is rendered text");
      navItem.Id.GetType().Should().Be<Guid>();

      Type type = typeof(Navigable);
      PropertyInfo property = type.GetProperty("Parent");
      property.PropertyType.Should().Be<Navigable>("because we need to crawl up the tree");

      type.GetProperties().ToList().ForEach(p=>p.Should().BeVirtual("because they need to be populated on the parent, which is a proxy"));
 
    }
  }
}