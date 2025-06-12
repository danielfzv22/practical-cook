import {
  Field,
  Fieldset,
  HStack,
  Input,
  NativeSelect,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { foodTypes } from "./RecipeIngredientsSection";

const measures = ["Unit", "Cup", "Tbsp", "Tsp", "Gr", "Milliliters", "Piece"];

const iconFromType = (type) => {
  const match = foodTypes.items.find((ft) => ft.value === type);
  return match?.icon || RiKnifeLine;
};

function IngredientsSelected({ selectedIngredients }) {
  return (
    <Wrap spacing="4" mt={8}>
      {selectedIngredients.map((ingredient) => {
        const Icon = iconFromType(ingredient.type);
        return (
          <WrapItem key={ingredient.id} width="30%" m={1}>
            <Fieldset.Root size="lg">
              <Field.Root>
                <Field.Label fontSize={"lg"} color={"secondary.700"}>
                  <Icon />
                  {ingredient.name}
                </Field.Label>
                <HStack>
                  <Input
                    variant="flushed"
                    width="20px"
                    placeholder="Qty"
                    color={"secondary.500"}
                    fontSize={"sm"}
                    textAlign={"center"}
                    borderColor={"secondary.500"}
                  />

                  <NativeSelect.Root size={"md"} variant={"subtle"}>
                    <NativeSelect.Field
                      bg={"neutral.100"}
                      color={"secondary.500"}
                      _hover={{ bg: "secondary.500", color: "white" }}
                    >
                      {measures.map((item) => (
                        <option key={item} value={item}>
                          {item}
                        </option>
                      ))}
                    </NativeSelect.Field>
                    <NativeSelect.Indicator color={"secondary.500"} />
                  </NativeSelect.Root>
                </HStack>
              </Field.Root>
            </Fieldset.Root>
          </WrapItem>
        );
      })}
    </Wrap>
  );
}

export default IngredientsSelected;
