using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ArticleCommentRepository : RepositoryBase<MobileContext, ArticleComment> , IArticleCommentRepository
    {
        public ArticleCommentRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IArticleCommentRepository : IRepository<ArticleComment>
    {
    }
    
}
