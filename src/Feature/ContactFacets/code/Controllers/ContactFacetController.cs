namespace Sitecore.Feature.ContactFacets.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.ContactFacets.Models;

  public class ContactFacetController : Controller
  {
    private readonly Contact contact;

    public ContactFacetController(Contact contact)
    {
      this.contact = contact;
    }

    public ActionResult Index()
    {
      ContactFacetModel model = new ContactFacetModel();

      if (this.contact != null)
      {

        model.FirstName = this.contact.GetFacet<IContactPersonalInfo>("Personal").FirstName;
      }
      else
      {
        this.ViewBag.NoXDB = true;
      }
      return this.View("ContactFacetForm", model);

    }
  }
}