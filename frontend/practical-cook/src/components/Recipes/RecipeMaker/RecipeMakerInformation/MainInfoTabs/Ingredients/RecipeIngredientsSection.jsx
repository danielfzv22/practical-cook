import { useEffect, useState } from "react";
import {
  Combobox,
  createListCollection,
  Field,
  Fieldset,
  HStack,
  Input,
  NativeSelect,
  Portal,
  Span,
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
import { useFieldArray, useFormContext } from "react-hook-form";
import AddIngredientPopover from "./AddIngredientPopover";
import { useLoaderData, useRouteLoaderData } from "react-router-dom";

// const INGREDIENTS_DATA = [
//   { isGlobal: true, id: 1, name: "Chicken breast", type: "Protein" },
//   { isGlobal: true, id: 2, name: "Olive oil", type: "Fat" },
//   { isGlobal: true, id: 3, name: "Garlic", type: "Vegetable" },
//   { isGlobal: true, id: 4, name: "Onion", type: "Vegetable" },
//   { isGlobal: true, id: 5, name: "Tomato", type: "Fruit" },
//   { isGlobal: true, id: 6, name: "Ground beef", type: "Protein" },
//   { isGlobal: true, id: 7, name: "Salt", type: "Condiment" },
//   { isGlobal: true, id: 8, name: "Black pepper", type: "Condiment" },
//   { isGlobal: true, id: 9, name: "Paprika", type: "Condiment" },
//   { isGlobal: true, id: 10, name: "Eggs", type: "Protein" },
//   { isGlobal: false, id: 11, name: "Milk", type: "Dairy" },
//   { isGlobal: true, id: 12, name: "Parmesan cheese", type: "Dairy" },
//   { isGlobal: true, id: 13, name: "Basil", type: "Herbs" },
//   { isGlobal: true, id: 14, name: "Butter", type: "Dairy" },
//   { isGlobal: false, id: 15, name: "Rice", type: "Grain" },
//   { isGlobal: false, id: 16, name: "Lemon juice", type: "Fruit" },
//   { isGlobal: false, id: 17, name: "Soy sauce", type: "Condiment" },
//   { isGlobal: true, id: 18, name: "Spinach", type: "Vegetable" },
//   { isGlobal: true, id: 19, name: "Carrots", type: "Vegetable" },
//   { isGlobal: true, id: 20, name: "Cumin", type: "Condiment" },
// ];

export const foodTypes = createListCollection({
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

const measures = [
  { id: 0, label: "Unit" },
  { id: 1, label: "Cup" },
  { id: 2, label: "Tbsp" },
  { id: 3, label: "Tsp" },
  { id: 4, label: "Gr" },
  { id: 5, label: "Milliliters" },
  { id: 6, label: "Piece" },
];

export default function RecipeIngredientsSection() {
  const { ingredientsData } = useRouteLoaderData("recipe-detail");

  const { control, register, watch, getValues } = useFormContext();
  const { fields, replace } = useFieldArray({
    control,
    name: "recipeIngredients",
  });

  const selectedIngredients = watch("recipeIngredients") || [];
  const defaultSelectedValues = selectedIngredients.map((ing) =>
    String(ing.ingredientId)
  );

  const [ingredientValue, setIngredientValue] = useState("");
  //const [selectedIngredients, setSelectedIngredients] = useState([]);

  const { contains } = useFilter({ sensitivity: "accent" });
  const items = ingredientsData.data.map((ingredient) => ({
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
    const currentValues = getValues("recipeIngredients");
    const selectedItems = e.items; // todos los seleccionados

    const newFields = selectedItems.map((item) => {
      const existing = currentValues.find((i) => i.ingredientId === item.id);
      return {
        ingredient: { ...item },
        ingredientId: item.id,
        measure: existing?.measure || "",
        quantity: existing?.quantity || "",
      };
    });

    replace(newFields);
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
    return match?.icon || RiKnifeLine;
  };
  return (
    <VStack>
      <Combobox.Root
        defaultValue={defaultSelectedValues}
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
          {fields.map((field, index) => {
            const Icon = iconFromType(field.ingredient.type);
            return (
              <WrapItem key={field.ingredientId} width="30%" m={1}>
                <Fieldset.Root size="lg">
                  <Field.Root>
                    <Field.Label fontSize={"lg"} color={"secondary.700"}>
                      <Icon />
                      {field.ingredient.name}
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
                        {...register(`recipeIngredients.${index}.quantity`)}
                      />

                      <NativeSelect.Root size={"md"} variant={"subtle"}>
                        <NativeSelect.Field
                          bg={"neutral.100"}
                          color={"secondary.500"}
                          _hover={{ bg: "secondary.500", color: "white" }}
                          {...register(`recipeIngredients.${index}.measure`)}
                        >
                          {measures.map((item) => (
                            <option
                              key={item.id}
                              value={item.id}
                              label={item.label}
                            >
                              {item.label}
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
      </Combobox.Root>
    </VStack>
  );
}
