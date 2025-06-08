import { useContext } from "react";
import RecipeContext from "../../context/RecipeContext";
import "./RecipeList.css";
import { fetchRecipeById } from "../../http";
import { Button, Card, Image } from "@chakra-ui/react";

export default function RecipeItem({ recipe }) {
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
    <Card.Root
      maxW="sm"
      overflow="hidden"
      bg="#f0f7da" // Fondo de la card
      borderRadius="xl"
      boxShadow="md"
      _hover={{ boxShadow: "lg", bg: "gray.100" }} // Hover effect
    >
      <Image
        src="https://images.unsplash.com/photo-1555041469-a586c61ea9bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80"
        alt={recipe.name}
      />
      <Card.Body gap="2">
        <Card.Title color={"black"}>{recipe.name}</Card.Title>
        <Card.Description>{recipe.description}</Card.Description>
      </Card.Body>
      <Card.Footer gap="2">
        <Button variant="solid" onClick={() => handleSelectRecipe(recipe.id)}>
          View
        </Button>
        <Button variant="ghost">Add to my meals</Button>
      </Card.Footer>
    </Card.Root>
  );
}
