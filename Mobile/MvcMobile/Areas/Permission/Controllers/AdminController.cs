using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobile.Core.Mvc.Attribute;
using Mobile.Core.Security;
using MvcMobile.Controllers;
using Mobile.Framework;
using Mobile.Object;

namespace MvcMobile.Areas.Permission.Controllers
{
    [MobileAuthorize]
    public class AdminController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetPermissionList(int jtStartIndex = 0, int jtPageSize = 1, string jtSorting = null, string keyword = "")
        {
            PagingInput paging = new PagingInput (){
                PageIndex = jtStartIndex/jtPageSize,
                PageSize = jtPageSize,
                SortDirection = Mobile.Framework.Extensions.SortDirection.Ascending,
                SortPropertyName = "PermissionID"
            };
            var source = AppGlobal.Services.BusinessPermission.GetPermissionPaging(MobileAuthentication.User.CompanyId.Value, keyword, false,paging);            
            return Json(new { Result = "OK", Records = source.Results, TotalRecordCount = source.RowCount });
        }

        [HttpPost]
        public JsonResult SavePermission(SystemPermission objPermission)
        {
            if (objPermission.PermissionID > 0) // Update
            {
                if (!AppGlobal.Services.BusinessPermission.IsExistPermission(objPermission.PermissionID))
                {
                    ErrorMessages.Add(new Mobile.Core.Mvc.Error() { Message = "Your request is not exist" });
                    return Json(new { suceess = 0, ErrorMessages = ErrorMessages });
                }
                objPermission.ModifiedBy = MobileAuthentication.UserId;
                if (string.IsNullOrEmpty(objPermission.PermissionName))
                {
                    ErrorMessages.Add(new Mobile.Core.Mvc.Error() { Message = "Please enter the permission name" });
                    return Json(new { success = 0, errormessages = ErrorMessages });
                }
                var result = AppGlobal.Services.BusinessPermission.UpdatePermission(objPermission);
                return Json(new { success = result ? 1 : 0 , errormessage = ErrorMessages});
            }
            else // Add new
            {
                objPermission.CreatedBy = MobileAuthentication.User.UserId;
                objPermission.CreatedDate = DateTime.Now;

                if (string.IsNullOrEmpty(objPermission.PermissionName))
                {
                    ErrorMessages.Add(new Mobile.Core.Mvc.Error() { Message = "Please enter the permission name" });
                    return Json(new { success = 0, errormessages = ErrorMessages });
                }
                var result = AppGlobal.Services.BusinessPermission.AddPermission(objPermission);
                return Json(new { success = result ? 1 : 0, errormessages = ErrorMessages });
            }
        }

        [HttpPost]
        public JsonResult DeletePermission(int permissionId)
        {
            var objPermission = AppGlobal.Services.BusinessPermission.GetPermission(permissionId);
            if (objPermission == null || objPermission.IsDeleted == true)
            {
                ErrorMessages.Add(new Mobile.Core.Mvc.Error() {Message = "Your request is not exist or deleted" });
                return Json(new {success = 0 , errormessages = ErrorMessages });
            }

            var result = AppGlobal.Services.BusinessPermission.DeletePermission(permissionId , MobileAuthentication.UserId);
            if (!result)
            {
                ErrorMessages.Add(new Mobile.Core.Mvc.Error (){Message = "Đã có lỗi xảy ra trong quá trình xoá."});
            }
            return Json(new { success = result ? 1 : 0 , errormessages = ErrorMessages});
                
        }

        [HttpPost]
        public JsonResult GetPermission(int permissionId)
        {
            var objPermission = AppGlobal.Services.BusinessPermission.GetPermission(permissionId);
            return Json(objPermission);
        }
    }
}
