import logo from "../../assets/PC.png";
import  "./Navbar.css";

export default function Navbar() {
  return (
    <nav className={"navbar"}>
      <a href="index.html">
        <img src={logo} alt=""></img>
      </a>

      <div className={"navbar__items_left"}>
        <a>My meals</a>
        <a>Calendar</a>
        <a>Shopping List</a>
      </div>
      <div className={"navbar__items_right"}>
        <a>User</a>
      </div>
    </nav>
  );
}
