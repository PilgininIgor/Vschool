using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILS.Web.Controllers
{
    //[Authorize]
    public class IdeologyController : Controller
    {
        //
        // GET: /Ideology/

        public ActionResult Index()
        {
            return View();
        }

    }
}
