import { useState } from "react";
import {
  Button,
  HStack,
  Popover,
  Portal,
  Select,
  Stack,
  Text,
  useComboboxContext,
} from "@chakra-ui/react";
import { foodTypes } from "./RecipeIngredientsSection";

function AddIngredientPopover({ ingredientValue, onAdd }) {
  const combobox = useComboboxContext();
  const [name, setName] = useState(ingredientValue);
  const [type, setType] = useState(["Other"]);

  const handleSubmit = () => {
    if (name.trim()) {
      const newIngredient = onAdd(name, type[0]);
      combobox.setValue([
        ...combobox.selectedItems.map((i) => i.value),
        newIngredient.value,
      ]);
      combobox.setInputValue("");
    }
  };

  return (
    <Popover.Root on>
      <Popover.Trigger asChild>
        <Button
          bg={"brand.700"}
          color={"neutral.100"}
          onClick={() => setName(ingredientValue)}
        >
          Add new ingredient: <strong>&nbsp;"{ingredientValue}"</strong>
        </Button>
      </Popover.Trigger>
      <Portal>
        <Popover.Positioner>
          <Popover.Content bg={"neutral.100"}>
            <Popover.Header color={"secondary.700"} fontSize={"lg"}>
              New Ingredient. (private)
            </Popover.Header>
            <Popover.Body>
              <Stack>
                <Text
                  fontWeight={"bold"}
                  fontSize={"xl"}
                  color={"secondary.500"}
                >
                  {name}
                </Text>
                <Select.Root
                  collection={foodTypes}
                  size="sm"
                  variant="subtle"
                  closeOnSelect
                  value={type}
                  onValueChange={(e) => setType(e.value)}
                >
                  <Select.HiddenSelect />
                  <Select.Label color={"secondary.700"}>
                    Select food type
                  </Select.Label>
                  <Select.Control>
                    <Select.Trigger bg={"neutral.100"} color={"neutral.900"}>
                      <Select.ValueText />
                    </Select.Trigger>
                    <Select.IndicatorGroup>
                      <Select.Indicator />
                    </Select.IndicatorGroup>
                  </Select.Control>
                  <Select.Positioner>
                    <Select.Content bg={"neutral.100"} color={"neutral.900"}>
                      {foodTypes.items.map((foodType) => {
                        const Icon = foodType.icon;
                        return (
                          <Select.Item
                            item={foodType}
                            key={foodType.value}
                            _hover={{ bg: "brand.500", color: "white" }}
                            _checked={{ bg: "brand.700", color: "white" }}
                            fontSize="md"
                          >
                            <HStack>
                              <Icon />
                              {foodType.label}
                            </HStack>
                            <Select.ItemIndicator />
                          </Select.Item>
                        );
                      })}
                    </Select.Content>
                  </Select.Positioner>
                </Select.Root>
              </Stack>
            </Popover.Body>
            <Popover.CloseTrigger asChild>
              <Button
                bg={"secondary.700"}
                color={"neutral.100"}
                onClick={handleSubmit}
                fontSize={"md"}
              >
                Add
              </Button>
            </Popover.CloseTrigger>
          </Popover.Content>
        </Popover.Positioner>
      </Portal>
    </Popover.Root>
  );
}

export default AddIngredientPopover;
