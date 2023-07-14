import { useState, useEffect } from "react";
import AdminNav from "../../components/AdminNav";
import UsersTable from "../UsersTable";
import { getUsers } from "../../services/user_service";


function AdminUserDashboard(props){
    const [users, setUsers] = useState([]);

    useEffect(() => {
        getUsers(props.user.token).then((response) => {
          console.log(response);
          setUsers(response.data.Results);
      })
      .catch((error) => {
        console.error("Error fetching users:", error);
      });
      }, []);

    return(
        <div>
            <AdminNav/>
            <UsersTable users={users}/>
        </div>
    );
}

export default AdminUserDashboard;