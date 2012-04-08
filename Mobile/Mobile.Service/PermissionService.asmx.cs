using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Mobile.Business;
using Mobile.Object;

namespace Mobile.Service
{
    /// <summary>
    /// Summary description for PermissionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PermissionService : ServicesBase<BLLPermission>
    {
        
        [WebMethod]
        public Object.SystemPermission GetPermission(int permissionId)
        {
            using (var bll = GetBLL())
            {
                return bll.GetPermission(permissionId);   
            }            
        }

        [WebMethod]
        public List<SystemPermission> GetPermissions(int companyId, string keyword, bool? isDeleted)
        {
            using (var bll = GetBLL())
            {
                return bll.GetPermissions(companyId, keyword, isDeleted).ToList();
            }
        }

        [WebMethod]
        public Framework.PagedResult<Object.SystemPermission> GetPermissionPaging(int companyId, string keyword, bool? isDeleted, Framework.PagingInput pagingInput)
        {
            using (var bll = GetBLL())
            {
                return bll.GetPermissionPaging(companyId, keyword, isDeleted, pagingInput);
            }
        }

        [WebMethod]
        public bool AddPermission(Object.SystemPermission objPermission)
        {
            using (var bll = GetBLL())
            {
                return bll.AddPermission(objPermission);
            }
        }

        [WebMethod]
        public bool DeletePermission(int permissionId, int userId)
        {
            using (var bll = GetBLL())
            {
                return bll.DeletePermission(permissionId, userId);
            }
        }

        [WebMethod]
        public bool UpdatePermission(Object.SystemPermission objPermission)
        {
            using (var bll = GetBLL())
            {
                return bll.UpdatePermission(objPermission);
            }
        }

        [WebMethod]
        public List<string> GetPermissionsByUserId(int userId)
        {
            using (var bll = GetBLL())
            {
                return bll.GetPermissionsByUserId(userId);
            }
        }

        [WebMethod]
        public List<Object.SystemPermission> GetPermissionByRoleId(int roleId)
        {
            using (var bll = GetBLL())
            {
                return bll.GetPermissionByRoleId(roleId).ToList();
            }
        }

        [WebMethod]
        public bool IsExistPermission(int permissionId)
        {
            using (var bll = GetBLL())
            {
                return bll.IsExistPermission(permissionId);
            }
        }
    }
}
