namespace Sitecore.Feature.Navigation.GlassDemo.Tests
{
  using FluentAssertions.Primitives;
  using Glass.Mapper.Sc;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Navigation.GlassDemo.Controllers;
  using Sitecore.Feature.Navigation.GlassDemo.Models;

  public class GlassNavDataAttribute : AutoDataAttribute
  {
    public GlassNavDataAttribute() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Freeze<ISitecoreContext>();
      this.Fixture.Register(() => this.Fixture.Build<Navigable>()
        .Without(item => item.Parent)
        .Without(navigable => navigable.IsActive)
        .Create());
      this.Fixture.Register(() => this.Fixture.Build<NavigationGlassController>().OmitAutoProperties().Create());
 
    }
  }
}
