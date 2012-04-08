using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMobile.Controllers;
using Mobile.Framework;
using Mobile.Core.Security;
using Mobile.Object;
using Mobile.Core.Mvc.Attribute;

namespace MvcMobile.Areas.Role.Controllers
{
    [MobileAuthorize]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult GetRoleList(int jtStartIndex = 0, int jtPageSize = 1, string jtSorting = null , string keyword = "")
        {
            PagingInput paging = new PagingInput()
            {
                PageIndex = jtStartIndex / jtPageSize,
                PageSize = jtPageSize,
                SortDirection = Mobile.Framework.Extensions.SortDirection.Ascending,
                SortPropertyName = "RoleID"
            };
            var source = AppGlobal.Services.BusinessRole.GetRolesPaging(MobileAuthentication.User.CompanyId.Value, keyword, false, paging);
            return Json(new { Result = "OK", Records = source.Results, TotalRecordCount = source.RowCount });
        }

        [HttpPost]
        public JsonResult SaveRole(SystemRole objRole)
        {
            if (objRole.RoleID > 0)
            {
                var result = AppGlobal.Services.BusinessRole.UpdateRole(objRole);
                return Json(new { success = result ? 1 : 0, errormessages = ErrorMessages });
            }
            else
            {

                ErrorMessages = Mobile.Core.Mvc.Validation.ModelValidation.Validate(objRole, this.ControllerContext).ToList();
                if (ErrorMessages.Count > 0)
                {
                    return Json(new { success = 0, errormessages = ErrorMessages });
                }

                var result = AppGlobal.Services.BusinessRole.AddRole(objRole);
                return Json(new { success = result ? 1 : 0, errormessages = ErrorMessages });
            }
        }

        [HttpPost]
        public JsonResult DeleteRole(int roleId)
        {
            var objRole = AppGlobal.Services.BusinessRole.GetRole(roleId);
            if (objRole == null || objRole.IsDeleted == true)
            {
                ErrorMessages.Add(new Mobile.Core.Mvc.Error() { Message = "Mục bạn yêu cầu không tồn tại hoặc đã bị xoá" });
                return Json(new { success = 0, errormessages = ErrorMessages });
            }

            var result = AppGlobal.Services.BusinessRole.DeleteRole(roleId, MobileAuthentication.UserId);
            if (!result)
            {
                ErrorMessages.Add(new Mobile.Core.Mvc.Error() { Message = "Đã có lỗi xảy ra trong quá trình xoá." });
            }
            return Json(new { success = result ? 1 : 0, errormessages = ErrorMessages });
        }

        //[HttpPost]
        //public JsonResult GetPermission(int permissionId)
        //{
        //    var objPermission = AppGlobal.Services.BusinessPermission.GetPermission(permissionId);
        //    return Json(objPermission);
        //}
    }
}
