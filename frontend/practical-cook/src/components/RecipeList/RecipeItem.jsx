import {
  Box,
  Button,
  Card,
  HStack,
  Image,
  Separator,
  Text,
} from "@chakra-ui/react";
import {
  HiOutlineChartBar,
  HiOutlineClock,
  HiOutlineFire,
} from "react-icons/hi";
import { Link } from "react-router-dom";

export default function RecipeItem({ recipe }) {
  return (
    <Card.Root
      overflow="hidden"
      border={"0"}
      maxW={"300px"}
      bg="neutral.100" // Fondo de la card
      boxShadow="md"
      _hover={{ boxShadow: "lg" }} // Hover effect
    >
      <Image
        src="https://images.unsplash.com/photo-1555041469-a586c61ea9bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80"
        alt={recipe.name}
      />
      <Box minW={"200px"} flex="1" display="flex" flexDirection="column">
        <Card.Body>
          <Card.Title
            fontSize={"3xl"}
            fontFamily={"heading"}
            color={"secondary.500"}
            mb={2}
            lineClamp={"2"}
            h={"2lh"}
            textAlign="center"
          >
            {recipe.name}
          </Card.Title>
          <Separator m={"1"} variant="dashed" borderColor={"secondary.500"} />
          <Card.Description lineClamp={"3"} mt={3}>
            {recipe.description}
          </Card.Description>
          <HStack mt={3}>
            <HiOutlineClock color="green" />
            <Text color={"neutral.900"}>{recipe.preparationTime} Minutes</Text>
            <HiOutlineFire color="green" />
            <Text color={"neutral.900"}>{recipe.calories}</Text>
          </HStack>
        </Card.Body>
        <Card.Footer gap="2">
          <Button bg={"brand.800"} color={"neutral.100"} asChild>
            <Link to={`/recipes/${recipe.id}/edit`}>Edit</Link>
          </Button>
          <Button bg={"secondary.500"} color={"neutral.100"}>
            Add to my meals
          </Button>
        </Card.Footer>
      </Box>
    </Card.Root>
  );
}
