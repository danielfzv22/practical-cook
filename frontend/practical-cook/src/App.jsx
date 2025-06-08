import { createBrowserRouter, RouterProvider } from "react-router-dom";
import RootLayout from "./pages/Root.jsx";
import HomePage from "./pages/Home.jsx";
import ErrorPage from "./pages/Error.jsx";

import { Provider } from "./components/UI/provider.jsx";
import { LoadRecipes as recipeLoader } from "./http.js";
import LoginPage, { action as loginAction } from "./pages/Login.jsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        element: <HomePage />,
        loader: recipeLoader,
      },
      { path: "/auth", element: <LoginPage />, action: loginAction },
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
