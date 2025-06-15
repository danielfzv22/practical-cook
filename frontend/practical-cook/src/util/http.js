import { redirect } from "react-router-dom";

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

export async function createRecipe(recipe) {
  await fetch("http://localhost:5086/recipes", {
    method: "POST",
    body: JSON.stringify(recipe),
    headers: {
      "Content-Type": "application/json",
    },
  });
}

export function getAuthToken() {
  const token = localStorage.getItem("token");

  const tokenDuration = getTokenDuration();

  if (!token) {
    return null;
  }

  if (tokenDuration < 0) {
    return "EXPIRED";
  }
  return token;
}

export function getTokenDuration() {
  const storedExpirationDate = localStorage.getItem("expiration");
  const expirationDate = new Date(storedExpirationDate);
  const now = new Date();

  const duration = expirationDate.getTime() - now.getTime();
  return duration;
}

export function tokenLoader() {
  const token = getAuthToken();
  return token;
}

export function checkAuthLoader() {
  const token = getAuthToken();
  if (!token) {
    return redirect("/auth?mode=login");
  }

  return token;
}

export const authFetch = (url, options = {}) => {
  const token = getAuthToken();

  if (!token) {
    return redirect("/auth?mode=login");
  }

  return fetch(url, {
    ...options,
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
      ...(options.headers || {}),
    },
  });
};

//-----------------------------------------------------------------------------------

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

export async function fetchRecipes() {
  const response = await fetch("http://localhost:5086/recipes");
  if (!response.ok) {
    throw new Error("Failed to fetch recipes");
  }

  const resData = await response.json();
  return resData.data;
}

export async function fetchRecipeById(id) {
  const response = await fetch(`http://localhost:5086/recipes/${id}`);
  const resData = await response.json();
  if (!response.ok) {
    throw new Error("Failed to fetch recipe " + id);
  }

  return resData.data;
}
