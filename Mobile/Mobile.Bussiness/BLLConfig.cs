using Mobile.Data.Repositories;
using Mobile.Data;
using Mobile.Framework.Infrastructure;
using System.Linq;
using Mobile.Object;
using Mobile.Framework.Extensions;
using Mobile.Framework;

namespace Mobile.Business
{
    public class BLLConfig : BLLBase, IBLLConfig
    {
        private readonly ISystemConfigRepository repSystemConfig;

        #region "Constructor"
        public BLLConfig()
        {
            repSystemConfig = new SystemConfigRepository(DatabaseFactory);
        }

        public BLLConfig(IDatabaseFactory<MobileContext> iDatabaseFactory)
        {
            repSystemConfig = new SystemConfigRepository(iDatabaseFactory);
        }
        #endregion

        #region "Function Support"
        public SystemConfig GetSystemConfig(string key , bool isActived)
        {
            return repSystemConfig.GetAll().Where(p => p.ConfigID == key && p.IsActived == isActived ).FirstOrDefault();
        }

        public SystemConfig GetSystemConfig(string key)
        {
            return repSystemConfig.GetAll().Where(p => p.ConfigID == key).FirstOrDefault();
        }

        public IQueryable<SystemConfig> GetSystemConfigs(int pageIndex, int pageSize , out int totalRecords)
        {
            var query = GetSystemConfigs();
            totalRecords = query.Count();
            return query
                .OrderBy(p=>p.ConfigID)
                .Page(pageIndex,pageSize);
        }

        public IQueryable<SystemConfig> GetSystemConfigs()
        {
            var query = repSystemConfig.GetAll();
            return query;
        }

        #endregion

        public SystemConfig GetConfig(string configId)
        {
            return repSystemConfig.GetAll().Where(p => p.ConfigID == configId).FirstOrDefault();
        }

        public IQueryable<SystemConfig> GetConfigs(string keyword, bool? isActived)
        {
            var query = repSystemConfig.GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(p => p.ConfigName.ToLower().Contains(keyword));
            }
            if (isActived.HasValue)
            {
                query = query.Where(p => p.IsActived == isActived);
            }
            return query;
        }

        public Framework.PagedResult<SystemConfig> GetConfigsPaging(string keyword, bool? isActived, Framework.PagingInput pagingInput)
        {
            var query = GetConfigs(keyword, isActived);
            var result = new PagedResult<SystemConfig>(query);
            result.SetPaging(pagingInput);
            return result;       
        }

        public bool AddConfig(SystemConfig objConfig)
        {
            try
            {
                repSystemConfig.Add(objConfig);
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteConfig(string configId, int userId)
        {
            try
            {
                repSystemConfig.Delete(p => p.ConfigID == configId);
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateConfig(SystemConfig objConfig)
        {
            try
            {
                repSystemConfig.Update(objConfig);
                SaveChanges();
                return true;
            }
            catch 
            {
                return false; ;
            }
        }
    }
}
