using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace ILS.Web.Extensions
{
	public class JsonController : Controller
	{
		public ActionResult Json(IEnumerable<object> data)
		{
			return Json(new 
			{
				data = data,
				total = data.Count(),
				success = true
			}, JsonRequestBehavior.AllowGet);
		}
	}
}