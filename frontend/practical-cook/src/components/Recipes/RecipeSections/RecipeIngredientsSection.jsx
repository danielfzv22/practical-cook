import { useContext } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeContext from "../../../context/RecipeContext";

export default function RecipeIngredientsSection() {
  const listLimit = 16;
  const defaultList = (
    <>
      <li>...</li>
      <li>...</li>
      <li>...</li>
    </>
  );

  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;

  const hasIngredients = newRecipe.ingredients.length > 0;
  return (
    <SectionButton
      className={`ingredients ${
        ctxRecipe.recipe.name === "" ? "invalid" : "valid"
      }`}
      isSelected={ctxRecipe.selectedForm === "ingredients"}
      onSelect={() => ctxRecipe.showModal("ingredients")}
      isDisabled={ctxRecipe.recipe.name === ""}
    >
      <h3>Ingredients:</h3>
      {hasIngredients ? (
        <ul>
          {newRecipe.ingredients.map((ingredient, index) => {
            if (index < listLimit) {
              return <li key={ingredient.ingredient.id}>{ingredient.ingredient.name}</li>;
            } else if (index === listLimit) {
              return (
                <li key={ingredient.ingredient.id}>
                  {newRecipe.ingredients.length - index} more...
                </li>
              );
            }
          })}
        </ul>
      ) : (
        <ul>{defaultList}</ul>
      )}
    </SectionButton>
  );
}
