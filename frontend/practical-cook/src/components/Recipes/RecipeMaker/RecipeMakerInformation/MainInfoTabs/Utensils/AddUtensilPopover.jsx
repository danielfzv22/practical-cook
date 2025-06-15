import { useState } from "react";
import {
  Button,
  Input,
  Popover,
  Portal,
  useComboboxContext,
} from "@chakra-ui/react";

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

export default AddUtensilPopover;
