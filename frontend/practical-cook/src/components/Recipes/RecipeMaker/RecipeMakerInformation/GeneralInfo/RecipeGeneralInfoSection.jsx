import {
  Box,
  Field,
  Fieldset,
  HStack,
  IconButton,
  NumberInput,
  SegmentGroup,
  Stack,
} from "@chakra-ui/react";
import { Controller, useFormContext } from "react-hook-form";
import {
  HiMinus,
  HiOutlineChartBar,
  HiOutlineClock,
  HiOutlineFire,
  HiOutlineUsers,
  HiPlus,
} from "react-icons/hi";

export default function RecipeGeneralInfoSection() {
  const { register, control } = useFormContext();

  return (
    <Box>
      <Fieldset.Root size={"lg"}>
        <Stack>
          <Fieldset.Legend
            color={"secondary.700"}
            fontFamily={"heading"}
            fontSize={"5xl"}
            mb={5}
          >
            General Information
          </Fieldset.Legend>
          <Fieldset.HelperText color="neutral.900">
            Don’t worry about being exact, just add your best estimates for
            things like prep time and calories. It helps others plan better!
          </Fieldset.HelperText>
        </Stack>
        <Fieldset.Content>
          <Stack gap="5" maxW="md">
            <Field.Root>
              <Field.Label color={"brand.700"} fontSize={"lg"}>
                <HiOutlineClock />
                Preparation Time (Minutes):
              </Field.Label>
              <NumberInput.Root
                defaultValue="15"
                color={"secondary.500"}
                variant={"flushed"}
              >
                <NumberInput.Input
                  width={"65px"}
                  textAlign={"center"}
                  fontSize={"lg"}
                  borderColor={"secondary.500"}
                  {...register("preparationTime")}
                />
              </NumberInput.Root>
            </Field.Root>
            <Field.Root>
              <Field.Label color={"brand.700"} fontSize={"lg"}>
                <HiOutlineUsers />
                Servings:
              </Field.Label>
              <NumberInput.Root
                defaultValue="2"
                color={"secondary.500"}
                variant={"flushed"}
                unstyled
                spinOnPress={false}
              >
                <HStack p={2} gap="2">
                  <NumberInput.DecrementTrigger asChild>
                    <IconButton
                      color={"secondary.700"}
                      size="sm"
                      border="1px solid"
                    >
                      <HiMinus />
                    </IconButton>
                  </NumberInput.DecrementTrigger>
                  <NumberInput.Input
                    width={"25px"}
                    textAlign={"center"}
                    fontSize={"lg"}
                    borderColor={"secondary.500"}
                    bg={"neutral.100"}
                    {...register("servings")}
                  />
                  <NumberInput.IncrementTrigger asChild>
                    <IconButton
                      color={"secondary.700"}
                      size="sm"
                      border="1px solid"
                    >
                      <HiPlus />
                    </IconButton>
                  </NumberInput.IncrementTrigger>
                </HStack>
              </NumberInput.Root>
            </Field.Root>
            <Field.Root>
              <Field.Label color={"brand.700"} fontSize={"lg"}>
                <HiOutlineFire />
                Calories:
              </Field.Label>
              <NumberInput.Root
                defaultValue="100"
                color={"secondary.500"}
                variant={"flushed"}
              >
                <NumberInput.Input
                  width={"65px"}
                  textAlign={"center"}
                  fontSize={"lg"}
                  borderColor={"secondary.500"}
                  {...register("calories")}
                />
              </NumberInput.Root>
            </Field.Root>

            <Controller
              name="difficulty"
              control={control}
              defaultValue={"Easy"}
              render={({ field }) => (
                <Field.Root>
                  <Field.Label color={"brand.700"} fontSize={"lg"}>
                    <HiOutlineChartBar />
                    Difficulty
                  </Field.Label>
                  <SegmentGroup.Root
                    value={field.value}
                    onValueChange={({ value }) => field.onChange(value)}
                    bgColor={"neutral.100"}
                    defaultValue="Easy"
                    mt={3}
                  >
                    <SegmentGroup.Indicator rounded="5" />
                    <SegmentGroup.Items
                      bgColor={"secondary.500"}
                      color={"neutral.100"}
                      _checked={{ bg: "secondary.700" }}
                      items={["Easy", "Medium", "Hard"]}
                      rounded={5}
                    />
                  </SegmentGroup.Root>
                </Field.Root>
              )}
            />
          </Stack>
        </Fieldset.Content>
      </Fieldset.Root>
    </Box>
  );
}
