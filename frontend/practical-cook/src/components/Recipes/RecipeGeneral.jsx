import { useState } from "react";
import Input from "../UI/Input";

export default function RecipeGeneralidades({ onSubmitGeneral }) {
  const [isPublic, setIsPublic] = useState(false);

  const handleChangeSelector = () => {
    setIsPublic(!isPublic);
  };
  const submitHandler = (e) => {
    e.preventDefault();
    //...

    onSubmitGeneral();
  };

  const privacy = !isPublic ? "Private" : "Public";

  return (
    <form onSubmit={submitHandler}>
      <div>
        <Input id="name" label="Name *" type="text" required />
        <p className="control">
          <label htmlFor="description">Description</label>
          <textarea id="description" name="description" rows={3} />
        </p>
        <Input id="calories" label="Calories *" type="number" required />
        <div className="control">
          <label htmlFor="difficulty">Difficulty</label>
          <select id="difficulty" name="difficulty">
            <option value="easy">Easy</option>
            <option value="medium">Medium</option>
            <option value="hard">Hard</option>
          </select>
        </div>
        <Input
          id="Prep-time"
          label="Preparation time (minutes) *"
          type="number"
          required
        />
        <Input
          id="servings"
          label="Portions / Servings *"
          type="number"
          required
        />
      </div>
      <div className="right-column">
        <div className="control">
          <label htmlFor="image">Image</label>
          <div id="image-selector">Image selector</div>
          <button type="button">Browse...</button>
        </div>
        <div>
          <p id={`public-label-${privacy}`}>{privacy}</p>
          <label className="switch" onChange={handleChangeSelector}>
            <input id="isPublic" name="isPublic" type="checkbox" />
            <span className="slider"></span>
          </label>
        </div>
        <div className="actions">
          <button>Next</button>
        </div>
      </div>
    </form>
  );
}
