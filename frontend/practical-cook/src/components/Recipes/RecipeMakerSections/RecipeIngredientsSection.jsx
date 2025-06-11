import { useContext, useState } from "react";
import RecipeContext from "../../../context/RecipeContext";
import {
  Button,
  Combobox,
  createListCollection,
  Field,
  Fieldset,
  HStack,
  Input,
  NativeSelect,
  Popover,
  Portal,
  Select,
  Span,
  Stack,
  Text,
  useComboboxContext,
  useFilter,
  useListCollection,
  VStack,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { TbAvocado, TbMeat } from "react-icons/tb";
import { MdPublic, MdVpnLock } from "react-icons/md";
import { LuApple, LuCroissant, LuMilk, LuWheat } from "react-icons/lu";
import { RiDrinks2Line, RiKnifeLine, RiLeafLine } from "react-icons/ri";
import { GiHerbsBundle, GiHoneyJar, GiKetchup } from "react-icons/gi";
import { PiAcorn, PiCookie } from "react-icons/pi";

const INGREDIENTS_DATA = [
  { isGlobal: true, id: 1, name: "Chicken breast", type: "Protein" },
  { isGlobal: true, id: 2, name: "Olive oil", type: "Fat" },
  { isGlobal: true, id: 3, name: "Garlic", type: "Vegetable" },
  { isGlobal: true, id: 4, name: "Onion", type: "Vegetable" },
  { isGlobal: true, id: 5, name: "Tomato", type: "Fruit" },
  { isGlobal: true, id: 6, name: "Ground beef", type: "Protein" },
  { isGlobal: true, id: 7, name: "Salt", type: "Condiment" },
  { isGlobal: true, id: 8, name: "Black pepper", type: "Condiment" },
  { isGlobal: true, id: 9, name: "Paprika", type: "Condiment" },
  { isGlobal: true, id: 10, name: "Eggs", type: "Protein" },
  { isGlobal: false, id: 11, name: "Milk", type: "Dairy" },
  { isGlobal: true, id: 12, name: "Parmesan cheese", type: "Dairy" },
  { isGlobal: true, id: 13, name: "Basil", type: "Herbs" },
  { isGlobal: true, id: 14, name: "Butter", type: "Dairy" },
  { isGlobal: false, id: 15, name: "Rice", type: "Grain" },
  { isGlobal: false, id: 16, name: "Lemon juice", type: "Fruit" },
  { isGlobal: false, id: 17, name: "Soy sauce", type: "Condiment" },
  { isGlobal: true, id: 18, name: "Spinach", type: "Vegetable" },
  { isGlobal: true, id: 19, name: "Carrots", type: "Vegetable" },
  { isGlobal: true, id: 20, name: "Cumin", type: "Condiment" },
];

const foodTypes = createListCollection({
  items: [
    { label: "Vegetable", value: "Vegetable", icon: RiLeafLine },
    { label: "Fruit", value: "Fruit", icon: LuApple },
    { label: "Grain", value: "Grain", icon: LuWheat },
    { label: "Protein", value: "Protein", icon: TbMeat },
    { label: "Dairy", value: "Dairy", icon: LuMilk },
    { label: "Fat", value: "Fat", icon: TbAvocado },
    { label: "Herb", value: "Herb", icon: GiHerbsBundle },
    { label: "Condiment", value: "Condiment", icon: GiKetchup },
    { label: "Sweetener", value: "Sweetener", icon: GiHoneyJar },
    { label: "Baking", value: "Baking", icon: LuCroissant },
    { label: "Beverage", value: "Beverage", icon: RiDrinks2Line },
    { label: "Nut", value: "Nut", icon: PiAcorn },
    { label: "Snack", value: "Snack", icon: PiCookie },
    { label: "Other", value: "Other", icon: RiKnifeLine },
  ],
});

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

export default function RecipeIngredientsSection() {
  const [ingredientValue, setIngredientValue] = useState({});
  const [selectedIngredients, setSelectedIngredients] = useState([]);

  const ctxRecipe = useContext(RecipeContext);
  const newRecipe = ctxRecipe.recipe;

  const hasIngredients = newRecipe.ingredients.length > 0;

  const { contains } = useFilter({ sensitivity: "accent" });
  const items = INGREDIENTS_DATA.map((ingredient) => ({
    label: ingredient.name,
    value: String(ingredient.id), // debe ser único y tipo string
    ...ingredient,
  }));

  const [ingredients, setIngredients] = useState(items);
  const { collection, filter } = useListCollection({
    initialItems: ingredients,
    filter: contains,
  });

  const handleInputChange = (e) => {
    const value = e.inputValue;
    setIngredientValue(value);
    filter(value);
  };

  const handleValueChange = (e) => {
    setSelectedIngredients(e.items);
  };

  const handleOnAddNewIngredient = (name, foodType) => {
    const lastId = Math.max(...ingredients.map((item) => item.id)) + 1;
    const newIngredient = {
      id: lastId,
      isGlobal: false,
      name: name,
      label: name,
      value: String(lastId),
      type: foodType,
    };

    setIngredients((prev) => [...prev, newIngredient]);
    return newIngredient;
  };

  const iconFromType = (type) => {
    const match = foodTypes.items.find((ft) => ft.value === type);
    return match?.icon || RiKnifeLine; // ícono por defecto si no hay match
  };

  return (
    <VStack>
      <Combobox.Root
        color={"brand.900"}
        collection={collection}
        onInputValueChange={handleInputChange}
        placeholder="If a required ingredient is missing, type its name to add it to the list."
        multiple
        size={"lg"}
        variant="flushed"
        onValueChange={handleValueChange}
        openOnClick
      >
        <Combobox.Label color={"neutral.900"}>
          Select ingredients and specify the amount (e.g., 1/2 cup, 2 tbsp). Use
          slashes for fractions like 1/4 or 3/4
        </Combobox.Label>
        <Combobox.Control>
          <Combobox.Input />
          <Combobox.IndicatorGroup>
            <Combobox.Trigger />
          </Combobox.IndicatorGroup>
        </Combobox.Control>
        <Portal>
          <Combobox.Positioner>
            <Combobox.Content bg={"neutral.100"} color={"neutral.900"}>
              <Combobox.Empty>
                <AddIngredientPopover
                  ingredientValue={ingredientValue}
                  onAdd={handleOnAddNewIngredient}
                />
              </Combobox.Empty>
              {collection.items.map((item) => (
                <Combobox.Item
                  item={item}
                  key={item.id}
                  _selected={{ bg: "brand.500", color: "white" }}
                  _checked={{ bg: "brand.700", color: "white" }}
                >
                  {item.isGlobal ? (
                    <MdPublic color="#419c41" />
                  ) : (
                    <MdVpnLock color="#bd2020" />
                  )}
                  <Span flex="1">{item.name}</Span>
                  <Combobox.ItemIndicator />
                </Combobox.Item>
              ))}
            </Combobox.Content>
          </Combobox.Positioner>
        </Portal>
        <Wrap spacing="4" mt={8}>
          {selectedIngredients.map((item) => {
            const Icon = iconFromType(item.type);
            return (
              <WrapItem key={item.id} width="30%" m={1}>
                <Fieldset.Root size="lg">
                  <Field.Root>
                    <Field.Label fontSize={"lg"} color={"secondary.700"}>
                      <Icon />
                      {item.name}
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
                          <option value="unit">Unit</option>
                          <option value="cup">Cup</option>
                          <option value="tbsp">Tbsp</option>
                          <option value="tsp">Tsp</option>
                          <option value="gr">Gr</option>
                          <option value="milliliters">Lt</option>
                          <option value="piece">Piece</option>
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
      </Combobox.Root>
    </VStack>
  );
}
