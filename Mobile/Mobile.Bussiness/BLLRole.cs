using System;
using Mobile.Business;
using Mobile.Data.Repositories;
using Mobile.Object;
using System.Linq;
using Mobile.Framework;

namespace Mobile.Business
{
    public class BLLRole : BLLBase ,  IBLLRole
    {
        private ISystemRoleRepository repSystemRole;
        private ISystemRoleUserRepository repRoleUser;

        public BLLRole()
        {
            repSystemRole = new SystemRoleRepository(DatabaseFactory);
            repRoleUser = new SystemRoleUserRepository(DatabaseFactory);
        }

        public SystemRole GetRole(int roleId)
        {
            return repSystemRole.GetAll().Where(p => p.RoleID == roleId).FirstOrDefault();
        }

        public IQueryable<SystemRole> GetRoles(int companyId, string keyword, bool? isDeleted)
        {
            var query = repSystemRole.GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(p => p.RoleName.ToLower().Contains(keyword));
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted);
            }
            return query;

        }

        public Framework.PagedResult<SystemRole> GetRolesPaging(int companyId, string keyword, bool? isDeleted, Framework.PagingInput pagingInput)
        {
            var query = GetRoles(companyId, keyword, isDeleted);
            var result = new PagedResult<SystemRole>(query);
            result.SetPaging(pagingInput);
            return result;
        }

        public bool DeleteRole(int roleId, int userId)
        {
            try
            {
                var objRole = GetRole(roleId);
                objRole.IsDeleted = true;
                objRole.DeletedBy = userId;
                objRole.DeletedDate = DateTime.Now;
                repSystemRole.Update(objRole);
                SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool UpdateRole(SystemRole objRole)
        {
            try
            {
                var existObj = GetRole(objRole.RoleID);
                existObj.Description = objRole.Description;
                existObj.IsActived = objRole.IsActived;
                existObj.ModifiedBy = objRole.ModifiedBy;
                existObj.ModifiedDate = DateTime.Now;
                existObj.RoleName = objRole.RoleName;                

                repSystemRole.Update(existObj);
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<int> GetRoleByUserId(int userId)
        {
            var query = (from q in repRoleUser.GetAll()
                         where q.UserID == userId
                         select q.RoleID.Value).Distinct();
            return query;
        }


        public bool AddRole(SystemRole objRole)
        {
            try
            {
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
