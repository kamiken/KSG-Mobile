using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Object;
using Mobile.Framework;

namespace Mobile.Business
{
    public interface IBLLCompany
    {
        Company GetCompany(int companyId);
        IQueryable<Company> GetCompanies(string keyword, bool? isActived, bool? isRootSite, bool? isDeleted);
        PagedResult<Company> GetCompaniesPaging(string keyword, bool? isActived, bool? isRootSite, bool? isDeleted, PagingInput pagingInput);

    }
}
