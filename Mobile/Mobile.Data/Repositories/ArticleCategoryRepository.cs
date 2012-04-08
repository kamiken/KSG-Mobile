using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ArticleCategoryRepository : RepositoryBase<MobileContext, ArticleCategory> , IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
    }
    
}
