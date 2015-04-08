using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ILS.Domain;
using Microsoft.Practices.Unity;

namespace ILS.Web.Extensions
{
	public class EFSessionModule : IHttpModule
	{
		private ILSContext context;

		public void Init(HttpApplication context)
		{

			context.BeginRequest += (s, e) =>
				{
					this.context = MvcApplication.Container.Resolve<ILSContext>();
				};

			context.EndRequest += (s, e) =>
				{
					this.context.SaveChanges();
				};

		}

		public void Dispose()
		{
		}
	}
}