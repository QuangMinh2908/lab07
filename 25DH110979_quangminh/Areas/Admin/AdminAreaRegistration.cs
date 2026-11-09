using System.Diagnostics;
using System.Web.Mvc;

namespace _25DH110979_quangminh.Areas.Admin
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "(),nq}")]
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "_25DH110979_quangminh.Areas.Admin.Controllers" }
            );
        }

        private string DebuggerDisplay => ToString();
    }
}
