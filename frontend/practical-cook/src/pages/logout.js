import { redirect } from "react-router-dom";

export function action() {
  // Clear the token from local storage
  localStorage.removeItem("token");
  localStorage.removeItem("expiration");

  // Redirect to the home page
  return redirect("/Auth?mode=login");
}
