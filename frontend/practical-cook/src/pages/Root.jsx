import { Outlet } from "react-router-dom";
import Navbar from "../components/Layout/Navbar.jsx";

function RootLayout() {
  return (
    <>
      <Navbar />
      <Outlet />
    </>
  );
}

export default RootLayout;
