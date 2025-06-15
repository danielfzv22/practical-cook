import { Button, HStack } from "@chakra-ui/react";
import { useFormContext } from "react-hook-form";

function RecipeControls() {
  const { reset } = useFormContext({
    defaultValues: {
      utensils: [],
      ingredients: [],
      instructions: [],
      name: "",
      description: "",
    },
  });

  return (
    <HStack>
      <Button type="submit" color={"neutral.900"}>
        Create
      </Button>
      <Button color={"neutral.900"} onClick={() => reset()}>
        Start Over
      </Button>
    </HStack>
  );
}

export default RecipeControls;
