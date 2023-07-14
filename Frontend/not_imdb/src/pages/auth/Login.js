import './Auth.css';
import { login } from "../../services/auth_service";
import React, { useState, useEffect } from 'react';
import { Navigate } from "react-router-dom";
import { setLoggedInUser } from "../../services/user_service";

function Login(props) {
    const [credentials, setCredentials] = useState({
        email: '',
        password: ''
    })

    const [errorMessage, setErrorMessage] = useState('');

    const [authenticated, setauthenticated] = useState(false);

    useEffect(() => {
        const loggedInUser = localStorage.getItem("loggedInUser");
        if (loggedInUser) {
            setauthenticated(true);
        }
    }, []);

    const handleCredentialsChange = (e) => {
        const { name, value } = e.target;
        setCredentials((prevCredentials) => ({
            ...prevCredentials,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        await login(credentials)
            .then((response) => {
                setLoggedInUser(response.data)
                let loggedInUser = setLoggedInUser(response.data)
                props.setUser(loggedInUser);
                console.log(response.data);
                setCredentials({ email: '', password: '' });
                setErrorMessage('');
                setauthenticated(true);
            })
            .catch(function (error) {
                setErrorMessage(error.response.data ? error.response.data : 'Something went wrong');
                console.log(error);
            });
    };

    if (authenticated) {
        return <Navigate replace to="/" />;
    }
    else{
        return (
            <div className="Login">
                <div className="formContainer">
                    <form className="testForm" onSubmit={handleSubmit}>
                        <div className="row">
                            <label htmlFor="username">Email</label>
                            <input type="email"
                                id="email"
                                name="email"
                                value={credentials.username}
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
                            <button type="submit">Login</button>
                        </div>
                        <div className="row">
                            {errorMessage && <p>{errorMessage}</p>}
                        </div>
                    </form>

                </div>
            </div>
        );
    }
}

export default Login;
