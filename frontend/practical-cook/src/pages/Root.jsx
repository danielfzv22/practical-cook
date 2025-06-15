import { Outlet, useNavigation } from "react-router-dom";
import Navbar from "../components/Layout/Navbar.jsx";

function RootLayout() {
  //const navigation = useNavigation();

  return (
    <>
      <Navbar />
      <main>{/* {navigation.state === "loading" && <p>Loading...</p>} */}</main>
      <Outlet />
    </>
  );
}

export default RootLayout;
