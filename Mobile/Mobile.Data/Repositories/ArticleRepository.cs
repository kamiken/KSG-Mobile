using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ArticleRepository : RepositoryBase<MobileContext, Article> , IArticleRepository
    {
        public ArticleRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IArticleRepository : IRepository<Article>
    {
    }
    
}
