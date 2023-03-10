using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ModalServices.AdminModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bewitching.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AccountController()
        {

        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Login()
        {
            await Task.Delay(0);

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(AdminLoginViewModel model)
        {
            try
            {
                // modelstate is used to validate the model if there is any reuired or some other annotation exist and aslo used to trigger the message if it is 
                // mention in the model
                if (ModelState.IsValid)
                {
                    string userName = await GetLoggedInUserName(model.Email);
                    if (string.IsNullOrEmpty(userName))
                    {
                        TempData["ErrorMessage"] = "Invalid Email/Password";
                        return RedirectToAction("Login", "Account", new { area = "Admin" });

                    }
                    else
                    {
                        var result = await SignInManager.PasswordSignInAsync(userName, model.Password, false, shouldLockout: false);

                        if (SignInStatus.Success == result)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        if (SignInStatus.Failure == result || SignInStatus.LockedOut == result)
                        {
                            TempData["ErrorMessage"] = "Invalid Email/Password";
                            return RedirectToAction("Login", "Account", new { area = "Admin" });
                        }

                        if (SignInStatus.RequiresVerification == result)
                        {
                            TempData["ErrorMessage"] = "Required Verification from Admin";
                            return RedirectToAction("Login", "Account", new { area = "Admin" });
                        }
                    }
                }

                return View(model);

            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

        }

        private async Task<string> GetLoggedInUserName(string logged_email)
        {
            var userDetails = await UserManager.FindByEmailAsync(logged_email);

            if (userDetails != null)
            {
                return userDetails.UserName;
            }
            return "";
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            TempData["SuccessMessage"] = "You have Successfully log out.";
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<ActionResult> ChangePassword()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            await Task.Delay(0);
            if (ModelState.IsValid)
            {
                

            }

            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}