using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;

namespace MVC.RecipeLogger.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly RecipeContext _context;

        public CreateModel(RecipeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        private int _numberOfIngredients = 1;

        [BindProperty]
        public int NumberOfIngredients {
            get => _numberOfIngredients;
            set
            {
                if (value < 1) 
                    return;

                _numberOfIngredients = value;
            }
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Recipe.Ingredients ??= [];

            for (int i = 0; i < NumberOfIngredients; i++)
            {
                Recipe.Ingredients.Add(new Ingredient());
                ModelState.Remove($"Recipe.Ingredients[{i}].Recipe");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var ingredient in Recipe.Ingredients)
            {
                ingredient.Recipe = Recipe;
            }

            Recipe.Ingredients = Recipe.Ingredients
                .Where(ingredient => !string.IsNullOrWhiteSpace(ingredient.Name))
                .ToList();

            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}