import { Link } from "react-router-dom";
import { logOut } from "../services/auth_service";
import '../components/styles/Nav.css';

function Nav(props) {
    const handleLogout = () => {
        props.user && props.setUser(null);
        logOut();
    }
    return (
        <nav>
            <Link to={"/"} className="nav-link">
                <h2>NotIMDb</h2>
            </Link>
            <ul className="nav-links">
                {(props.user && props.user.isAdmin) &&
                    <Link to={"/admin_dashboard/movies"} className="nav-link">
                        <li>Admin Dashboard</li>
                    </Link>
                }
                {(props.user && props.user.isLoggedIn) &&
                    <Link to={"/watchlist"} className="nav-link">
                        <li>Watchlist</li>
                    </Link>
                }
            </ul>

            {props.user && props.user.isLoggedIn ?
                <div className="nav-links auth dropdown">
                    <span>Welcome, {props.user.firstName}</span>
                    <button className="btn btn-sm btn-user dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fa-solid fa-user"></i>
                    </button>
                    <ul className="dropdown-menu dropdown-menu-dark dropdown-menu-end">
                        <Link
                            to={'/user/details/' + props.user.id}
                            // state={{ data: {fromProfile: true, id: null} }}
                            className="dropdown-item">
                            <li>Profile</li>
                        </Link>

                        <li className="dropdown-item">Watchlist</li>
                        <li><hr class="dropdown-divider" /></li>
                        <li className="dropdown-item" onClick={handleLogout}>Log out</li>
                    </ul>
                </div>
                :
                <ul className="nav-links auth">
                    <Link to={"/login"} className="nav-link">
                        <li>Log In</li>
                    </Link>
                    <Link to={"/register"} className="nav-link">
                        <li>Register</li>
                    </Link>
                </ul>
            }
        </nav>
    );
}

export default Nav;