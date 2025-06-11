import { useContext, useState } from "react";
import RecipeContext from "../../../context/RecipeContext";
import {
  Badge,
  Button,
  Combobox,
  Icon,
  Input,
  Popover,
  Portal,
  Span,
  VStack,
  Wrap,
  useComboboxContext,
  useFilter,
  useListCollection,
} from "@chakra-ui/react";
import { MdPublic, MdVpnLock } from "react-icons/md";

const UTENSILS = [
  { id: 1, isGlobal: true, name: "Parchment paper" },
  { id: 2, isGlobal: true, name: "Large Bowl" },
  { id: 3, isGlobal: true, name: "Small fry pan" },
  { id: 4, isGlobal: true, name: "Strainer" },
  { id: 5, isGlobal: true, name: "Whisk" },
  { id: 6, isGlobal: true, name: "Medium Pot" },
  { id: 7, isGlobal: false, name: "Parchment paper2" },
  { id: 8, isGlobal: false, name: "Large Bowl3" },
  { id: 9, isGlobal: true, name: "Small fry pan4" },
  { id: 10, isGlobal: true, name: "Strainer5" },
  { id: 11, isGlobal: true, name: "Whisk6" },
  { id: 12, isGlobal: true, name: "Medium Pot7" },
];

function AddUtensilPopover({ utensilValue, onAdd }) {
  const combobox = useComboboxContext();
  const [name, setName] = useState(utensilValue);

  const handleSubmit = () => {
    if (name.trim()) {
      const newUtensil = onAdd(name);
      combobox.setValue([
        ...combobox.selectedItems.map((i) => i.value),
        newUtensil,
      ]);
      combobox.setInputValue("");
    }
  };

  return (
    <Popover.Root bg="neutral.100">
      <Popover.Trigger asChild>
        <Button onClick={() => setName(utensilValue)}>
          Add new utensil: <strong>&nbsp;{utensilValue}</strong>
        </Button>
      </Popover.Trigger>
      <Portal>
        <Popover.Positioner>
          <Popover.Content bg={"neutral.100"}>
            <Popover.Header color={"secondary.700"} fontSize={"lg"}>
              New Utensil. (private)
            </Popover.Header>
            <Popover.Body>
              <Input
                fontWeight={"bold"}
                fontSize={"xl"}
                color={"secondary.500"}
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Utensil name"
              />
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

export default function RecipeUtensilsSection() {
  const ctxRecipe = useContext(RecipeContext);

  const [utensilValue, setUtensilValue] = useState();
  const [selectedUtensils, setSelectedUtensils] = useState([]);

  const { contains } = useFilter({ sensitivity: "accent" });
  const items = UTENSILS.map((utensil) => ({
    label: utensil.name,
    value: String(utensil.id), // debe ser único y tipo string
    ...utensil,
  }));

  const [utensils, setUtensils] = useState(items);

  const { collection, filter } = useListCollection({
    initialItems: utensils,
    filter: contains,
  });
  const handleInputChange = (e) => {
    const value = e.inputValue;
    setUtensilValue(value);
    filter(value);
  };

  const handleValueChange = (e) => {
    setSelectedUtensils(e.items);
  };

  return (
    <VStack>
      <Combobox.Root
        color={"brand.900"}
        size={"lg"}
        variant="flushed"
        multiple
        collection={collection}
        onInputValueChange={handleInputChange}
        onValueChange={handleValueChange}
        openOnClick
      >
        <Combobox.Label color={"neutral.900"}>
          Select the utensils you will need
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
                <AddUtensilPopover
                  utensilValue={utensilValue}
                  onAdd={(newUtensilName) => {
                    const lastId =
                      Math.max(...utensils.map((item) => item.id)) + 1;
                    const newUtensil = {
                      id: lastId,
                      isGlobal: false,
                      name: newUtensilName,
                      label: newUtensilName,
                      value: String(lastId),
                    };
                    setUtensils((prev) => [...prev, newUtensil]);
                    return newUtensil.value;
                  }}
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
        <Wrap mt={8} gap="2">
          {selectedUtensils.map((utensil) => (
            <Badge
              color={"secondary.500"}
              variant={"solid"}
              size={"lg"}
              key={utensil.id}
            >
              {utensil.name}
            </Badge>
          ))}
        </Wrap>
      </Combobox.Root>
    </VStack>
  );
}
