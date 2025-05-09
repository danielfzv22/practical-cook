import { useContext, useState } from "react";
import Input from "../../UI/Input";
import Modal from "../../UI/Modal";
import RecipeContext from "../../../context/RecipeContext";

export default function RecipeNotesModal() {
  const ctxRecipe = useContext(RecipeContext);
  const defaultValues =
    ctxRecipe.recipe.notes.length > 0
      ? {
          notes: ctxRecipe.recipe.notes,
          note: ctxRecipe.recipe.notes[ctxRecipe.recipe.notes.length - 1],
          index: ctxRecipe.recipe.notes.length - 1,
        }
      : {
          notes: [],
          note: "",
          index: 1,
        };

  const [infoNewNotes, setInfoNewNotes] = useState(defaultValues.notes);
  const [note, setNote] = useState(defaultValues.note);
  const [currentIndex, setCurrentIndex] = useState(defaultValues.index);

  const handleSubmit = (e) => {
    console.log(infoNewNotes);
    e.preventDefault();
    setNote(infoNewNotes[infoNewNotes.length - 1]);
    setCurrentIndex(infoNewNotes.length);
    ctxRecipe.editRecipeNotes(infoNewNotes);
  };

  const handleOnClose = () => {
    ctxRecipe.hideModal();
    setInfoNewNotes(defaultValues.notes);
  };

  const handleAddNote = () => {
    setInfoNewNotes((prevState) => [...prevState, ""]);
    setCurrentIndex((prevState) => prevState + 1);
    setNote("");
  };

  const handlePreviousNote = () => {
    setCurrentIndex((prevState) => prevState - 1);
    setNote(infoNewNotes[currentIndex - 2]);
  };

  const handleNextNote = () => {
    setCurrentIndex((prevState) => prevState + 1);
    setNote(infoNewNotes[currentIndex]);
  };

  const handleRemoveNote = () => {
    setInfoNewNotes((prevState) => {
      const notes = [...prevState];
      notes.splice(infoNewNotes.length - 1, 1);
      return notes;
    });
    setNote(infoNewNotes[infoNewNotes.length - 2]);
    setCurrentIndex((prevState) => prevState - 1);
  };

  const handleOnChange = (e) => {
    const { name, value } = e.target;
    setNote(value);
    setInfoNewNotes((prevState) => {
      const currStep = prevState;
      currStep[currentIndex - 1] = value;
      return currStep;
    });
  };

  const isEmpty = note.trim() === "";
  const enableAdd = !isEmpty && currentIndex === infoNewNotes.length;
  const enableRemove = currentIndex > 1 && currentIndex === infoNewNotes.length;
  const enableNext = !isEmpty && currentIndex < infoNewNotes.length;
  const enablePrevious = !isEmpty && currentIndex > 1;

  return (
    <Modal open={ctxRecipe.selectedForm === "notes"} onClose={handleOnClose}>
      <form onSubmit={handleSubmit}>
        <h3>Notes...</h3>
        <div className="instructions-content">
          <p className="control">
            <label>{currentIndex}.</label>
            <textarea
              id="step-description"
              name="description"
              type="text"
              value={note}
              onChange={handleOnChange}
              required
            ></textarea>
          </p>
        </div>
        <div className="instructions-controls">
          <button
            className={`step-control${!enableRemove ? " disabled" : ""}`}
            type="button"
            onClick={handleRemoveNote}
            disabled={!enableRemove}
          >
            -
          </button>
          <button
            className={`step-control${!enablePrevious ? " disabled" : ""}`}
            type="button"
            onClick={handlePreviousNote}
            disabled={!enablePrevious}
          >
            {"<-"}
          </button>
          <button
            className={`step-control${!enableNext ? " disabled" : ""}`}
            type="button"
            onClick={handleNextNote}
            disabled={!enableNext}
          >
            {"->"}
          </button>
          <button
            className={`step-control${!enableAdd ? " disabled" : ""}`}
            type="button"
            onClick={handleAddNote}
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
