using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private AccountsService Service { get; set; }

        public AccountsController()
        {
            this.Service = new AccountsService();

        }

        [HttpGet, AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Index(Account account) {
            try
            {
                Account user = this.Service.SignIn(account);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    return RedirectToAction("Index", "Weather", null);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user name or password is incorrect");
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult SignUp() {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            try
            {
                bool status = this.Service.SignUp(account);
                if (status == true)
                {
                    return RedirectToAction("EmailConfirmation", "Accounts", null);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Username or email not valid! Are you sure you dont have account?");
                    return View();
                }
            }
            catch (Exception ex) {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Accounts", null);
        }

        [HttpGet]
        public ActionResult SignInAsGuest() {
            FormsAuthentication.SetAuthCookie("Guest", true);
            return RedirectToAction("Index", "Weather", null);
        }

        [HttpGet]
        public ActionResult EmailConfirmation() {
            return View();
        }

        [HttpGet]
        [Route("{activationToken}")]
        public ActionResult Activation(string activationToken) {
            try
            {
                bool status = Service.Activation(activationToken);
                if (status == true)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    return RedirectToAction("UserDontExist", "Accounts");
                }
            }
            catch (Exception ex) {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Shared", null);
            }
        }

        [HttpGet]
        public ActionResult Success() {
            return View();
        }

        [HttpGet]
        public ActionResult Error() {
            return View();
        }

        [HttpGet]
        public ActionResult UserDontExist() {
            return View();
        }
    }
}