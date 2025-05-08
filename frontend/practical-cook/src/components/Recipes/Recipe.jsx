import "./Recipe.css";
import RecipeTitleSection from "./RecipeSections/RecipeTitleSection";
import RecipeGeneralSection from "./RecipeSections/RecipeGeneralSection";
import RecipeUtensilsSection from "./RecipeSections/RecipeUtensilsSection";
import RecipeIngredientsSection from "./RecipeSections/RecipeIngredientsSection";
import RecipeInstructionsSection from "./RecipeSections/RecipeInstructionsSection";
import RecipeNotesSection from "./RecipeSections/RecipeNotesSection";
import { useContext } from "react";
import RecipeContext from "../../context/RecipeContext";
import { Button, ButtonGroup, Steps } from "@chakra-ui/react";

export default function Recipe() {
  const ctxRecipe = useContext(RecipeContext);

  function handleCreateRecipe() {
    ctxRecipe.createRecipe();
  }

  function handleCancel() {
    ctxRecipe.cancelRecipe();
  }

  const steps = [
    {
      title: "Recipe Title",
      content: <RecipeTitleSection />,
    },
    {
      title: "General Information",
      content: <RecipeGeneralSection />,
    },
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
    <>
      <Steps.Root
        defaultStep={1}
        count={steps.length}
        bg={"rgba(255, 255, 255, 0.9)"}
        fontFamily={("Rancho", "cursive")}
        fontSize={"2xl"}
      >
        <Steps.List>
          {steps.map((step, index) => (
            <Steps.Item key={index} index={index} title={step.title}>
              <Steps.Indicator />
              <Steps.Title>{step.title}</Steps.Title>
              <Steps.Separator />
            </Steps.Item>
          ))}
        </Steps.List>

        {steps.map((step, index) => (
          <Steps.Content key={index} index={index}>
            {step.content}
          </Steps.Content>
        ))}

        <Steps.CompletedContent>All steps are complete!</Steps.CompletedContent>

        <ButtonGroup size="sm" variant="outline">
          <Steps.PrevTrigger asChild>
            <Button>Prev</Button>
          </Steps.PrevTrigger>
          <Steps.NextTrigger asChild>
            <Button>Next</Button>
          </Steps.NextTrigger>
        </ButtonGroup>
      </Steps.Root>
      {/*<div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          height: "95vh",
          width: "60%",
        }}
      >
         <div className="recipe">
          <button id="cancel-button" onClick={handleCancel}>
            Cancel
          </button>
          <div className="recipe-header">
            <RecipeTitleSection />
          </div>
          <div id="recipe-content">
            <section className="left-column">
              <RecipeGeneralSection />
              <RecipeUtensilsSection />
            </section>
            <section className="right-column">
              <RecipeIngredientsSection />
              <RecipeInstructionsSection />
            </section>
          </div>
          <div className="recipe-footer">
            <RecipeNotesSection />
          </div>
        </div>
      </div> */}
    </>
  );
}
