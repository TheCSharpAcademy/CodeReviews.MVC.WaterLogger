using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;

namespace MVC.RecipeLogger.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly MVC.RecipeLogger.Context.RecipeContext _context;

        public IndexModel(MVC.RecipeLogger.Context.RecipeContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Recipe = await _context.Recipes.ToListAsync();
        }
    }
}
