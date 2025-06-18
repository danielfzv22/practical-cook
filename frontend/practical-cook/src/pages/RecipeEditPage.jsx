import { useRouteLoaderData } from "react-router-dom";
import RecipeMaker from "../components/Recipes/RecipeMaker";

function RecipeEditPage() {
  const { recipe } = useRouteLoaderData("recipe-detail");
  console.log(recipe);
  return <RecipeMaker recipe={recipe.data} />;
}

export default RecipeEditPage;
