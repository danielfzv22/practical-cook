import { useContext } from "react";
import SectionButton from "../../UI/SectionButton";
import RecipeContext from "../../../context/RecipeContext";
import RecipeUtensilsModal from "../RecipeMakerSections/RecipeUtensilsSection";

export default function RecipeUtensilsSectionOld() {
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const limitLists = 10;

  const defaultList = (
    <>
      <li>...</li>
      <li>...</li>
      <li>...</li>
    </>
  );

  const hasUtensils = newRecipe.utensils.length > 0;

  return (
    <>
      <SectionButton
        className={`utensils ${
          ctxRecipe.recipe.name === "" ? "invalid" : "valid"
        }`}
        isSelected={ctxRecipe.selectedForm === "utensils"}
        onSelect={() => ctxRecipe.showModal("utensils")}
        isDisabled={ctxRecipe.recipe.name === ""}
      >
        <h3>Utensils</h3>
        {hasUtensils ? (
          <ul>
            {newRecipe.utensils.map((utensil, index) => {
              if (index < limitLists) {
                return <li key={utensil.id}>{utensil.name}</li>;
              } else if (index === limitLists) {
                return (
                  <li key={utensil.id}>
                    {newRecipe.utensils.length - index} more...
                  </li>
                );
              }
              return;
            })}
          </ul>
        ) : (
          <ul>{defaultList}</ul>
        )}
      </SectionButton>
      <RecipeUtensilsModal />
    </>
  );
}
