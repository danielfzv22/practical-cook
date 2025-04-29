import RecipeItem from "./RecipeItem";
import "./RecipeList.css";

export function RecipeList({
  recipes,
  fallbackText,
  isLoading,
  loadingText,
}) {
  return (
    <div className="recipes-list">
      {isLoading && <p className="fallback-text">{loadingText}</p>}
      <ul>
        {recipes.map( r => <RecipeItem key={r.id} id={r.id} title={r.name} description={r.description}/>)}
      </ul>
    </div>
  );
}
