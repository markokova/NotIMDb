import './Auth.css';
import { register } from "../../services/auth_service";
import React, { useState } from 'react';
import { Navigate } from "react-router-dom";

function Register() {

    const [credentials, setCredentials] = useState({
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        email: '',
        password: ''
    })

    const [registered, setRegistered] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');

    const handleCredentialsChange = (e) => {
        const { name, value } = e.target;
        setCredentials((prevCredentials) => ({
            ...prevCredentials,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        await register(credentials)
            .then((response) => {
                setRegistered(response.data ? true : false);
                console.log(response.data);
                setCredentials({ firstName: '', lastName:'', dateOfBirth:'', email: '', password: '' });
                setErrorMessage('');
            })
            .catch(function (error) {
                setErrorMessage(error.response.data ? error.response.data : 'Something went wrong');
                console.log(error);
            });
    };

    if (registered) {
        return <Navigate replace to="/" />;
    }
    else{

    }
    return (
        <div className="Register">
            <div className="formContainer">
                <form className="testForm" onSubmit={handleSubmit}>
                    <div className="row">
                        <label htmlFor="firstName">First Name</label>
                        <input type="text"
                            id="firstName"
                            name="firstName"
                            value={credentials.firstName}
                            onChange={handleCredentialsChange}>
                        </input>
                    </div>
                    <div className="row">
                        <label htmlFor="lastName">Last Name</label>
                        <input type="text"
                            id="lastName"
                            name="lastName"
                            value={credentials.lastName}
                            onChange={handleCredentialsChange}>
                        </input>
                    </div>
                    <div className="row">
                        <label htmlFor="email">Email</label>
                        <input type="email"
                            id="email"
                            name="email"
                            value={credentials.email}
                            onChange={handleCredentialsChange}>
                        </input>
                    </div>
                    <div className="row">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={credentials.password}
                            onChange={handleCredentialsChange}>
                        </input>
                    </div>
                    <div className="row">
                        <label htmlFor="dateOfBirth">Date of Birth</label>
                        <input
                            type="date"
                            id="dateOfBirth"
                            name="dateOfBirth"
                            value={credentials.dateOfBirth}
                            onChange={handleCredentialsChange}>
                        </input>
                    </div>

                    <div className="row">
                        <button type="submit">Register</button>
                    </div>
                    <div className="row">
                        {errorMessage && <p>{errorMessage}</p>}
                    </div>
                </form>

            </div>
        </div>);
}

export default Register;
