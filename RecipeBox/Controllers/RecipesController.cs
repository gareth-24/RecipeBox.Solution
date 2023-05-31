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
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Recipes.ToList());
    }

    public ActionResult Details(int id)
    {
      Recipe thisRecipe = _db.Recipes
                              .Include(recipe => recipe.JoinIngredientRecipeEntities)
                              .ThenInclude(join => join.Ingredient)
                              .Include(recipe => recipe.JoinRecipeTagEntities)
                              .ThenInclude(join => join.Tag)
                              .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe)
    {
      if (!ModelState.IsValid)
      {
        return View(recipe);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        recipe.User = currentUser;
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      return View(thisRecipe);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit (Recipe recipe)
    {
      _db.Recipes.Update(recipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      return View(thisRecipe);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddIngredient(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
      return View(thisRecipe);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddIngredient(Recipe recipe, int ingredientId)
    {
      #nullable enable
      IngredientRecipe? joinEntity = _db.IngredientRecipes.FirstOrDefault(join => (join.IngredientId == ingredientId && join.RecipeId == recipe.RecipeId));
      #nullable disable
      if (joinEntity == null && ingredientId != 0)
      {
        _db.IngredientRecipes.Add(new IngredientRecipe() { IngredientId = ingredientId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      IngredientRecipe joinEntry = _db.IngredientRecipes.FirstOrDefault(entry => entry.IngredientRecipeId == joinId);
      _db.IngredientRecipes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddTag(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
      return View(thisRecipe);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int tagId)
    {
      #nullable enable
      RecipeTag? joinEntity = _db.RecipeTags.FirstOrDefault(join => (join.TagId == tagId && join.RecipeId == recipe.RecipeId));
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() { TagId = tagId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteTagJoin(int joinId)
    {
      RecipeTag joinEntry = _db.RecipeTags.FirstOrDefault(entry => entry.RecipeTagId == joinId);
      _db.RecipeTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}