using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Object;
using Mobile.Framework;

namespace Mobile.Business
{
    public interface IBLLPermission
    {
        SystemPermission GetPermission(int permissionId);
        IQueryable<SystemPermission> GetPermissions(int companyId, string keyword, bool? isDeleted);
        PagedResult<SystemPermission> GetPermissionPaging(int companyId, string keyword, bool? isDeleted, PagingInput pagingInput);
        bool AddPermission(SystemPermission objPermission);
        bool DeletePermission(int permissionId, int userId);
        bool UpdatePermission(SystemPermission objPermission);
        List<string> GetPermissionsByUserId(int userId);
        IQueryable<SystemPermission> GetPermissionByRoleId(int roleId);
        bool IsExistPermission(int permissionId);
    }
}
