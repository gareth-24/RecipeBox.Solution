using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Tag
  {
    public int TagId { get; set; }
    [Required(ErrorMessage = "The tag's title can't be empty!")]
    public string Title { get; set; }
    public List<RecipeTag> JoinEntities { get;} //collection navigation property
    public ApplicationUser User { get; set; }
  }
}