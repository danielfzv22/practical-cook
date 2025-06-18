import { useLoaderData, useRouteLoaderData } from "react-router-dom";
import RecipeMaker from "../components/Recipes/RecipeMaker";
import { fetchIngredients, fetchRecipe, fetchUtensils } from "../util/http";

function RecipeDetailPage() {
  const { recipe } = useRouteLoaderData("recipe-detail");
  const recipeData = !recipe ? "" : recipe.data;
  return <RecipeMaker recipe={recipeData} />;
}

export default RecipeDetailPage;

export async function loader({ params }) {
  const { recipeId } = params;

  const [recipe, utensilsData, ingredientsData] = await Promise.all([
    fetchRecipe(recipeId),
    fetchUtensils(),
    fetchIngredients(),
  ]);

  return { recipe, utensilsData, ingredientsData };
}
