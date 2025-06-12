import { useState } from "react";
import {
  Combobox,
  Portal,
  Span,
  VStack,
  useFilter,
  useListCollection,
} from "@chakra-ui/react";
import { MdPublic, MdVpnLock } from "react-icons/md";
import AddUtensilPopover from "./AddUtensilPopover";
import UtensilsSelected from "./UtensilsSelected";

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

export default function RecipeUtensilsSection() {
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
        <UtensilsSelected selectedUtensils={selectedUtensils} />
      </Combobox.Root>
    </VStack>
  );
}
