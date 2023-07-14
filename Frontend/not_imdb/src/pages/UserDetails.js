import './UserDetails.css'
import React, { useState, useEffect } from 'react';
import axios from "axios";
import { parseDate } from '../services/common_service';
import { Navigate, useParams } from "react-router-dom";

function UserDetails(props) {
    const { id } = useParams();

    const baseURL = 'https://localhost:44394/';

    const [user, setUser] = useState({});

    useEffect(() => {
        console.log(props.user);
        const getUserbyId = async (id) => {
            await axios
                .get(baseURL + "api/usermanagement/getbyid?id=" + id)
                .then((response) => {

                    setUser(response.data.Result)
                });
        };

        getUserbyId(id).catch(function (error) {
            console.log(error);
        });
    }, []);

    if (props.user === null) {
        return <Navigate replace to="/" />;
    }
    else {
        return (
            <div className="userDetails">
                <h1>User Details</h1>
                <div className="container w-30 shadow-sm bg-light rounded">

                    <div className="row text-white bg-secondary bg-gradient">
                        <div className='col-4'>User Id</div>
                        <div className='col'>{user.Id}</div>
                    </div>
                    <div className="row">
                        <div className='col'>First Name</div>
                        <div className='col'>{user.FirstName}</div>
                    </div>
                    <div className="row">
                        <div className='col'>Last Name</div>
                        <div className='col'>{user.LastName}</div>
                    </div>
                    <div className="row">
                        <div className='col'>Email</div>
                        <div className='col'>{user.Email}</div>
                    </div>
                    <div className="row">
                        <div className='col'>Date of Birth</div>
                        <div className='col'>{parseDate(user.DateOfBirth)}</div>
                    </div>
                    <div className="row">
                        <div className='col'>Date Created</div>
                        <div className='col'>{parseDate(user.DateCreated)}</div>
                    </div>
                    {(props.user.isAdmin && user.UpdatedByUser) &&
                        <div className="row">
                            <div className='col'>Updated by</div>
                            <div className='col'>{user.UpdatedByUser}</div>
                        </div>
                    }
                    {(props.user.isAdmin && user.DateUpdated) &&
                        <div className="row">
                            <div className='col'>Date Updated</div>
                            <div className='col'>{parseDate(user.DateUpdated)}</div>
                        </div>
                    }

                    <div className="row">
                        <div className='col'>Role</div>
                        <div className='col'>{user.Role}</div>
                    </div>
                </div>
            </div>
        )
    }
}

export default UserDetails;
