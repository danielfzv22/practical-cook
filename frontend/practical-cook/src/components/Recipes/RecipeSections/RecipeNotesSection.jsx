import { useContext } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeContext from "../../../context/RecipeContext";
import RecipeNotesModal from "../RecipeMakerSections/RecipeNotesModal";

export default function RecipeNotesSection() {
  const listLimit = 5;
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const hasNotes = newRecipe.notes.length > 0;
  return (
    <>
      <SectionButton
        className={`notes ${
          ctxRecipe.recipe.name === "" ? "invalid" : "valid"
        }`}
        isSelected={ctxRecipe.selectedForm === "notes"}
        onSelect={() => ctxRecipe.showModal("notes")}
        isDisabled={ctxRecipe.recipe.name === ""}
      >
        <h3>Notes:</h3>
        <ul>
          {hasNotes ? (
            newRecipe.notes.map((note) => {
              if (index < listLimit) {
                return <li key={note}>{note}</li>;
              } else if (index === listLimit) {
                return <li>{newRecipe.notes.length - index} more...</li>;
              }
              return;
            })
          ) : (
            <li>...</li>
          )}
        </ul>
      </SectionButton>
      <RecipeNotesModal />
    </>
  );
}
