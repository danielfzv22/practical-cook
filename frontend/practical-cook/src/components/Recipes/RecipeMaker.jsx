import RecipeTitleSection from "./RecipeMaker/RecipeTitleSection";
import { Flex, VStack } from "@chakra-ui/react";
import { useForm, FormProvider } from "react-hook-form";
import RecipeControls from "./RecipeMaker/RecipeControls";
import RecipeMakerInformation from "./RecipeMaker/RecipeMakerInformation/RecipeMakerInformation";

const stepsDefault = [
  { stepOrder: 0, title: "Before Starting", description: "" },
  { stepOrder: 1, title: "", description: "" },
  { stepOrder: 2, title: "", description: "" },
  { stepOrder: 3, title: "", description: "" },
];

const difficultyValue = ["Easy", "Medium", "Hard"];
export default function RecipeMaker({ recipe }) {
  const defaultRecipe = !recipe
    ? {
        steps: stepsDefault,
        recipeUtensils: [],
        recipeIngredients: [],
      }
    : { ...recipe, difficulty: difficultyValue[recipe.difficulty] };

  const methods = useForm({
    defaultValues: { ...defaultRecipe },
  });

  const onSubmit = (data) => {
    const difficultyEnum = {
      Easy: 1,
      EasyMedium: 2,
      Medium: 3,
      MediumHard: 4,
      Hard: 5,
    };
    const numericDifficulty = difficultyEnum[data.difficulty];
    const cleanData = {
      ...data,
      difficulty: numericDifficulty,
      recipeUtensils: data.recipeUtensils.map((u) => ({
        utensilId: u.utensilId,
      })),
      recipeIngredients: data.recipeIngredients.map((i) => ({
        ingredientId: i.ingredientId,
        measure: i.measure,
        quantity: i.quantity,
      })),
    };
    // Enviar cleanData al backend
    console.log("Recipe submitted", cleanData);
  };

  return (
    <FormProvider {...methods}>
      <form onSubmit={methods.handleSubmit(onSubmit)}>
        <Flex align="center" justify="center" alignItems={"Center"}>
          <VStack
            w={{ base: "90vw", md: "70vw", lg: "50vw" }}
            data-state="open"
            m={4}
            p={4}
            borderRadius="md"
            boxShadow="md"
            bg="rgba(255, 255, 255, 0.75)"
            _open={{
              animation: "fade-in 300ms ease-out",
            }}
          >
            <RecipeTitleSection recipeTitle={recipe.name} />
            <RecipeMakerInformation />
            <RecipeControls />
          </VStack>
        </Flex>
      </form>
    </FormProvider>
  );
}
