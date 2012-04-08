using Mobile.Business;
using Mobile.Data.Repositories;
using Mobile.Object;
using System.Linq;
using System;

namespace Mobile.Business
{
    public class BLLUser : BLLBase
    {
        private ISystemUserRepository repSystemUser;

        public BLLUser()
        {
            repSystemUser = new SystemUserRepository(DatabaseFactory);
        }

        public IQueryable<SystemUser> GetUsers()
        {
            return repSystemUser.GetAll();
        }

        public IQueryable<SystemUser> GetUsers(string keyword , bool? IsDeleted , bool? IsLocked , int? companyID)
        {
            var query = GetUsers();
            if (!string.IsNullOrEmpty(keyword)){
                keyword = keyword.Trim().ToLower();
                query.Where(p => p.Email.ToLower().Contains(keyword));
            }
            if (IsDeleted.HasValue){
                query = query.Where(p => p.IsDeleted == IsDeleted);
            }
            if (IsLocked.HasValue){
                query = query.Where(p => p.IsLocked == IsLocked);
            }
            if (companyID.HasValue){
                query = query.Where(p => p.CompanyID == p.CompanyID);
            }
            return query;
        }

        /// <summary>
        /// Lấy danh sách người dùng, 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="IsLocked"></param>
        /// <param name="pageIndex">Tính từ 0</param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public IQueryable<SystemUser> GetUsers(string keyword , bool? IsDeleted , bool? IsLocked,int? companyID , int pageIndex, int pageSize , out int totalRecord)
        {
            var query = GetUsers(keyword, IsDeleted, IsLocked, companyID);
            totalRecord = query.Count();
            return query
                .OrderBy(p=>p.UserID)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }

        public SystemUser GetUser(int userID) {
            return repSystemUser.GetAll().Where(p => p.UserID == userID).FirstOrDefault();
        }

        public bool AddUser(SystemUser objUser) {
            try
            {
                repSystemUser.Add(objUser);
                SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(int userID, int deletedBy)
        {
            try
            {
                var objUser = GetUser(userID);
                objUser.IsDeleted = true;
                objUser.DeletedDate = DateTime.Now;
                objUser.DeletedBy = deletedBy;
                repSystemUser.Update(objUser);
                SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(SystemUser objUser) {
            try
            {
                repSystemUser.Update(objUser);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="email">Emai address</param>
        /// <param name="password">Password encrypted</param>
        /// <returns></returns>
        public SystemUser GetUser(string email, string password)
        {   
            return repSystemUser.GetAll().Where(p => p.Email == email && p.Password == password).FirstOrDefault();
        }
        


    }
}
