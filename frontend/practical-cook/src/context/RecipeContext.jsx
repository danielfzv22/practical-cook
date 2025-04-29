import { createContext, useReducer, useState } from "react";

export const RecipeContext = createContext({
  recipe: {
    id: 0,
    name: "",
    description: "",
    prepTime: 0,
    servings: 1,
    calories: 100,
    difficulty: 1,
    rating: 3,
    utensils: [],
    ingredients: [],
    instructions: [],
    notes: [],
  },
  editRecipeTitle: () => {},
  editRecipeGeneral: () => {},
  editRecipeInstructions: () => {},
  editRecipeNotes: () => {},
  editRecipeUtensils: () => {},
  editRecipeIngredients: () => {},
  editRecipe: () => {},
  createRecipe: () => {},
  cancelRecipe: () => {},
  selectedForm: "",
  editableRecipe: false,
  showError: () => {},
  showModal: () => {},
  hideModal: () => {},
  isLoading: true,
  errorUpdating: { success: true, message: "" },
});

function recipeReducer(state, action) {
  if (action.type === "recipe.title") {
    const { title: name, description } = action.value;
    return {
      ...state,
      name,
      description,
    };
  } else if (action.type === "recipe.general") {
    const { prepTime, servings, calories, difficulty, rating } = action.value;
    return {
      ...state,
      prepTime,
      servings,
      calories,
      difficulty,
      rating,
    };
  } else if (action.type === "recipe.instructions") {
    return {
      ...state,
      instructions: action.value,
    };
  } else if (action.type === "recipe.notes") {
    return {
      ...state,
      notes: action.value,
    };
  } else if (action.type === "recipe.utensils") {
    return {
      ...state,
      utensils: action.value,
    };
  } else if (action.type === "recipe.ingredients") {
    return {
      ...state,
      ingredients: action.value,
    };
  } else if (action.type === "recipe.edit") {
    return action.value;
  } else if (action.type === "recipe.create") {
    return {
      name: "",
      description: "",
      prepTime: 0,
      servings: 1,
      calories: 100,
      difficulty: 1,
      rating: 3,
      utensils: [],
      ingredients: [],
      instructions: [],
      notes: [],
    };
  }

  return state;
}

export function RecipeContextProvider({ children }) {
  const [selectedForm, setSelectedForm] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [errorUpdating, setErrorUpdating] = useState();
  const [editableRecipe, setEditableRecipe] = useState(false);
  const [recipe, dispatchRecipe] = useReducer(recipeReducer, {
    id: 0,
    name: "",
    description: "",
    prepTime: 0,
    servings: 1,
    calories: 100,
    difficulty: 1,
    rating: 3,
    utensils: [],
    ingredients: [],
    instructions: [],
    notes: [],
  });

  const editRecipeTitle = (recipeTitle) => {
    dispatchRecipe({ type: "recipe.title", value: recipeTitle });
    setSelectedForm("");
  };

  const editRecipeGeneral = (recipeGeneralInfo) => {
    dispatchRecipe({ type: "recipe.general", value: recipeGeneralInfo });
    setSelectedForm("");
  };

  const editRecipeInstructions = (recipeInstructions) => {
    dispatchRecipe({ type: "recipe.instructions", value: recipeInstructions });
    setSelectedForm("");
  };

  const editRecipeNotes = (recipeNotes) => {
    dispatchRecipe({ type: "recipe.notes", value: recipeNotes });
    setSelectedForm("");
  };

  const editRecipeUtensils = (recipeUtensils) => {
    dispatchRecipe({ type: "recipe.utensils", value: recipeUtensils });
    setSelectedForm("");
  };

  const editRecipeIngredients = (recipeIngredients) => {
    dispatchRecipe({ type: "recipe.ingredients", value: recipeIngredients });
    setSelectedForm("");
  };

  const editRecipe = (recipe) => {
    dispatchRecipe({ type: "recipe.edit", value: recipe });
    setEditableRecipe(true);
  };

  const createRecipe = () => {
    dispatchRecipe({ type: "recipe.create" });
    setEditableRecipe(true);
  };

  const cancelRecipe = () => {
    dispatchRecipe({ type: "recipe.create" });
    setEditableRecipe(false);
  };

  const showModal = (selection) => {
    setSelectedForm(selection);
  };

  const hideModal = () => {
    setSelectedForm("");
  };

  const showError = (error) => {
    setErrorUpdating(error);
  };

  const recipeContextValues = {
    recipe,
    editRecipeTitle,
    editRecipeGeneral,
    editRecipeInstructions,
    editRecipeNotes,
    editRecipeUtensils,
    editRecipeIngredients,
    editRecipe,
    createRecipe,
    cancelRecipe,
    selectedForm,
    editableRecipe,
    isLoading,
    showModal,
    hideModal,
    showError,
    errorUpdating,
  };

  return (
    <RecipeContext.Provider value={recipeContextValues}>
      {children}
    </RecipeContext.Provider>
  );
}

export default RecipeContext;
