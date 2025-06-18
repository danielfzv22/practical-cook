import { createBrowserRouter, RouterProvider } from "react-router-dom";
import RootLayout from "./pages/Root.jsx";
import HomePage from "./pages/Home.jsx";
import ErrorPage from "./pages/Error.jsx";

import { Provider } from "./components/UI/provider.jsx";
import { LoadRecipes as recipeLoader, tokenLoader } from "./util/http.js";
import LoginPage, { action as loginAction } from "./pages/Login.jsx";
import RecipeDetailPage, {
  loader as recipeDetailLoader,
} from "./pages/RecipeDetailPage.jsx";
import RecipeEditPage from "./pages/RecipeEditPage.jsx";
import RecipeNewPage from "./pages/RecipeNewPage.jsx";
import { action as logoutAction } from "./pages/logout.js";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    id: "root",
    loader: tokenLoader,
    children: [
      {
        path: "recipes",
        children: [
          {
            index: true,
            element: <HomePage />,
            loader: recipeLoader,
          },
          {
            path: ":recipeId",
            id: "recipe-detail",
            loader: recipeDetailLoader,
            children: [
              {
                index: true,
                element: <RecipeDetailPage />,
              },
              {
                path: "edit",
                element: <RecipeEditPage />,
              },
              {
                path: "new-recipe",
                element: <RecipeNewPage />,
              },
            ],
          },
        ],
      },

      { path: "auth", element: <LoginPage />, action: loginAction },
      { path: "logout", action: logoutAction },
    ],
  },
]);

function App() {
  return (
    <Provider>
      <RouterProvider router={router} />
    </Provider>
  );
}

export default App;
