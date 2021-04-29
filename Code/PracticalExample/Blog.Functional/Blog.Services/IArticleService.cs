namespace Blog.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Models;

    public interface IArticleService
    {
        Task<List<ArticleListingServiceModel>> All(
            int page = 1, 
            int pageSize = ServicesConstants.ArticlesPerPage, 
            bool publicOnly = true);

        Task<List<TModel>> All<TModel>(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            where TModel : class;

        Task<List<ArticleForUserListingServiceModel>> ByUser(string userId);

        Task<bool> IsByUser(int id, string userId);

        Task<Maybe<ArticleDetailsServiceModel>> Details(int id);

        Task<int> Total();

        Task<int> Add(string title, string content, string userId);
        
        Task Edit(int id, string title, string content);

        Task Delete(int id);

        Task ChangeVisibility(int id);
    }
}
