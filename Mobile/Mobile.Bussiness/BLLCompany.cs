using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Data.Repositories;
using Mobile.Object;
using Mobile.Framework;

namespace Mobile.Business
{
    public class BLLCompany : BLLBase , IBLLCompany
    {
        private readonly ICompanyRepository repCompany;

        public BLLCompany()
        {
            repCompany = new CompanyRepository(DatabaseFactory);
        }        

        public Company GetCompany(int companyId) {
            var obj = repCompany.GetById(companyId);
            return obj;
        }

        public IQueryable<Company> GetCompanies(string keyword , bool? isActived, bool? isRootSite, bool? isDeleted)
        {
            var query = repCompany.GetAll();                 
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(p => p.CompanyName.ToLower().Contains(keyword));
            }
            if (isActived.HasValue)
            {
                query = query.Where(p => p.IsActived == isActived);
            }
            if (isRootSite.HasValue)
            {
                query = query.Where(p=>p.IsRootSite == isRootSite);
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted);
            }
            return query;
        }

        public PagedResult<Company> GetCompaniesPaging(string keyword, bool? isActived, bool? isRootSite, bool? isDeleted, PagingInput pagingInput)
        {
            var query = GetCompanies(keyword, isActived, isRootSite, isDeleted);
            var result = new PagedResult<Company>(query);
            result.SetPaging(pagingInput);
            return result;                 
        }
    }
}
