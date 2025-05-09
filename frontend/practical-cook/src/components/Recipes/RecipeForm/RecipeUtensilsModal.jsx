import { useContext, useRef, useState } from "react";
import Modal from "../../UI/Modal";
import RecipeContext from "../../../context/RecipeContext";
import { Autocomplete, TextField, createFilterOptions } from "@mui/material";

const UTENSILS_DATA = [
  { id: "0", name: "Parchment paper" },
  { id: "1", name: "Large Bowl" },
  { id: "2", name: "Small fry pan" },
  { id: "3", name: "Strainer" },
  { id: "4", name: "Whisk" },
  { id: "5", name: "Medium Pot" },
  { id: "6", name: "Parchment paper2" },
  { id: "7", name: "Large Bowl3" },
  { id: "8", name: "Small fry pan4" },
  { id: "9", name: "Strainer5" },
  { id: "10", name: "Whisk6" },
  { id: "11", name: "Medium Pot7" },
];
const filter = createFilterOptions();

export default function RecipeUtensilsModal() {
  const ctxRecipe = useContext(RecipeContext);

  const defaultValue = ctxRecipe.recipe.utensils;
  const newUtensilRef = useRef();

  const [infoNewRecipeUtensils, setInfoNewRecipeUtensils] =
    useState(defaultValue);
  const [utensil, setUtensil] = useState({ id: "", name: "" });

  const handleChange = (e) => {
    setUtensil(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    ctxRecipe.editRecipeUtensils(infoNewRecipeUtensils);
  };

  const handleOnClose = () => {
    ctxRecipe.hideModal();
    setInfoNewRecipeUtensils(defaultValue);
  };
  const handleClick = () => {};

  const listUtensils = (
    <>
      {infoNewRecipeUtensils.map((u) => (
        <li key={u.id}>
          <button className="utensil-control" type="button">
            -
          </button>
          {u.name}
        </li>
      ))}
    </>
  );

  const handleChangeAutocomplete = (event, newUtensil) => {
    if (typeof newUtensil === "string") {
      setUtensil({
        id: "",
        name: newUtensil,
      });
    } else if (newUtensil?.inputValue) {
      const newId = UTENSILS_DATA[UTENSILS_DATA.length - 1].id + 1;
      UTENSILS_DATA.push({ id: newId, name: newUtensil.inputValue });
      setUtensil({
        name: newUtensil.inputValue,
      });
    } else {
      setUtensil(newUtensil);
    }
  };

  return (
    <Modal open={ctxRecipe.selectedForm === "utensils"} onClose={handleOnClose}>
      <h3>Utensils...</h3>
      <form onSubmit={handleSubmit}>
        <div className="control">
          <Autocomplete
            value={utensil}
            disablePortal
            onChange={(event, newUtensil) =>
              handleChangeAutocomplete(event, newUtensil)
            }
            filterOptions={(options, params) => {
              const filtered = filter(options, params);

              const { inputValue } = params;
              // Suggest the creation of a new value
              const isExisting = options.some(
                (option) => inputValue === option.name
              );
              if (inputValue !== "" && !isExisting) {
                filtered.push({
                  inputValue,
                  name: `Add New: "${inputValue}"`,
                });
              }

              return filtered;
            }}
            selectOnFocus
            clearOnBlur
            handleHomeEndKeys
            id="free-solo-with-text-demo"
            options={UTENSILS_DATA}
            getOptionLabel={(option) => {
              // Value selected with enter, right from the input
              if (typeof option === "string") {
                return option;
              }
              // Add "xxx" option created dynamically
              if (option.inputValue) {
                return option.inputValue;
              }
              // Regular option
              return option.name;
            }}
            renderOption={(props, option) => <li {...props}>{option.name}</li>}
            sx={{
              width: 400,
              "& + .MuiAutocomplete-popper .MuiAutocomplete-option": {
                fontFamily: "'Kalam', cursive",
                color: "#6f48b9"
              }
            }}
            freeSolo
            renderInput={(params) => (
              <TextField {...params} label="Add Utensil" />
            )}
          />
          <button className="new-utensil-control" type="button">
            +
          </button>
        </div>

        <ul className="utensil-list">
          {infoNewRecipeUtensils.length > 0 && listUtensils}
        </ul>
        <div className="actions">
          <button className="ok">Ok</button>
          <button className="cancel" onClick={handleOnClose} type="button">
            Cancel
          </button>
        </div>
      </form>
      <dialog ref={newUtensilRef}>Mostrar</dialog>
    </Modal>
  );
}
