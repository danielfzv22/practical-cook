import RecipeTitleSection from "./RecipeTitleSection";
import { Flex, VStack } from "@chakra-ui/react";
import { useForm, FormProvider } from "react-hook-form";
import RecipeControls from "./RecipeControls";
import RecipeMakerInformation from "./RecipeMakerInformation/RecipeMakerInformation";

export default function RecipeMaker() {
  const methods = useForm({
    defaultValues: {
      ingredients: [{ name: "", quantity: "", measure: "" }],
    },
  });

  const onSubmit = (data) => {
    console.log("Recipe submitted", data);
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
            <RecipeTitleSection />
            <RecipeMakerInformation />
            <RecipeControls />
          </VStack>
        </Flex>
      </form>
    </FormProvider>
  );
}
