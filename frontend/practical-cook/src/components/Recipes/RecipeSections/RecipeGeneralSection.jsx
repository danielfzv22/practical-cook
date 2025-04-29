import { useContext } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeGeneralModal from "../RecipeForm/RecipeGeneralModal";
import RecipeContext from "../../../context/RecipeContext";

export default function RecipeGeneralSection() {
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const difficultyValue = ["Easy", "Medium", "Hard"];

  return (
    <>
      <SectionButton
        className={`general ${ctxRecipe.recipe.name === "" ? "invalid" : "valid"}`}
        isSelected={ctxRecipe.selectedForm === "general"}
        onSelect={() => ctxRecipe.showModal("general")}
        isDisabled={ctxRecipe.recipe.name === ""}
      >
        <ul>
          <li>
            <label>Prep time:</label>
            <p>{newRecipe.prepTime} minutes</p>
          </li>
          <li>
            <label>Servings:</label>
            <p>{newRecipe.servings}</p>
          </li>
          <li>
            <label>Calories:</label>
            <p>{newRecipe.calories}</p>
          </li>
          <li>
            <label>Difficulty:</label>
            <p>{difficultyValue[newRecipe.difficulty]}</p>
          </li>
        </ul>
        <div>
          <label>{newRecipe.rating}</label>
        </div>
      </SectionButton>
      <RecipeGeneralModal />
    </>
  );
}
