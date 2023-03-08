using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bewitching.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
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
    }
}