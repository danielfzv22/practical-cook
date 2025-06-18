import { RecipeList } from "../components/RecipeList/RecipeList.jsx";
import { RecipeContextProvider } from "../context/RecipeContext.jsx";
import { Link, useLoaderData } from "react-router-dom";
import { Box, Button, Flex, Heading, VStack } from "@chakra-ui/react";

function HomePage() {
  const recipe = useLoaderData();

  if (recipe.isError) {
    return (
      <div style={{ textAlign: "center", marginTop: "20px" }}>
        <h2>Error loading recipes</h2>
        <p>{recipe.message}</p>
      </div>
    );
  }
  const recipes = recipe?.data || [];

  return (
    <RecipeContextProvider>
      <Flex align="center" justify="center">
        <VStack
          w={{ base: "90vw", lg: "75vw" }}
          m={5}
          p={4}
          bg="rgba(255, 255, 255, 0.75)"
          borderRadius={"md"}
        >
          <Box textAlign={"center"}>
            <Button
              variant="subtle"
              size="2xl"
              bg={"accent.500"}
              color={"neutral.100"}
            >
              <Link to="/recipes/new-recipe">Create New Recipe</Link>
            </Button>
          </Box>
          <Box
            alignSelf={"flex-start"}
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center ",
              justifyContent: "space-between",
            }}
          >
            <Heading
              p={2}
              alignSelf={"inherit"}
              size={"5xl"}
              color={"neutral.900"}
            >
              Top recipes
            </Heading>
            <RecipeList
              title="Recipes"
              recipes={recipes}
              isLoading={false}
              loadingText="Loading..."
              fallbackText="No recipes available."
            />
          </Box>
        </VStack>
      </Flex>
    </RecipeContextProvider>
  );
}

export default HomePage;
