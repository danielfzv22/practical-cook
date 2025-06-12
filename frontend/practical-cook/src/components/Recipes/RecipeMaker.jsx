import RecipeTitleSection from "./RecipeMakerSections/RecipeTitleSection";
import RecipeGeneralSection from "./RecipeMakerSections/RecipeGeneralInfoSection";
import RecipeIngredientsSection from "./RecipeMakerSections/RecipeIngredientsSection";
import RecipeInstructionsSection from "./RecipeMakerSections/RecipeInstructionsSection";
import RecipeUtensilsSection from "./RecipeMakerSections/RecipeUtensilsSection";
import RecipeNotesSection from "./RecipeMakerSections/RecipeNotesSection";
import { useContext } from "react";
import RecipeContext from "../../context/RecipeContext";
import {
  Box,
  Button,
  ButtonGroup,
  Flex,
  Heading,
  Separator,
  Steps,
  Tabs,
  VStack,
} from "@chakra-ui/react";
import RecipeNotesModal from "./old/RecipeNotesModal";
import { HiClipboardList } from "react-icons/hi";
import { GiWhisk } from "react-icons/gi";
import { TbSalt } from "react-icons/tb";

export default function RecipeWizard() {
  const ctxRecipe = useContext(RecipeContext);

  function handleCreateRecipe() {
    ctxRecipe.createRecipe();
  }

  function handleCancel() {
    ctxRecipe.cancelRecipe();
  }

  const steps = [
    {
      title: "Ingredients",
      content: <RecipeIngredientsSection />,
    },
    {
      title: "Utensils",
      content: <RecipeUtensilsSection />,
    },
    {
      title: "Instructions",
      content: <RecipeInstructionsSection />,
    },
    {
      title: "Special Notes",
      content: <RecipeNotesSection />,
    },
  ];
  return (
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
        <Box
          mt={5}
          w={{ base: "85vw", md: "65vw", lg: "40vw" }}
          bg={"neutral.100"}
          p={5}
        >
          <RecipeGeneralSection />
          <Separator mt={"5"} variant="dashed" borderColor={"secondary.500"} />
          <Tabs.Root defaultValue="Instruccions" p={5} variant="plain" fitted>
            <Tabs.List
              bg="bg.muted"
              rounded="l3"
              p="1"
              bgColor={"secondary.500"}
            >
              <Tabs.Trigger
                value="Instruccions"
                color={"neutral.100"}
                fontFamily={"heading"}
                fontSize={"2xl"}
              >
                <HiClipboardList />
                Instruccions
              </Tabs.Trigger>
              <Tabs.Trigger
                value="Ingredients"
                color={"neutral.100"}
                fontFamily={"heading"}
                fontSize={"2xl"}
              >
                <TbSalt />
                Ingredients
              </Tabs.Trigger>
              <Tabs.Trigger
                value="Utensils"
                color={"neutral.100"}
                fontFamily={"heading"}
                fontSize={"2xl"}
              >
                <GiWhisk />
                Utensils
              </Tabs.Trigger>

              <Tabs.Indicator bgColor={"brand.700"} rounded="l2" />
            </Tabs.List>
            <Tabs.Content value="Instruccions" minH={"20vw"}>
              <RecipeInstructionsSection />
            </Tabs.Content>
            <Tabs.Content value="Ingredients" minH={"20vw"}>
              <RecipeIngredientsSection />
            </Tabs.Content>
            <Tabs.Content value="Utensils" minH={"20vw"}>
              <RecipeUtensilsSection />
            </Tabs.Content>
          </Tabs.Root>
          <Separator mt={"5"} variant="dashed" borderColor={"secondary.500"} />
          <RecipeNotesSection />
        </Box>
      </VStack>
    </Flex>
  );
}
