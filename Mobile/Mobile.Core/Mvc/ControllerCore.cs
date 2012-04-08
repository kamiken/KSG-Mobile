using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mobile.Core.Mvc
{
    public class ControllerCore : Controller
    {
        public string Layout { get; set; }
        public string Title { get; set; }
        public List<Error> ErrorMessages { get; set; }
        public ControllerCore()
        {
            ErrorMessages = new List<Error>();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            base.OnResultExecuted(filterContext);
        }
        protected override IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            base.Execute(requestContext);
        }

        protected override void ExecuteCore()
        {
            base.ExecuteCore();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Title = ConfigManager.GetSystemConfig(ConstantManager.DEFAULT_SITENAME);
            Title = string.IsNullOrEmpty(Title) ? ConfigManager.GetAppConfig(ConstantManager.DEFAULT_SITENAME) : Title;

            var layoutFolder = ConfigManager.GetSystemConfig(ConstantManager.LAYOUT_FOLDER);
            var layoutName = ConfigManager.GetSystemConfig(ConstantManager.DEFAULT_ADMIN_LAYOUT);
            Layout = layoutFolder + layoutName + "/_Layout.cshtml";

            base.Initialize(requestContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Title = Title;
            ViewBag.Layout = Layout;
            base.OnActionExecuted(filterContext);
        }
    }
}
