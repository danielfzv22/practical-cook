import { useContext, useState } from "react";
import RecipeContext from "../../../context/RecipeContext";
import {
  Box,
  Button,
  Collapsible,
  Field,
  Fieldset,
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
  const stepsDefault = [
    { order: 1, title: "", description: "" },
    { order: 2, title: "", description: "" },
    { order: 3, title: "", description: "" },
  ];

  const [steps, setSteps] = useState(stepsDefault);
  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;
  const hasInstructions = newRecipe.instructions.length > 0;

  const handleAddStep = () => {
    setSteps((prev) => [
      ...prev,
      { order: prev.length + 1, title: "", description: "" },
    ]);
  };
  const handleRemoveStep = () => {
    setSteps((prev) => {
      const instructions = [...prev];
      instructions.splice(prev.length - 1, 1);
      return instructions;
    });
  };
  const handleStepInput = (e, order) => {
    const { name, value } = e.target;
    setSteps((prev) => {
      const newSteps = [...prev]; // copia superficial del array
      newSteps[order] = { ...newSteps[order], [name]: value }; // copia el objeto modificado con el nuevo valor
      return newSteps;
    });
  };

  return (
    <VStack alignItems={"baseline"} mt={5}>
      {steps.map((step) => (
        <Fieldset.Root
          w={{ base: "50vw", md: "50vw", lg: "30vw" }}
          mb={5}
          key={step.order}
        >
          <Field.Root>
            <Field.Label
              color={"secondary.500"}
              fontSize={"xl"}
              fontWeight={"medium"}
            >
              {step.order}.
              <Input
                name="title"
                onChange={(e) => {
                  const order = step.order - 1;
                  handleStepInput(e, order);
                }}
                fontSize="inherit"
                fontWeight={"inherit"}
                variant={"flushed"}
                value={step.title}
                placeholder="Step Title (e.g., 'Chop vegetables')"
                borderColor={"secondary.500"}
                w={{ base: "60vw", md: "35vw", lg: "20vw" }}
              ></Input>
            </Field.Label>
          </Field.Root>
          <Field.Root>
            <Textarea
              name="description"
              value={step.description}
              onChange={(e) => {
                handleStepInput(e, step.order - 1);
              }}
              w={{ base: "70vw", md: "55vw", lg: "35vw" }}
              color={"neutral.900"}
              size={"lg"}
              variant={"flushed"}
              placeholder="Step Description (e.g., 'Finely chop the carrots and onions...')"
              borderColor={"secondary.500"}
              autoresize
            />
          </Field.Root>
        </Fieldset.Root>
      ))}
      <HStack wrap="wrap">
        <Button onClick={handleAddStep}>
          <HiPlus />
          <Text color={"neutral.900"} textStyle="sm">
            Add new step
          </Text>
        </Button>
        <Button disabled={steps.length <= 1} onClick={handleRemoveStep}>
          <HiMinus />
          <Text color={"neutral.900"} textStyle="sm">
            Remove Last Step
          </Text>
        </Button>
      </HStack>
    </VStack>
  );
}
