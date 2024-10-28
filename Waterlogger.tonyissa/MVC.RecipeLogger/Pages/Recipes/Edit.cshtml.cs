using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;

namespace MVC.RecipeLogger.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly RecipeContext _context;

        public EditModel(RecipeContext context)
        {
            _context = context;
        }

        private int _numberOfIngredients;

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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe =  await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            Recipe = recipe;
            NumberOfIngredients = recipe.Ingredients.Count;
            return Page();
        }

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

            var oldIngredients = await _context.Ingredients.Where(i => i.RecipeId == Recipe.Id).ToListAsync();
            _context.Ingredients.RemoveRange(oldIngredients);
            _context.Recipes.Update(Recipe);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
