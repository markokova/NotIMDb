// import Button from '../Button';
import { Link } from "react-router-dom";

function UsersTable(props) {
    return (
        <div>
            <table className="table table-sm">
                <thead>
                    <tr>
                        <td className="w-10"></td>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Date of Birth</th>
                        <th>Date Created</th>
                        <th>Updated by</th>
                        <th>Date Updated</th>
                        <th>Is Active</th>
                        <th>Role</th>
                    </tr>
                </thead>
                <tbody>
                    {props.users.map((user) => (
                        <tr key={user.Id} >
                            <td>
                                <Link
                                    to={"/user/details/" + user.Id}>
                                    <i class="fa-solid fa-circle-info"></i>
                                </Link>
                            </td>
                            <td>{user.Id}</td>
                            <td>{user.FirstName}</td>
                            <td>{user.LastName}</td>
                            <td>{user.Email}</td>
                            <td>{user.DateOfBirth}</td>
                            <td>{user.DateCreated}</td>
                            <td>{user.UpdatedBy}</td>
                            <td>{user.DateUpdated}</td>
                            <td>{user.IsActive}</td>
                            <td>{user.Role}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default UsersTable;