import Button from '../components/Button';
import { Link } from "react-router-dom";


function AdminNav(){
    return(
        <div id="adminButtons">
            <Link to={`/admin_dashboard/movies`}><Button text="Movies"></Button></Link>
            <Link to={"/admin_dashboard/genres"}><Button text="Genres"></Button></Link>
            <Link to={"/admin_dashboard/actors"}><Button text="Actors"></Button></Link>
            <Link to={"/admin_dashboard/reviews"}><Button text="Reviews"></Button></Link>
            <Link to={"/admin_dashboard/users"}><Button text="Users"></Button></Link>
        </div>
    );
}

export default AdminNav;