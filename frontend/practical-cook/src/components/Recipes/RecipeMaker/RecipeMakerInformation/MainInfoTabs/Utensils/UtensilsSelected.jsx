import { Badge, Wrap } from "@chakra-ui/react";

function UtensilsSelected({ selectedUtensils }) {
  return (
    <Wrap mt={8} gap="2">
      {selectedUtensils.map((utensil) => (
        <Badge
          color={"secondary.500"}
          variant={"solid"}
          size={"lg"}
          key={utensil.utensilId}
        >
          {utensil.utensil.name}
        </Badge>
      ))}
    </Wrap>
  );
}

export default UtensilsSelected;
