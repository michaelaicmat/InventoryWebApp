using InventoryWebApp.ViewModels;
using System.Web.Mvc;
using InventoryWebApp.Infrastructure.DAL.Services;


//Controller for Group page
namespace InventoryWebApp.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        
        public ActionResult Index(bool showNoRowAlert = false)
        {
            return View(new CountGroupViewModel()
            {
                GroupRecords = GroupDataService.GetAllGroups(),
                ShowNoRowAlert = showNoRowAlert
            });
        }
    }
}