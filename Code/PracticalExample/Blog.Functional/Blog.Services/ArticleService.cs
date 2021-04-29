namespace Blog.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Extensions;
    using Extensions;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext db;
        private readonly IMapper mapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public ArticleService(
            BlogDbContext db, 
            IMapper mapper, 
            IDateTimeProvider dateTimeProvider)
        {
            this.db = db;
            this.mapper = mapper;
            this.dateTimeProvider = dateTimeProvider;
        }

        public Task<List<ArticleListingServiceModel>> All(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            => this.All<ArticleListingServiceModel>(page, pageSize, publicOnly);

        public Task<List<TModel>> All<TModel>(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            where TModel : class
            => this.db.Articles
                .FilterOn(publicOnly, a => a.IsPublic)
                .OrderByDescending(a => a.PublishedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
                
        public Task<List<ArticleForUserListingServiceModel>> ByUser(string userId)
            => this.db
                .Articles
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.PublishedOn)
                .ProjectTo<ArticleForUserListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public Task<bool> IsByUser(int id, string userId)
            => this.db
                .Articles
                .AnyAsync(a => a.Id == id && a.UserId == userId);

        public Task<Maybe<ArticleDetailsServiceModel>> Details(int id)
            => this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ToMaybe();

        public Task<int> Total()
            => this.db
                .Articles
                .Where(a => a.IsPublic)
                .CountAsync();

        public Task<int> Add(string title, string content, string userId)
            => this.db.Create(
                new Article
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                },
                a => a.Id);

        public Task Edit(int id, string title, string content) 
            => this.db.Update<Article>(id, article =>
            {
                article.Title = title;
                article.Content = content;
                article.IsPublic = false;
            });

        public Task Delete(int id)
            => this.db.Remove<Article>(id);

        public Task ChangeVisibility(int id)
            => this.db
                .Get<Article>(id)
                .Tap(a => a.IsPublic = !a.IsPublic)
                .Where(a => a.PublishedOn == null)
                .Tap(a => a.PublishedOn = this.dateTimeProvider.Now())
                .Execute(a => db.SaveChangesAsync());
    }
}
