import { useContext, useState } from "react";
import Input from "../../UI/Input";
import Modal from "../../UI/Modal";
import RecipeContext from "../../../context/RecipeContext";
import { Box } from "@chakra-ui/react";

export default function RecipeTitleModal({ createNewRecipe }) {
  const ctxRecipe = useContext(RecipeContext);
  const defaultValue = {
    title: ctxRecipe.recipe.name,
    description: ctxRecipe.recipe.description,
  };

  const [infoNewRecipeTitle, setInfoNewRecipeTitle] = useState(defaultValue);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setInfoNewRecipeTitle((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    ctxRecipe.editRecipeTitle(infoNewRecipeTitle);
    createNewRecipe(infoNewRecipeTitle);
  };

  const handleOnClose = () => {
    ctxRecipe.hideModal();
    setInfoNewRecipeTitle(defaultValue);
  };

  return (
    <Box>
      <h3>Title...</h3>
      <form onSubmit={handleSubmit}>
        <Input
          id="title"
          label="Recipe Title:"
          type="text"
          value={infoNewRecipeTitle.title}
          onChange={handleChange}
          required
        />
        <Input
          id="description"
          label="Description:"
          type="text"
          value={infoNewRecipeTitle.description}
          onChange={handleChange}
          required
        />
        <div id="title-actions" className="actions">
          <button className="ok">Create!</button>
          <button className="cancel" onClick={handleOnClose} type="button">
            Cancel
          </button>
        </div>
      </form>
    </Box>
  );
}
