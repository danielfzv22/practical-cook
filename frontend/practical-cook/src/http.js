import { s } from "motion/react-client";

export async function fetchAvailablePlaces() {
  const response = await fetch("http://localhost:3000/places");
  const resData = await response.json();
  if (!response.ok) {
    throw new Error("Failed to fetch places");
  }

  return resData.places;
}

export async function updateUserPlaces(places) {
  const response = await fetch("http://localhost:3000/user-places", {
    method: "PUT",
    body: JSON.stringify({ places: places }),
    headers: {
      "Content-Type": "application/json",
    },
  });

  const resData = await response.json();

  if (!response.ok) {
    throw new Error("Failed to fetch places");
  }

  return resData.message;
}

export async function updateRecipeGeneral(recipe) {
  console.log(JSON.stringify(recipe));
  const response = await fetch("http://localhost:5086/recipes", {
    method: "PUT",
    body: JSON.stringify(recipe),
    headers: {
      "Content-Type": "application/json",
    },
  });

  const resData = await response.json();

  if (!response.ok) {
    throw new Error("Failed to fetch places");
  }

  return resData.message;
}

export async function createRecipe(recipe) {
  const response = await fetch("http://localhost:5086/recipes", {
    method: "POST",
    body: JSON.stringify(recipe),
    headers: {
      "Content-Type": "application/json",
    },
  });
}

export async function fetchRecipes() {
  const response = await fetch("http://localhost:5086/recipes");
  if (!response.ok) {
    throw new Error("Failed to fetch recipes");
  }

  const resData = await response.json();
  return resData.data;
}

export async function LoadRecipes() {
  const response = await fetch("http://localhost:5086/recipes");
  if (!response.ok) {
    throw new Response(
      JSON.stringify({ isError: true, message: "Failed to fetch recipes" }),
      { status: 500 }
    );
  }
  return response;
}

export async function fetchRecipeById(id) {
  const response = await fetch(`http://localhost:5086/recipes/${id}`);
  const resData = await response.json();
  if (!response.ok) {
    throw new Error("Failed to fetch recipe " + id);
  }

  return resData.data;
}
