using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace Licenta.App_Start
{
	public class BungleConfig 
	{
		public static void RegisterBundler(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/scripts/desktop")
                .Include(
                "~/bootstrap-4.1.1-dist/js/bootstrap.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css/desktop")
                .Include(
                "~/bootstrap-4.1.1-dist/css/bootstrap-grid.css",
                "~/bootstrap-4.1.1-dist/css/bootstrap-reboot.css",
                "~/bootstrap-4.1.1-dist/css/bootstrap.css"));
        }
	}
}