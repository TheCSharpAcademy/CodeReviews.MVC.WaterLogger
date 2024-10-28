using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public int NumberOfIngredients
        {
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
            if (Recipe.Ingredients.Count < NumberOfIngredients)
            {
                for (int i = Recipe.Ingredients.Count; i < NumberOfIngredients; i++)
                {
                    Recipe.Ingredients.Add(new Ingredient());
                }
            }
            else if (Recipe.Ingredients.Count > NumberOfIngredients)
            {
                for (int i = Recipe.Ingredients.Count; i > NumberOfIngredients; i--)
                {
                    Recipe.Ingredients.Remove(Recipe.Ingredients[i - 1]);
                }
            }
            else
            {
                for (int i = 0; i < NumberOfIngredients; i++)
                {
                    ModelState.Remove($"Recipe.Ingredients[{i}].Recipe");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}