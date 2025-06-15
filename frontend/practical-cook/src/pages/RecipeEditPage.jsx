import { useLoaderData } from "react-router-dom";
import RecipeMaker from "../components/Recipes/RecipeMaker";
import { authFetch } from "../util/http";

function RecipeEditPage() {
  const { recipe } = useLoaderData();
  console.log(recipe.data);
  return <RecipeMaker recipe={recipe.data} />;
}

export default RecipeEditPage;

export async function loader({ request, params }) {
  const id = params.recipeId;
  const [recipeRes, utensilsRes, ingredientsRes] = await Promise.all([
    authFetch(`http://localhost:5086/recipes/${id}`),
    authFetch(`http://localhost:5086/utensils`),
    authFetch(`http://localhost:5086/ingredients`),
  ]);

  if (!recipeRes.ok || !utensilsRes.ok || !ingredientsRes.ok) {
    throw new Response(
      JSON.stringify({ isError: true, message: "Error loading data" }),
      { status: 500 }
    );
  }
  const recipe = await recipeRes.json();
  const utensilsData = await utensilsRes.json();
  const ingredientsData = await ingredientsRes.json();

  return { recipe, utensilsData, ingredientsData };
}
