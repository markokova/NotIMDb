import axios from "axios"

export const getGenres = () => {
    return axios.get("https://localhost:44394/api/Genre");
}