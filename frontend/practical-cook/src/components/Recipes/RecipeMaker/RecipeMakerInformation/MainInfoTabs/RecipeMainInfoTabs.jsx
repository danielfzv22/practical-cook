import { Box, Tabs } from "@chakra-ui/react";
import { HiClipboardList } from "react-icons/hi";
import { GiWhisk } from "react-icons/gi";
import { TbSalt } from "react-icons/tb";
import RecipeIngredientsSection from "./Ingredients/RecipeIngredientsSection";
import RecipeInstructionsSection from "./Instrucctions/RecipeInstructionsSection";
import RecipeUtensilsSection from "./Utensils/RecipeUtensilsSection";

function RecipeMainInfoTabs() {
  return (
    <Box>
      <Tabs.Root defaultValue="Instruccions" p={5} variant="plain" fitted>
        <Tabs.List bg="bg.muted" rounded="l3" p="1" bgColor={"secondary.500"}>
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
    </Box>
  );
}

export default RecipeMainInfoTabs;
