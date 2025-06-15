import { useLoaderData } from "react-router-dom";
import RecipeMaker from "../components/Recipes/RecipeMaker";

function RecipeDetailPage() {
  const res = useLoaderData();
  return <RecipeMaker recipe={res.data} />;
}

export default RecipeDetailPage;

export async function loader({ request, params }) {
  const id = params.recipeId;
  const response = await fetch(`http://localhost:5086/recipes/${id}`);

  if (!response.ok) {
    throw new Response(
      JSON.stringify({ isError: true, message: "Failed to fetch recipes" }),
      { status: 500 }
    );
  }
  return response;
}
