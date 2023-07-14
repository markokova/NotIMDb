import axios from "axios";

const baseUrl = 'https://localhost:44394/';

export const setLoggedInUser = (userData) => {

    // const [user, setUser] = useContext(UserContext)
    let loggedInUser = {
        isLoggedIn: true,
        isGuest: userData.Role === 'Guest',
        isUser: userData.Role === 'User',
        isAdmin: userData.Role === 'Administrator',
        id: userData.Id,
        firstName: userData.FirstName,
        lastName: userData.LastName,
        email: userData.Email,
        role: userData.Role,
        token: userData.Token
    }

    // setUser(loggedInUser)
    // let userToken = userData.token

    localStorage.setItem("loggedInUser", JSON.stringify(loggedInUser))
    // localStorage.setItem("userToken", JSON.stringify(userToken))

    return loggedInUser;
}

export const getLoggedInUser = () => {

    return localStorage.getItem("loggedInUser") != null ? JSON.parse(localStorage.getItem("loggedInUser")) : null;
}

export const getUsers = (token) => {
    let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }

    return axios.get(baseUrl + 'api/usermanagement/getall?email&firstname&lastname', { headers: header });
}
