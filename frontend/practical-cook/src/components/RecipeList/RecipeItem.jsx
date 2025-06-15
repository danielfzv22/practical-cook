import { Box, Button, Card, Image, Text } from "@chakra-ui/react";
import { Link } from "react-router-dom";

export default function RecipeItem({ recipe }) {
  return (
    <Card.Root
      maxW="lg"
      minW={"lg"}
      minH={"250px"}
      overflow="hidden"
      flexDirection={"row"}
      bg="neutral.100" // Fondo de la card
      borderRadius="xl"
      boxShadow="md"
      _hover={{ boxShadow: "lg", bg: "brand.50" }} // Hover effect
    >
      <Image
        objectFit={"cover"}
        src="https://images.unsplash.com/photo-1555041469-a586c61ea9bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80"
        alt={recipe.name}
        maxW={"200px"}
      />

      <Box minW={"200px"} flex="1" display="flex" flexDirection="column">
        <Card.Body gap="2">
          <Card.Title mb="2" color={"neutral.900"}>
            {recipe.name}
          </Card.Title>
          <Card.Description asChild>
            <Text lineClamp="3">{recipe.description}</Text>
          </Card.Description>
        </Card.Body>
        <Card.Footer gap="2">
          <Button variant="solid" asChild>
            <Link to={`/recipes/${recipe.id}/edit`}>Edit</Link>
          </Button>
          <Button variant="ghost">Add to my meals</Button>
        </Card.Footer>
      </Box>
    </Card.Root>
  );
}
