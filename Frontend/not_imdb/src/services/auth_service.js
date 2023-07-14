import axios from "axios";

const baseURL = 'https://localhost:44394/';

export const login = (creds) => {
  return axios.post(baseURL + "api/user/login", creds);
}

export const logOut = () => {
  localStorage.getItem("loggedInUser") && localStorage.removeItem("loggedInUser");
  localStorage.getItem("userToken") && localStorage.removeItem("userToken");
}

export const register = (creds) => {
  return axios.post(baseURL + "api/user/register", creds);
}

