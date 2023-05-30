using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    [Required(ErrorMessage = "The recipe's name can't be empty!")]
    public string RecipeName { get; set; }
    [Required(ErrorMessage = "The recipe's description can't be empty!")]
    public string Description { get; set; }
    public List<IngredientRecipe> JoinIngredientRecipeEntities { get;}
    public List<RecipeTag> JoinRecipeTagEntities { get;}
    public ApplicationUser User { get; set; }
  }
}