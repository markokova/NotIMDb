import axios from "axios"

export const getActors = () => {
    return axios.get("https://localhost:44394/api/Actor");
}