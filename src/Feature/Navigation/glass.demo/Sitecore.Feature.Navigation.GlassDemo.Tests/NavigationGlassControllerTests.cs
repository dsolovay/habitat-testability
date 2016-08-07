namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using System.Linq;
  using System.Web.Mvc;
  using FluentAssertions;
  using Glass.Mapper.Sc;
  using NSubstitute;
  using Sitecore.Feature.Navigation.GlassDemo.Controllers;
  using Sitecore.Feature.Navigation.GlassDemo.Models;
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
    public void Breadcrumb_Called_ReturnsBreadcrumbGlassView(NavigationGlassController sut)
    {
      ViewResult result = sut.GetBreadcrumb();

      result.ViewName.Should().Be("BreadcrumbGlass", "because we don't want to overwrite the main breadcrumb view");
    }
  }
}