import { useContext, useState } from "react";
import RecipeContext from "../../../context/RecipeContext";
import {
  Box,
  Button,
  Collapsible,
  Field,
  Flex,
  HStack,
  IconButton,
  Input,
  Text,
  Textarea,
  VStack,
} from "@chakra-ui/react";
import { HiMinus, HiPlus } from "react-icons/hi";

export default function RecipeInstructionsSection() {
  const [lastStep, setLastStep] = useState(1);
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const hasInstructions = newRecipe.instructions.length > 0;
  return (
    <VStack alignItems={"baseline"} mt={5}>
      <Field.Root w={{ base: "50vw", md: "50vw", lg: "30vw" }} mb={5}>
        <Field.Label
          color={"secondary.500"}
          fontSize={"xl"}
          fontWeight={"medium"}
        >
          {lastStep}.
          <Input
            fontSize="inherit"
            fontWeight={"inherit"}
            variant={"flushed"}
            placeholder="Step Title (e.g., 'Chop vegetables')"
            borderColor={"secondary.500"}
            w={{ base: "60vw", md: "35vw", lg: "20vw" }}
          ></Input>
        </Field.Label>
        <Textarea
          w={{ base: "70vw", md: "55vw", lg: "35vw" }}
          color={"neutral.900"}
          size={"lg"}
          variant={"flushed"}
          placeholder="Step Description (e.g., 'Finely chop the carrots and onions...')"
          borderColor={"secondary.500"}
          autoresize
        />
      </Field.Root>
      <HStack wrap="wrap">
        <Button>
          <HiPlus />
          <Text color={"neutral.900"} textStyle="sm">
            Add new step
          </Text>
        </Button>
        <Button>
          <HiMinus />
          <Text color={"neutral.900"} textStyle="sm">
            Remove Last Step
          </Text>
        </Button>
      </HStack>
    </VStack>
  );
}
