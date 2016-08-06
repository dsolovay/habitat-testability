namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using System.Web.Mvc;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using Glass.Mapper.Sc;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
 

  using Xunit;

  public class NavigationGlassControllerTests
  {
    [Theory, GlassNavData]
    public void GetBreadcrumb_HomeItem_ReturnsOneItem(NavigationGlassController sut, ISitecoreContext context, NavigationItem homeItem)
    {
      context.GetHomeItem<NavigationItem>().Returns(homeItem);
      context.GetCurrentItem<NavigationItem>().Returns(homeItem);

      ViewResult result = sut.GetBreadcrumb();

      result.Should().NotBe(null);
      (result.Model as BreadcrumbModel).Elements.Count().Should().Be(1);

    }

    
  }

  public class NavigationItem
  {
    
  }

  public class GlassNavDataAttribute : AutoDataAttribute
  {
    public GlassNavDataAttribute(): base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Register(() => this.Fixture.Build<NavigationGlassController>().OmitAutoProperties().Create());
    }
  }

  public class BreadcrumbModel
  {
    public IEnumerable<NavigationItem> Elements { get; set; }
  }

  public class NavigationGlassController: Controller
  {
    public ViewResult GetBreadcrumb()
    {
      var breadcrumbModel = new BreadcrumbModel() {Elements = GetBreadcrumbElements() };
      return View(breadcrumbModel);
    }

    private static List<NavigationItem> GetBreadcrumbElements()
    {
      return new List<NavigationItem> {new NavigationItem()};
    }
  }
}
