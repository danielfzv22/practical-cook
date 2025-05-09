import { useContext } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeContext from "../../../context/RecipeContext";
import RecipeInstructionsModal from "../RecipeForm/RecipeInstructionsModal";

export default function RecipeInstructionsSection() {
  const listLimit = 8;
  const defaultList = (
    <>
      <li>...</li>
      <li>...</li>
      <li>...</li>
    </>
  );
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const hasInstructions = newRecipe.instructions.length > 0;
  return (
    <>
      <SectionButton
        className={`instructions ${ctxRecipe.recipe.name === "" ? "invalid" : "valid"}`}
        isSelected={ctxRecipe.selectedForm === "instructions"}
        onSelect={() => ctxRecipe.showModal("instructions")}
        isDisabled={ctxRecipe.recipe.name === ""}
      >
        <div>
          <h3>Instructions:</h3>
          {hasInstructions ? (
            <ol>
              {newRecipe.instructions.map((step, index) => {
                if (index < listLimit) {
                  return (
                    <li key={step.order}>
                      <b>{step.title}: </b>
                      {step.description}
                    </li>
                  );
                } else if (index === listLimit) {
                  return (
                    <li>{newRecipe.instructions.length - index} more...</li>
                  );
                }
                return;
              })}
            </ol>
          ) : (
            <ol>{defaultList}</ol>
          )}
        </div>
      </SectionButton>
      <RecipeInstructionsModal />
    </>
  );
}
