import RecipeGeneralInfoSection from "./GeneralInfo/RecipeGeneralInfoSection";
import RecipeNotesSection from "./Notes/RecipeNotesSection";

import { Box, Separator } from "@chakra-ui/react";
import RecipeMainInfoTabs from "./MainInfoTabs/RecipeMainInfoTabs";

function RecipeMakerInformation() {
  return (
    <Box
      mt={5}
      w={{ base: "85vw", md: "65vw", lg: "40vw" }}
      bg={"neutral.100"}
      p={5}
    >
      <RecipeGeneralInfoSection />
      <Separator mt={"5"} variant="dashed" borderColor={"secondary.500"} />
      <RecipeMainInfoTabs />
      <Separator mt={"5"} variant="dashed" borderColor={"secondary.500"} />
      <RecipeNotesSection />
      <Separator mt={"5"} variant="dashed" borderColor={"secondary.500"} />
    </Box>
  );
}

export default RecipeMakerInformation;
