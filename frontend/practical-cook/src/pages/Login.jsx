import { redirect } from "react-router-dom";
import LoginForm from "../components/Login/LoginForm.jsx";

function LoginPage() {
  return <LoginForm />;
}

export default LoginPage;

export async function action({ request }) {
  const data = await request.formData();

  const mode = new URL(request.url).searchParams.get("mode") || "login";
  const authData = {
    email: data.get("email"),
    password: data.get("password"),
  };

  if (mode !== "login" && mode !== "signup") {
    throw JSON.stringify({
      message: "Invalid mode! Use 'login' or 'signup'.",
      status: 422,
    });
  }

  const response = await fetch("http://localhost:5086/Auth/" + mode, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(authData),
  });

  if (response.status === 409 || response.status === 401) {
    return response;
  }

  if (!response.ok) {
    throw new Response(
      JSON.stringify({ isError: true, message: "Authentication failed!" }),
      { status: 500 }
    );
  }

  const resData = await response.json();
  const token = resData.data.accessToken;

  localStorage.setItem("token", token);
  const expiration = new Date();
  expiration.setHours(expiration.getHours() + 1);
  localStorage.setItem("expiration", expiration.toISOString());

  return redirect("/recipes");
}
