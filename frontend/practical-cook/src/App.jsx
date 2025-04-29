import { useEffect, useRef, useState } from "react";
import Navbar from "./components/Layout/Navbar.jsx";
import { RecipeList } from "./components/RecipeList/RecipeList.jsx";
import backgroundImage from "./assets/choppingBoard.jpg";
import Recipe from "./components/Recipes/Recipe.jsx";
import { RecipeContextProvider } from "./context/RecipeContext.jsx";
import { fetchRecipes } from "./http";

function App() {
  const [isFetching, setIsFetching] = useState(true);
  const [recipes, setRecipes] = useState([]);
  const [error, setError] = useState();

  useEffect(() => {
    setIsFetching(true);
    async function fetchingRecipes() {
      try {
        const recipes = await fetchRecipes();
        setRecipes(recipes);
        setIsFetching(false);
      } catch (error) {
        setError({
          message:
            error.message || "Could not fetch recipes, please ty again later.",
        });
        setIsFetching(false);
      }
    }

    fetchingRecipes();
  }, []);

  if (error) {
    return <Error title="An error ocurred!" message={error.message} />;
  }

  return (
    <RecipeContextProvider>
      <Navbar />
      <div
        style={{
          backgroundImage: `url(${backgroundImage})`,
          backgroundSize: "cover",
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
        }}
      >
        <RecipeList
          title="Recipes"
          recipes={recipes}
          isLoading={isFetching}
          loadingText="Loading..."
          fallbackText="No recipes available."
        />
        <Recipe />
      </div>
    </RecipeContextProvider>
  );
}

export default App;
