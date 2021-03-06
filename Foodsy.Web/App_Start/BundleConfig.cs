﻿using System;
using System.Web;
using System.Web.Optimization;

namespace Foodsy.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);    

            BundleTable.EnableOptimizations = false;
        }
 
        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include("~/Content/kendo/kendo.common.min.css",
                      "~/Content/kendo/kendo.common-bootstrap.min.css",
                      "~/Content/kendo/kendo.default.min.css"));

            bundles.Add(new StyleBundle("~/Content/prettyPhoto").Include(
                      "~/Content/prettyPhoto.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                      "~/Content/site.css"));
        }
 
        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                //.Include("~/Scripts/jquery-{version}.js",
                //        "~/Scripts/jquery.unobtrusive-ajax.js"));
                .Include("~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax")
               .Include(
                       "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo")
                .Include("~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/prettyPhoto").Include(
                        "~/Scripts/jquery.prettyPhoto.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/myscript").Include(
                      "~/Scripts/myscript.js",
                      "~/Scripts/sorting.js"));
        }
    }
}
