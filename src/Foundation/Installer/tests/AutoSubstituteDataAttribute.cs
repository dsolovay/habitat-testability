namespace Sitecore.Foundation.Installer.Tests
{
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;

  public class AutoSubstituteDataAttribute : AutoDataAttribute
  {
    public AutoSubstituteDataAttribute()
    {
      this.Fixture.Customize(new AutoNSubstituteCustomization());
    }
  }
}