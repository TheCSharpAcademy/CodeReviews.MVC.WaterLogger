using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;

namespace MVC.RecipeLogger.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly RecipeContext _context;

        public IndexModel(RecipeContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Recipe = await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
        }
    }
}
