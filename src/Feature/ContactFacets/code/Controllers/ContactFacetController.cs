namespace Sitecore.Feature.ContactFacets.Controllers
{
  using System.Runtime.InteropServices;
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

    [HttpGet]
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

    [HttpPost]
    public ActionResult Index(ContactFacetModel model)
    {
      this.contact.GetFacet<IContactPersonalInfo>("Personal").FirstName = model.FirstName;
      ViewBag.Success = true;
      return this.View("ContactFacetForm", model);
    }
  }
}