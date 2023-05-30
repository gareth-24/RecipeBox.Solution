using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public IngredientsController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Ingredient> userIngredients = _db.Ingredients
                                      .Where(entry => entry.User.Id == currentUser.Id)
                                      .ToList();
      return View(userIngredients);
    }
    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Ingredient ingredient, int RecipeId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
        return View(ingredient);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        ingredient.User = currentUser;
        _db.Ingredients.Add(ingredient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    public ActionResult Details (int id)
    {
      Ingredient thisIngredient = _db.Ingredients
                .Include(ingredient => ingredient.JoinIngredientRecipeEntities)
                .FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    public ActionResult Edit(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View(thisIngredient);
    }
    [HttpPost]
    public ActionResult Edit (Ingredient ingredient)
    {
      _db.Ingredients.Update(ingredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete (int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      _db.Ingredients.Remove(thisIngredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View(thisIngredient);
    }
    [HttpPost]
    public ActionResult AddRecipe(Ingredient ingredient, int RecipeId)
    {
#nullable enable
    IngredientRecipe? joinEntity = _db.IngredientRecipes.FirstOrDefault(join => (join.IngredientId == ingredient.IngredientId && join.RecipeId == RecipeId));
#nullable disable
      if (RecipeId != 0 && joinEntity == null)
      {
        _db.IngredientRecipes.Add(new IngredientRecipe() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ingredient.IngredientId });
    }
  }
}