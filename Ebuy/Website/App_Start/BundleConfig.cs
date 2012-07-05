using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(BundleConfig), "RegisterBundles")]

namespace Ebuy.Website.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles()
		{
			var bundles = BundleTable.Bundles;
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate.min.js",
						"~/Scripts/jquery.validate.unobtrusive.min.js"
						));
		}
	}
}