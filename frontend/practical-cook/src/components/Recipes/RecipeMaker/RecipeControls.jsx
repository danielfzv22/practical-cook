import { Button, HStack } from "@chakra-ui/react";

function RecipeControls() {
  return (
    <HStack>
      <Button type="submit" color={"neutral.900"}>
        Create
      </Button>
      <Button color={"neutral.900"}>Clear</Button>
    </HStack>
  );
}

export default RecipeControls;
