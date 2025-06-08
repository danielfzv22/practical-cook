import { RecipeList } from "../components/RecipeList/RecipeList.jsx";
import backgroundImage from "../assets/choppingBoard.jpg";
import { RecipeContextProvider } from "../context/RecipeContext.jsx";
import { useLoaderData } from "react-router-dom";

function HomePage() {
  const res = useLoaderData();

  if (res.isError) {
    return (
      <div style={{ textAlign: "center", marginTop: "20px" }}>
        <h2>Error loading recipes</h2>
        <p>{res.message}</p>
      </div>
    );
  }
  const recipes = res?.data || [];

  return (
    <RecipeContextProvider>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center ",
          justifyContent: "space-between",
        }}
      >
        <RecipeList
          title="Recipes"
          recipes={recipes}
          isLoading={false}
          loadingText="Loading..."
          fallbackText="No recipes available."
        />
      </div>
    </RecipeContextProvider>
  );
}

export default HomePage;
