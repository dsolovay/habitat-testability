namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using System.Web.Mvc;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using FluentAssertions.Primitives;
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

      var breadcrumbModel = result.Model as BreadcrumbModel;
      breadcrumbModel.Elements.Count().Should().Be(1);
      breadcrumbModel.Elements.First().Should().Be(homeItem);
    }

    [Theory, GlassNavData]
    public void GetBreadcrumb_ChildOfHome_ReturnsHomeThenChild(NavigationGlassController sut, ISitecoreContext context, NavigationItem homeItem, NavigationItem childItem)
    {
      context.GetHomeItem<NavigationItem>().Returns(homeItem);
      context.GetCurrentItem<NavigationItem>().Returns(childItem);
      childItem.Parent = homeItem;

      ViewResult result = sut.GetBreadcrumb();

      var breadcrumbModel = result.Model as BreadcrumbModel;
      breadcrumbModel.Elements.Count().Should().Be(2);
      breadcrumbModel.Elements.ToList()[0].Should().Be(homeItem);
      breadcrumbModel.Elements.ToList()[1].Should().Be(childItem);
    }

    [Theory, GlassNavData]
    public void NavigationItem_RequiredFields_Present(NavigationItem navItem)
    {
      navItem.DividerBefore.GetType().Should().Be<bool>("because this is a checkbox field");
      navItem.Icon.GetType().Should().Be<string>("because this is used to store an icon css class");
      navItem.Icon.Should().NotBeEmpty("because AutoFixure should popluate properties");
    }
  }

  public class NavigationItem
  {
    public NavigationItem Parent { get; set; }
    public bool DividerBefore { get; set; }
    public string Icon { get; set; }
  }

  public class GlassNavDataAttribute : AutoDataAttribute
  {
    public GlassNavDataAttribute() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Freeze<ISitecoreContext>();
      this.Fixture.Register(() => this.Fixture.Build<NavigationItem>().Without(item => item.Parent).Create());
      this.Fixture.Register(() => this.Fixture.Build<NavigationGlassController>().OmitAutoProperties().Create());
    }
  }

  public class BreadcrumbModel
  {
    public IEnumerable<NavigationItem> Elements { get; set; }
  }

  public class NavigationGlassController : Controller
  {
    private readonly ISitecoreContext context;

    public NavigationGlassController(ISitecoreContext context)
    {
      this.context = context;
    }

    public ViewResult GetBreadcrumb()
    {
      var breadcrumbModel = new BreadcrumbModel() {Elements = GetBreadcrumbElements()};
      return View(breadcrumbModel);
    }

    private List<NavigationItem> GetBreadcrumbElements()
    {
      List<NavigationItem> items = new List<NavigationItem>();
      NavigationItem current = this.context.GetCurrentItem<NavigationItem>();
      NavigationItem home = this.context.GetHomeItem<NavigationItem>();
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
