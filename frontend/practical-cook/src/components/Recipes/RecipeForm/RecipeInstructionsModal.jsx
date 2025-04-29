import { useContext, useState } from "react";
import Modal from "../../UI/Modal";
import RecipeContext from "../../../context/RecipeContext";

export default function RecipeInstructionsModal() {
  const ctxRecipe = useContext(RecipeContext);
  const defaultValue =
    ctxRecipe.recipe.instructions.length > 0
      ? ctxRecipe.recipe.instructions
      : [{ order: 1, title: "", description: "" }];

  const defaultValueStep =
    ctxRecipe.recipe.instructions.length > 0
      ? ctxRecipe.recipe.instructions[ctxRecipe.recipe.instructions.length - 1]
      : {
          order: 1,
          title: "",
          description: "",
        };

  const [infoNewRecipeInstr, setInfoNewRecipeInstr] = useState(defaultValue);
  const [currentStep, setCurrentStep] = useState(defaultValueStep);

  const handleSubmit = (e) => {
    e.preventDefault();
    
    setCurrentStep(infoNewRecipeInstr[infoNewRecipeInstr.length - 1]);
    ctxRecipe.editRecipeInstructions(infoNewRecipeInstr);
  };

  const handleOnClose = () => {
    ctxRecipe.hideModal();
    setInfoNewRecipeInstr(defaultValue);
  };

  const handleAddStep = () => {
    setInfoNewRecipeInstr((prevState) => [
      ...prevState,
      { order: prevState.length + 1, title: "", description: "" },
    ]);
    setCurrentStep((prevState) => ({
      order: infoNewRecipeInstr.length + 1,
      title: "",
      description: "",
    }));
  };

  const handlePreviousStep = () => {
    const prevStep = infoNewRecipeInstr[currentStep.order - 2];
    setCurrentStep(prevStep);
  };

  const handleNextStep = () => {
    const nextStep = infoNewRecipeInstr[currentStep.order];
    setCurrentStep(nextStep);
  };

  const handleRemoveStep = () => {
    setInfoNewRecipeInstr((prevState) => {
      const instructions = [...prevState];
      instructions.splice(infoNewRecipeInstr.length - 1, 1);
      return instructions;
    });
    setCurrentStep(infoNewRecipeInstr[infoNewRecipeInstr.length - 2]);
  };

  const handleOnChange = (e) => {
    const { name, value } = e.target;
    setCurrentStep((prevState) => ({ ...prevState, [name]: value }));
    setInfoNewRecipeInstr((prevState) => {
      const currStep = prevState;
      currStep[currentStep.order - 1][name] = value;
      return currStep;
    });
  };

  const isEmpty =
    currentStep.title.trim() === "" || currentStep.description.trim() === "";
  const enableAdd = !isEmpty && currentStep.order === infoNewRecipeInstr.length;
  const enableRemove =
    currentStep.order > 1 && currentStep.order === infoNewRecipeInstr.length;
  const enableNext = !isEmpty && currentStep.order < infoNewRecipeInstr.length;
  const enablePrevious = !isEmpty && currentStep.order > 1;

  return (
    <Modal
      open={ctxRecipe.selectedForm === "instructions"}
      onClose={handleOnClose}
    >
      <form onSubmit={handleSubmit}>
        <h3>Instructions...</h3>
        <div className="instructions-content">
          <p className="control">
            <label>{currentStep.order}.</label>
            <input
              id="step-title"
              name="title"
              type="text"
              value={currentStep.title}
              onChange={handleOnChange}
              required
            />
          </p>
          <p className="control">
            <textarea
              id="step-description"
              name="description"
              type="text"
              value={currentStep.description}
              onChange={handleOnChange}
              required
            ></textarea>
          </p>
        </div>
        <div className="instructions-controls">
          <button
            className={`step-control${!enableRemove ? " disabled" : ""}`}
            type="button"
            onClick={handleRemoveStep}
            disabled={!enableRemove}
          >
            -
          </button>
          <button
            className={`step-control${!enablePrevious ? " disabled" : ""}`}
            type="button"
            onClick={handlePreviousStep}
            disabled={!enablePrevious}
          >
            {"<-"}
          </button>
          <button
            className={`step-control${!enableNext ? " disabled" : ""}`}
            type="button"
            onClick={handleNextStep}
            disabled={!enableNext}
          >
            {"->"}
          </button>
          <button
            className={`step-control${!enableAdd ? " disabled" : ""}`}
            type="button"
            onClick={handleAddStep}
            disabled={!enableAdd}
          >
            +
          </button>
        </div>
        <div className="actions">
          <button className="ok">Ok</button>
          <button className="cancel" onClick={handleOnClose} type="button">
            Cancel
          </button>
        </div>
      </form>
    </Modal>
  );
}
