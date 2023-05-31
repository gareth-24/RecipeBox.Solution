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
    [Required(ErrorMessage = "The recipe's instructions can't be empty!")]
    public string Instructions { get; set; }
    [Range(1, 10, ErrorMessage = "You must give a rating on a scale of 1 to 10.")]
    public int Rating { get; set; }
    public List<IngredientRecipe> JoinIngredientRecipeEntities { get;}
    public List<RecipeTag> JoinRecipeTagEntities { get;}
    public ApplicationUser User { get; set; }
  }
}