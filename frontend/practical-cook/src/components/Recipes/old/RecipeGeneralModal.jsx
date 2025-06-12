import { useContext, useState } from "react";
import Input from "../../UI/Input";
import Modal from "../../UI/Modal";
import RecipeContext from "../../../context/RecipeContext";
import { updateRecipeGeneral } from "../../../http";
import { Box } from "@chakra-ui/react";

export default function RecipeGeneralModal() {
  const ctxRecipe = useContext(RecipeContext);
  const defaultValue = {
    prepTime: ctxRecipe.recipe.prepTime,
    servings: ctxRecipe.recipe.servings,
    calories: ctxRecipe.recipe.calories,
    difficulty: ctxRecipe.recipe.difficulty,
    rating: ctxRecipe.recipe.rating,
  };

  const [infoNewRecipeGeneral, setInfoNewRecipeGeneral] =
    useState(defaultValue);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setInfoNewRecipeGeneral((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      await updateRecipeGeneral({
        id: ctxRecipe.recipe.id,
        name: ctxRecipe.recipe.name,
        preparationTime: +infoNewRecipeGeneral.prepTime,
        calories: +infoNewRecipeGeneral.calories,
        difficulty: +infoNewRecipeGeneral.difficulty,
        rating: +infoNewRecipeGeneral.rating,
        servings: +infoNewRecipeGeneral.servings,
      });
      ctxRecipe.editRecipeGeneral(infoNewRecipeGeneral);
    } catch (error) {
      ctxRecipe.showError({
        success: false,
        message: "Error updating recipe general information",
      });
    }
  }

  const handleOnClose = () => {
    ctxRecipe.hideModal();
    setInfoNewRecipeGeneral(defaultValue);
  };

  return (
    <Box>
      <h3>General Information...</h3>
      <form onSubmit={handleSubmit}>
        <Input
          id="prepTime"
          label="Preparation Time (Minutes):"
          value={infoNewRecipeGeneral.prepTime}
          onChange={handleChange}
          type="number"
          required
        />
        <Input
          id="servings"
          label="Servings:"
          value={infoNewRecipeGeneral.servings}
          onChange={handleChange}
          type="number"
          min={1}
          max={10}
          required
        />
        <Input
          id="calories"
          label="Calories:"
          value={infoNewRecipeGeneral.calories}
          onChange={handleChange}
          type="number"
          required
        />
        <div className="control">
          <label htmlFor="difficulty">Difficulty</label>
          <select
            id="difficulty"
            name="difficulty"
            onChange={handleChange}
            value={infoNewRecipeGeneral.difficulty}
          >
            <option value={0}>Easy</option>
            <option value={1}>Medium</option>
            <option value={2}>Hard</option>
          </select>
        </div>
        <Input
          id="rating"
          label="Rating:"
          value={infoNewRecipeGeneral.rating}
          onChange={handleChange}
          type="number"
          min={1}
          max={5}
        />
        <div className="actions">
          <button className="ok">Ok</button>
          <button className="cancel" onClick={handleOnClose} type="button">
            Cancel
          </button>
        </div>
      </form>
    </Box>
  );
}
