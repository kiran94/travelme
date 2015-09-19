using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.kiransprojects.travelme.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
