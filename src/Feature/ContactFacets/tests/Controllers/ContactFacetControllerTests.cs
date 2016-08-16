namespace Sitecore.Feature.ContactFacets.Tests.Controllers
{
  using System.Web.Mvc;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Feature.ContactFacets.Controllers;
  using Sitecore.Feature.ContactFacets.Models;
  using Xunit;
  using Analytics.Tracking;
  using FluentAssertions;

  public class ContactFacetControllerTests
  {
    private ContactFacetController _sut;
    private Contact _substituteContact;

    public ContactFacetControllerTests()
    {

      _substituteContact = Substitute.For<Contact>();
      _sut = new ContactFacetController(_substituteContact);
    }

    [Fact]
    public void New_Called_ReturnsController()
    {
      _sut.Should().NotBeNull();
    }

    [Fact]
    public void Index_CalledWithNoParms_ReturnsView()
    {

      ActionResult result = _sut.Index();

      result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Index_NoParms_ReturnsCorrectView()
    {
      ViewResult result = _sut.Index() as ViewResult;

      Assert.Equal("ContactFacetForm", result.ViewName);
    }

    [Fact]
    public void Index_ContactNotNull_PopulatesModel()
    {
      string firstName = "first name";
      this._substituteContact.GetFacet<IContactPersonalInfo>("Personal").FirstName.Returns(firstName);

      ViewResult result = _sut.Index() as ViewResult;

      ContactFacetModel model = result.Model as ContactFacetModel;
      Assert.Equal(firstName, model.FirstName);

    }

    [Fact]
    public void Index_ContactNull_SetsNoXdb()
    {
      this._sut = new ContactFacetController(contact: null);

      ViewResult result = _sut.Index() as ViewResult;

      Assert.True(result.ViewBag.NoXDB);
    }

    //[Fact]
    //public void Index_PassedModel_UpdatesFacet()
    //{
    //  string expected = "first name";
    //  ContactFacetModel model = new ContactFacetModel() { FirstName = expected };

    //  _sut.Index(model);

    //  this._substituteContact.GetFacet<IContactPersonalInfo>("Personal")
    //    .Received().FirstName = expected;
    //}

    //[Fact]
    //public void Index_PassedModel_ReturnsSuccess()
    //{

    //  ViewResult result = _sut.Index(new ContactFacetModel());

    //  Assert.True(result.ViewBag.Success);
    //  Assert.Equal("ContactFacetForm", result.ViewName);
    //}

    //[Fact]
    //public void Index_PassedModel_ReturnsIt()
    //{
    //  ContactFacetModel model = new ContactFacetModel();

    //  ViewResult result = _sut.Index(model) as ViewResult;
   
    //  Assert.Equal(model, result.Model);
    //}


}
}