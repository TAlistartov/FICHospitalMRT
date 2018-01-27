using FICHospitalMRT.Models;
using FICHospitalMRT.Models.Authorization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace FICHospitalMRT.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginmodel)
        {
            if(ModelState.IsValid)
            {
                //here we search user in Db
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == loginmodel.Login &&
                                  u.Password == loginmodel.Password);
                }
                if(user!=null)
                {
                    FormsAuthentication.SetAuthCookie(loginmodel.Login, true);
                    return (RedirectToAction("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем не существует!");
                }

            }
            return View(loginmodel);
        }
    }
}