using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(DisplayModesRegistration), "RegisterDisplayModes")]
namespace Ebuy.Website.App_Start
{
    public class DisplayModesRegistration
    {
         public static void RegisterDisplayModes()
         {
             // register iPhone specific Views
             
             DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("iPhone")
             {
                 ContextCondition = (ctx => ctx.Request.UserAgent.IndexOf(
                    "iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
             });

             // register Windows Phone specific Views
             DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("WindowsPhone")
             {
                 ContextCondition = (ctx => ctx.Request.UserAgent.IndexOf(
                    "Windows Phone", StringComparison.OrdinalIgnoreCase) >= 0)
             });
             
         }
    }
}