import { useContext } from "react";
import RecipeContext from "../../../context/RecipeContext";
import { Box, Field, Textarea } from "@chakra-ui/react";

export default function RecipeNotesSection() {
  const listLimit = 5;
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const hasNotes = newRecipe.notes.length > 0;
  return (
    <Box mt={10}>
      <Field.Root>
        <Field.Label
          color={"secondary.700"}
          fontFamily={"heading"}
          fontSize={"5xl"}
          mb={5}
        >
          Special Notes <Field.RequiredIndicator />
        </Field.Label>
        <Textarea
          w={{ base: "85vw", md: "65vw", lg: "40vw" }}
          bg={"neutral.100"}
          color={"secondary.500"}
          placeholder="Add any special notes here, like ingredient variations, allergen information, or your own helpful tips for making this recipe just right."
          variant="subtle"
          fontSize={"md"}
          minH={"5lh"}
          maxH="10lh"
          lineHeight="1.5"
          autoresize
        />
        <Field.HelperText>Max 500 characters.</Field.HelperText>
      </Field.Root>
    </Box>
  );
}
