import { useState } from "react";
import {
  Box,
  Button,
  Field,
  Fieldset,
  HStack,
  Input,
  Text,
  Textarea,
  VStack,
} from "@chakra-ui/react";
import { HiMinus, HiPlus } from "react-icons/hi";

export default function RecipeInstructionsSection() {
  const stepsDefault = [
    { order: 0, title: "Before Starting", description: "" },
    { order: 1, title: "", description: "" },
    { order: 2, title: "", description: "" },
    { order: 3, title: "", description: "" },
  ];

  const [steps, setSteps] = useState(stepsDefault);

  const handleAddStep = () => {
    setSteps((prev) => [
      ...prev,
      { order: prev.length, title: "", description: "" },
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
      const newSteps = [...prev];
      newSteps[order] = { ...newSteps[order], [name]: value };
      return newSteps;
    });
  };

  return (
    <VStack alignItems={"baseline"} mt={5}>
      {steps.map((step) => {
        const isBeforeStarting = step.order === 0;
        return (
          <Box
            key={step.order}
            w={{ base: "50vw", md: "50vw", lg: "30vw" }}
            mb={5}
          >
            <Fieldset.Root>
              <Field.Root>
                <Field.Label
                  color={"secondary.500"}
                  fontSize={"xl"}
                  fontWeight={"medium"}
                >
                  {!isBeforeStarting && step.order + "."}
                  {isBeforeStarting ? (
                    <Text
                      textAlign={"revert-layer"}
                      fontSize="inherit"
                      fontWeight={"inherit"}
                    >
                      Before Starting
                    </Text>
                  ) : (
                    <Input
                      name="title"
                      onChange={(e) => {
                        handleStepInput(e, step.order);
                      }}
                      fontSize="inherit"
                      fontWeight={"inherit"}
                      variant={"flushed"}
                      value={step.title}
                      placeholder="Step Title (e.g., 'Chop vegetables')"
                      borderColor={"secondary.500"}
                      w={{ base: "60vw", md: "35vw", lg: "20vw" }}
                    />
                  )}
                </Field.Label>
              </Field.Root>
              <Field.Root>
                <Textarea
                  name="description"
                  value={step.description}
                  onChange={(e) => {
                    handleStepInput(e, step.order);
                  }}
                  w={{ base: "70vw", md: "55vw", lg: "35vw" }}
                  color={"neutral.900"}
                  size={"lg"}
                  variant={`${isBeforeStarting ? "outline" : "flushed"}`}
                  placeholder={`${
                    isBeforeStarting
                      ? "Optional: list anything to prepare before cooking, like preheating the oven or washing vegetables..."
                      : "Step Description (e.g., 'Finely chop the carrots and onions...')"
                  }`}
                  borderColor={"secondary.500"}
                  autoresize
                />
              </Field.Root>
            </Fieldset.Root>
          </Box>
        );
      })}
      <HStack wrap="wrap">
        <Button onClick={handleAddStep}>
          <HiPlus />
          <Text color={"neutral.900"} textStyle="sm">
            Add new step
          </Text>
        </Button>
        <Button disabled={steps.length <= 2} onClick={handleRemoveStep}>
          <HiMinus />
          <Text color={"neutral.900"} textStyle="sm">
            Remove Last Step
          </Text>
        </Button>
      </HStack>
    </VStack>
  );
}
