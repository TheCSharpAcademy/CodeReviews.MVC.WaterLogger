using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.RecipeLogger.Context;

namespace MVC.RecipeLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecipeContext _recipeContext;

        public IndexModel(RecipeContext context)
        {
            _recipeContext = context;
        }

        public int Count = default!;

        public void OnGet()
        {
            Count = _recipeContext.Recipes.Count();
        }
    }
}
