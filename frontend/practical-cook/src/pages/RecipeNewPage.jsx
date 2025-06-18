import { redirect } from "react-router-dom";
import RecipeMaker from "../components/Recipes/RecipeMaker";
import { authFetch, getAuthToken } from "../util/http";

function RecipeNewPage() {
  return <RecipeMaker />;
}

export default RecipeNewPage;
