using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Object;
using Mobile.Framework;

namespace Mobile.Business
{
    public interface IBLLRole
    {
        SystemRole GetRole(int roleId);
        IQueryable<SystemRole> GetRoles(int companyId, string keyword, bool? isDeleted);
        PagedResult<SystemRole> GetRolesPaging(int companyId, string keyword, bool? isDeleted, PagingInput pagingInput);
        bool AddRole(SystemRole objRole);
        bool DeleteRole(int roleId, int userId);
        bool UpdateRole(SystemRole objRole);
        IQueryable<int> GetRoleByUserId(int userId);
    }
}
