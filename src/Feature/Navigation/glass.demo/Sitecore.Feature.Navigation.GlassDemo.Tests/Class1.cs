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
    public void GetBreadcrumb_HomeItem_ReturnsOneItem(NavigationGlassController sut, ISitecoreContext context, Navigable homeItem)
    {
      context.GetHomeItem<Navigable>().Returns(homeItem);
      context.GetCurrentItem<Navigable>().Returns(homeItem);

      ViewResult result = sut.GetBreadcrumb();

      var breadcrumbModel = result.Model as BreadcrumbModel;
      breadcrumbModel.Elements.Count().Should().Be(1);
      breadcrumbModel.Elements.First().Should().Be(homeItem);
    }

    [Theory, GlassNavData]
    public void GetBreadcrumb_ChildOfHome_ReturnsHomeThenChild(NavigationGlassController sut, ISitecoreContext context, Navigable homeItem, Navigable childItem)
    {
      context.GetHomeItem<Navigable>().Returns(homeItem);
      context.GetCurrentItem<Navigable>().Returns(childItem);
      childItem.Parent = homeItem;

      ViewResult result = sut.GetBreadcrumb();

      var breadcrumbModel = result.Model as BreadcrumbModel;
      breadcrumbModel.Elements.Count().Should().Be(2);
      breadcrumbModel.Elements.ToList()[0].Should().Be(homeItem);
      breadcrumbModel.Elements.ToList()[1].Should().Be(childItem);
    }

    [Theory, GlassNavData]
    public void NavigationItem_RequiredFields_Present(Navigable navItem)
    {
      navItem.Url.GetType().Should().Be<string>("because this stores the link");
      navItem.IsActive.GetType().Should().Be<bool>("because this toggles a CSS class");
      navItem.NavigationTitle.GetType().Should().Be<string>("because this is rendered text");
    }
  }

  public class Navigable
  {
    public Navigable Parent { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public string NavigationTitle { get; set; }
  }

  public class GlassNavDataAttribute : AutoDataAttribute
  {
    public GlassNavDataAttribute() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Freeze<ISitecoreContext>();
      this.Fixture.Register(() => this.Fixture.Build<Navigable>().Without(item => item.Parent).Create());
      this.Fixture.Register(() => this.Fixture.Build<NavigationGlassController>().OmitAutoProperties().Create());
    }
  }

  public class BreadcrumbModel
  {
    public IEnumerable<Navigable> Elements { get; set; }
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
