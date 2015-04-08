using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ILS.Web.Extensions
{
	public class PerSessionLifetimeManager : LifetimeManager
	{
		private string _key = Guid.NewGuid().ToString();

		public override object GetValue()
		{
			return HttpContext.Current.Session[_key];
		}

		public override void SetValue(object value)
		{
			HttpContext.Current.Session[_key] = value;
		}

		public override void RemoveValue()
		{
			HttpContext.Current.Session.Remove(_key);
		}
	}
}