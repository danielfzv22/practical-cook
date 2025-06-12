import { useState } from "react";
import { Box, Editable, Separator, Text, Textarea } from "@chakra-ui/react";
import { useFormContext } from "react-hook-form";

export default function RecipeTitleSection() {
  const [title, setTitle] = useState("");
  const widthRes = { base: "85vw", md: "65vw", lg: "40vw" };
  const [description, setDescription] = useState("");

  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <>
      <Box
        fontFamily={"heading"}
        position="sticky"
        top="0"
        zIndex="sticky"
        bg="secondary.500"
        py={2}
        px={4}
        boxShadow="sm"
      >
        {errors.ingredients?.[0]?.name && (
          <Text>{errors.ingredients[0].name.message}</Text>
        )}
        <Editable.Root
          value={title}
          onValueChange={(e) => setTitle(e.value)}
          placeholder={"Let's give your recipe a Title!"}
          color={"neutral.100"}
          fontSize={"7xl"}
          {...register("title", { required: "Required" })}
        >
          <Editable.Preview
            maxW={widthRes}
            px={2}
            py={1}
            lineHeight="1"
            whiteSpace="pre-wrap"
            wordBreak="break-word"
            lineClamp={"3"}
            textAlign="center"
            _hover={{
              bg: "secondary.100",
              color: "secondary.500",
              cursor: "pointer",
            }}
          />
          <Editable.Input
            px={2}
            py={1}
            colorPalette={"orange"}
            textAlign="center"
            bg={"secondary.100"}
            color={"secondary.500"}
          />
        </Editable.Root>
      </Box>

      <Separator
        m={"5"}
        variant="dashed"
        borderColor={"secondary.500"}
        w={widthRes}
      />
      <Textarea
        w={widthRes}
        colorPalette={"green"}
        color={"brand.900"}
        fontSize={"lg"}
        variant="flushed"
        placeholder="Description"
        autoresize
        maxH="10lh"
        lineHeight="1.2"
        textAlign={"justify"}
      />
    </>
  );
}
