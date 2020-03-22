using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using InventoryWebApp.Models;
using System.Data.Entity;



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


        //Controller for Contact Page
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactUs(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {

                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                await EmailService.SendAsync("New IT Inquiry", "From: \n" + model.Email + "\n \n Message:\n " + model.Message);
                ViewBag.message = "Message successfully delivered!";
                return View("Contact");
            }

            // If we got this far, something failed, redisplay form
            ViewBag.message = "Message not sent. Please try again.";
            return View(model);
        }
    }
}