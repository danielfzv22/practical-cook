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
import { useFieldArray, useFormContext } from "react-hook-form";

export default function RecipeInstructionsSection() {
  const { control, register } = useFormContext();

  const { fields, append, remove } = useFieldArray({
    control,
    name: "steps",
  });

  return (
    <VStack alignItems={"baseline"} mt={5}>
      {fields.map((step, index) => {
        //{steps.map((step) => {
        const isBeforeStarting = step.stepOrder === 0;
        return (
          <Box
            key={step.stepOrder}
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
                  {!isBeforeStarting && step.stepOrder + "."}
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
                      fontSize="inherit"
                      fontWeight={"inherit"}
                      variant={"flushed"}
                      placeholder="Step Title (e.g., 'Chop vegetables')"
                      borderColor={"secondary.500"}
                      w={{ base: "60vw", md: "35vw", lg: "20vw" }}
                      {...register(`steps.${index}.title`)}
                    />
                  )}
                </Field.Label>
              </Field.Root>
              <Field.Root>
                <Textarea
                  name="description"
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
                  {...register(`steps.${index}.description`)}
                />
              </Field.Root>
            </Fieldset.Root>
          </Box>
        );
      })}
      <HStack wrap="wrap">
        <Button
          onClick={() =>
            append({ stepOrder: fields.length, title: "", description: "" })
          }
        >
          <HiPlus />
          <Text color={"neutral.900"} textStyle="sm">
            Add new step
          </Text>
        </Button>
        <Button
          disabled={fields.length <= 2}
          onClick={() => remove(fields.length - 1)}
        >
          <HiMinus />
          <Text color={"neutral.900"} textStyle="sm">
            Remove Last Step
          </Text>
        </Button>
      </HStack>
    </VStack>
  );
}
