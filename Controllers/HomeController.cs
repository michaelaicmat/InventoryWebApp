using System.Web.Mvc;
using System.Web.Security;

namespace InventoryWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Group");
        }

        public ActionResult LogOut()
        {
          
           FormsAuthentication.SignOut();
            Session.Abandon(); 
            return View("Login", "Account");
        }

        public ActionResult Contact()
        {
            return View();
   
        }
    }
}