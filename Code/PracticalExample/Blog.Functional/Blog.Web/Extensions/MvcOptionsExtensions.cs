namespace Blog.Web.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class MvcOptionsExtensions
    {
        public static MvcOptions AddAutoValidateAntiforgeryToken(this MvcOptions options)
        {
            options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            return options;
        }
    }
}
