using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBox.Migrations
{
    public partial class AddIngredientRecipeDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipeId",
                table: "IngredientRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientRecipe",
                table: "IngredientRecipe");

            migrationBuilder.RenameTable(
                name: "IngredientRecipe",
                newName: "IngredientRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipe_RecipeId",
                table: "IngredientRecipes",
                newName: "IX_IngredientRecipes_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipe_IngredientId",
                table: "IngredientRecipes",
                newName: "IX_IngredientRecipes_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientRecipes",
                table: "IngredientRecipes",
                column: "IngredientRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipes_Ingredients_IngredientId",
                table: "IngredientRecipes",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipes_Recipes_RecipeId",
                table: "IngredientRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipes_Ingredients_IngredientId",
                table: "IngredientRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipes_Recipes_RecipeId",
                table: "IngredientRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientRecipes",
                table: "IngredientRecipes");

            migrationBuilder.RenameTable(
                name: "IngredientRecipes",
                newName: "IngredientRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipes_RecipeId",
                table: "IngredientRecipe",
                newName: "IX_IngredientRecipe_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipes_IngredientId",
                table: "IngredientRecipe",
                newName: "IX_IngredientRecipe_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientRecipe",
                table: "IngredientRecipe",
                column: "IngredientRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientId",
                table: "IngredientRecipe",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipeId",
                table: "IngredientRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
