import "./Recipe.css";
import RecipeTitleSection from "./RecipeSections/RecipeTitleSection";
import RecipeGeneralSection from "./RecipeSections/RecipeGeneralSection";
import RecipeUtensilsSection from "./RecipeSections/RecipeUtensilsSection";
import RecipeIngredientsSection from "./RecipeSections/RecipeIngredientsSection";
import RecipeInstructionsSection from "./RecipeSections/RecipeInstructionsSection";
import RecipeNotesSection from "./RecipeSections/RecipeNotesSection";
import { useContext } from "react";
import RecipeContext from "../../context/RecipeContext";

export default function Recipe() {
  const ctxRecipe = useContext(RecipeContext);

  function handleCreateRecipe() {
    ctxRecipe.createRecipe();
  }

  function handleCancel() {
    ctxRecipe.cancelRecipe();
  }

  const recipe = (
    <>
      <button id="cancel-button" onClick={handleCancel}>Cancel</button>
      <div className="recipe-header">
        <RecipeTitleSection />
      </div>
      <div id="recipe-content">
        <section className="left-column">
          <RecipeGeneralSection />
          <RecipeUtensilsSection />
        </section>
        <section className="right-column">
          <RecipeIngredientsSection />
          <RecipeInstructionsSection />
        </section>
      </div>
      <div className="recipe-footer">
        <RecipeNotesSection />
      </div>
    </>
  );
  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        height: "95vh",
        width: "60%",
      }}
    >
      <div className="recipe">
        {ctxRecipe.editableRecipe ? (
          recipe
        ) : (
          <button id="create-button" onClick={handleCreateRecipe}>Create Recipe</button>
        )}
      </div>
    </div>
  );
}
