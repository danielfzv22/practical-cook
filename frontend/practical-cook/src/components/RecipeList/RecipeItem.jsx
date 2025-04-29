import { useContext } from "react";
import RecipeContext from "../../context/RecipeContext";
import "./RecipeList.css";
import { fetchRecipeById } from "../../http";

export default function RecipeItem({ id, title, description }) {
  const ctxRecipe = useContext(RecipeContext);

  async function handleSelectRecipe(id) {
    const recipe = await fetchRecipeById(id);
    console.log(recipe);
    console.log({
      ...recipe,
      prepTime: recipe.preparationTime,
      instructions: recipe.steps,
    });
    ctxRecipe.editRecipe({
      ...recipe,
      prepTime: recipe.preparationTime,
      instructions: recipe.steps,
      notes: [],
    });
  }

  return (
    <li onClick={() => handleSelectRecipe(id)}>
      <div>
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </li>
  );
}
