namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using CSharpFunctionalExtensions;
    using Controllers.Extensions;
    using Data.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesController(
            IArticleService articleService,
            IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> All([FromQuery]int page = 1) 
            => this.View(new ArticleListingViewModel
            {
                Articles = await this.articleService.All(page),
                Total = await this.articleService.Total(),
                Page = page
            });

        public Task<IActionResult> Details(int id)
            => this.articleService
                .Details(id)
                .Where(a => a.IsPublic 
                    || a.Author == this.User.Identity.Name 
                    || this.User.IsAdministrator())
                .Match(
                    Some: a => View(a),
                    None: NotFound);

        [HttpGet]
        [Authorize]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize]
        public Task<IActionResult> Create(ArticleFormModel article)
            => this.ModelState
                .ToResult()
                .Ensure(ms => ms.IsValid, "Model state is not valid.")
                .Tap(async _ => await this.articleService
                    .Add(article.Title, article.Content, this.User.GetId()))
                .Tap(_ => this.TempData.Add(
                    ControllerConstants.SuccessMessage, 
                    "Article created successfully is waiting for approval!"))
                .Match(
                    Some: _ => this.RedirectToAction(nameof(this.Mine)),
                    None: () => this.View(article));

        [HttpGet]
        [Authorize]
        public Task<IActionResult> Edit(int id)
            => this.articleService
                .Details(id)
                .Where(a => a.Author == this.User.Identity.Name 
                    || this.User.IsAdministrator())
                .Map(a => this.mapper.Map<ArticleFormModel>(a))
                .Match(
                    Some: a => View(a),
                    None: NotFound);

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ArticleFormModel article)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }
            
            if (this.ModelState.IsValid)
            {
                await this.articleService.Edit(id, article.Title, article.Content);

                this.TempData.Add(ControllerConstants.SuccessMessage, "Article edited successfully and is waiting for approval!");

                return this.RedirectToAction(nameof(this.Mine));
            }

            return this.View(article);
        }
        
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            return this.View(id);
        }
        
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            await this.articleService.Delete(id);
            
            this.TempData.Add(ControllerConstants.SuccessMessage, "Article deleted successfully!");

            return this.RedirectToAction(nameof(this.Mine));
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            var articles = await this.articleService.ByUser(this.User.GetId());

            return this.View(articles);
        }
    }
}
