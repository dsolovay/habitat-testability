namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using System;
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

      PropertyInfo property = typeof(Navigable).GetProperty("Parent");
      property.Should().BeVirtual("because Glass requires this so it can use lazy loading and avoid infinite recursion");
      property.PropertyType.Should().Be<Navigable>("because we need to crawl up the tree");
    }
  }
}