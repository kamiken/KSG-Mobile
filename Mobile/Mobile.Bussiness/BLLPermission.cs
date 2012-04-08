using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Object;
using Mobile.Data.Repositories;
using Mobile.Framework;

namespace Mobile.Business
{
    public class BLLPermission : BLLBase , IBLLPermission
    {
        private readonly ISystemPermissionRepository repPermission;
        private readonly ISystemUserPermissionRepository repUserPermission;
        public BLLPermission()
        {
            repPermission = new SystemPermissionRepository(DatabaseFactory);
            repUserPermission = new SystemUserPermissionRepository(DatabaseFactory);
        }

        public SystemPermission GetPermission(int permissionId)
        {
            return repPermission.GetById(permissionId);
        }

        public IQueryable<SystemPermission> GetPermissions(int companyId, string keyword, bool? isDeleted)
        {
            var query = repPermission.GetAll();
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(p => p.PermissionName.ToLower().Contains(keyword));
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted);
            }
            return query;
        }

        public PagedResult<SystemPermission> GetPermissionPaging(int companyId, string keyword, bool? isDeleted, PagingInput pagingInput)
        {
            var query = GetPermissions(companyId, keyword, isDeleted);
            var result = new PagedResult<SystemPermission>(query);
            result.SetPaging(pagingInput);
            return result;                
        }

        public bool AddPermission(SystemPermission objPermission)
        {
            try
            {
                repPermission.Add(objPermission);
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool DeletePermission(int permissionId , int userId)
        {
            try
            {
                var objPermission = GetPermission(permissionId);
                objPermission.IsDeleted = true;
                objPermission.DeletedBy = userId;
                objPermission.DeletedDate = DateTime.Now;
                repPermission.Update(objPermission);
                SaveChanges();
                return true;
            }
            catch {
                return false;
            }

        }

        public bool UpdatePermission(SystemPermission objPermission)
        {
            try
            {
                var existObj = GetPermission(objPermission.PermissionID);
                existObj.ModifiedDate = DateTime.Now;
                existObj.PermissionName = objPermission.PermissionName;
                existObj.ModifiedBy = objPermission.ModifiedBy;                

                repPermission.Update(existObj);
                SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<string> GetPermissionsByUserId(int userId)
        {
            //var permissions = from p in repUserPermission.GetMany(p=>p.UserID == userId).Select(p=>p.
            //repUserPermission.GetById(1).

            throw new NotImplementedException();

            
        }

        public IQueryable<SystemPermission> GetPermissionByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public bool IsExistPermission(int permissionId)
        {
            return (repPermission.GetMany(p=>p.PermissionID == permissionId).Count() > 0);
        }
    }
}
