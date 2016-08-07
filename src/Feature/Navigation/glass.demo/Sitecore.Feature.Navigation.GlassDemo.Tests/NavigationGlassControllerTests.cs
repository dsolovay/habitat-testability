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
      Navigable first = breadcrumbModel.Elements.First();
      first.Should().Be(homeItem);
      first.IsActive.Should().BeTrue();
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
      var navigables = breadcrumbModel.Elements.ToList();
      navigables[0].Should().Be(homeItem);
      navigables[0].IsActive.Should().BeFalse("because it is not last in list");
      navigables[1].Should().Be(childItem);
      navigables[1].IsActive.Should().BeTrue("because it is last in list");
    }

 

    [Theory, GlassNavData]
    public void GetBreadcrumb_TwoHomeObjectsSameId_DoesntThrow(NavigationGlassController sut, ISitecoreContext context, Navigable homeItem1, Navigable homeItem2, Navigable childItem)
    {
      homeItem2.Id = homeItem1.Id;
      context.GetHomeItem<Navigable>().Returns(homeItem1);
      context.GetCurrentItem<Navigable>().Returns(childItem);
      childItem.Parent = homeItem2;

      sut.GetBreadcrumb();

    }

    [Theory, GlassNavData]
    public void Breadcrumb_Called_ReturnsBreadcrumbGlassView(NavigationGlassController sut)
    {
      ViewResult result = sut.GetBreadcrumb();

      result.ViewName.Should().Be("BreadcrumbGlass", "we don't want to overwrite the main breadcrumb view");
    }
  }
}