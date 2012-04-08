using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using Mobile.Core.Security;


namespace Mobile.Core.Mvc.Attribute
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MobileAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string _permission;
        private string[] _permissionSplit = new string[0];       

        public string Permissions
        {
            get
            {
                return _permission ?? String.Empty;
            }
            set
            {
                _permission = value;
                _permissionSplit = SplitString(value);
            }
        }        

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!MobileAuthentication.IsAuthenticated)
            {
                HandleUnAuthenticatedRequest(filterContext);
            }
            if(!AuthorizeCore())
            {            
                HandleUnauthorizedRequest(filterContext);
            }            
        }

        
        /// <summary>
        /// Xử lý khi User chưa đăng nhập
        /// </summary>
        private void HandleUnAuthenticatedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = -1 };
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        /// <summary>
        /// Xử lý khi User không có quyền
        /// </summary>
        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = 0 };
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        protected virtual bool AuthorizeCore()
        {
            if (_permissionSplit.Length > 0 && !_permissionSplit.Any(p => MobileAuthentication.User.Permissions.Contains(p)))
            {
                return false;
            }
            return true;
        }

        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }

        

    }
}
