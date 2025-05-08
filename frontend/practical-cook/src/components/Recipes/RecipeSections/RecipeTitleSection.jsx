import { useContext, useState } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeTitleModal from "../RecipeForm/RecipeTitleModal";
import RecipeContext from "../../../context/RecipeContext";
import { createRecipe } from "../../../http";

export default function RecipeTitleSection() {
  const [errorUpdatingPlaces, seterrorUpdatingPlaces] = useState();
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;

  async function handleCreateRecipe(recipe) {
    console.log(recipe);
    try {
      await createRecipe({
        name: recipe.title,
        description: recipe.description,
      });
    } catch (error) {
      seterrorUpdatingPlaces({
        message: error.message || "Failed to create recipe.",
      });
    }
  }

  return (
    <>
      <SectionButton
        className="title valid"
        isSelected={ctxRecipe.selectedForm === "title"}
        onSelect={() => ctxRecipe.showModal("title")}
      >
        <h2>Recipe:</h2>
        <div>
          <h1>{newRecipe.name || "Your new creation"}</h1>
          <p>{newRecipe.description || "Start from here!"}</p>
        </div>
      </SectionButton>
      <RecipeTitleModal createNewRecipe={handleCreateRecipe} />
    </>
  );
}
