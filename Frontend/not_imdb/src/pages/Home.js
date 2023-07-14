import 'bootstrap/dist/css/bootstrap.min.css';
import MovieCard from "../components/movie_components/MovieCard";
import { useEffect, useState } from "react";
import { getGenres } from "../services/genre_service";
import '../components/styles/Home.css';

function Home(props){
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        getGenres().then((response) => {
            setGenres(response.data.genreRests);
        }).catch((error) => {
            console.error("Error fetching genres.", error);
        });
    }, []);

    return(
        <div className='bck'>
            <h1>Welcome to NotIMDb!</h1>
            <div>
                <h2>Watch now: </h2>
                <MovieCard user = {props.user}/>
            </div>
            {genres.map((genre) => (
                <div key={genre.Id}>
                    <h2 id="genreTitle">{genre.Title}</h2>
                    <MovieCard genreId={genre.Id} user = {props.user}/>
                </div>
            ))}
        </div>
    );
}

export default Home;