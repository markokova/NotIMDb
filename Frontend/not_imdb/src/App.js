import './App.css';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import Home from "./pages/Home";
import MovieDetails from './pages/MovieDetails';
import WatchList from "./pages/Watchlist";
import Nav from "./components/Nav";
import Login from './pages/auth/Login';
import Register from './pages/auth/Register';
import UserDetails from './pages/UserDetails';
import { useState } from 'react';

import AdminMovieDashboard from "./pages/AdminMovieDashboard";
import AdminActorDashboard from './pages/AdminDasboard/AdminActorDashboard';
import AdminGenreDashboard from './pages/AdminDasboard/AdminGenreDashboard';
import AdminReviewDashboard from './pages/AdminDasboard/AdminReviewDashboard';
import Reviews from "./pages/Reviews";
import AdminUserDashboard from './pages/AdminDasboard/AdminUserDashboard';

function App() {
  const [user, setUser] = useState(JSON.parse(localStorage.getItem("loggedInUser")));

  return (
    <Router>
      <div className='everything'>
        <Nav user={user} setUser={setUser} />
        <Routes>
          <Route exact path="/" element={<Home user = {user}/>} />
          <Route path="/admin_dashboard/movies" element={<AdminMovieDashboard user={user} />}></Route>
          <Route path="/watchlist" element={<WatchList user={user} />}></Route>
          <Route path="/:id" element={<MovieDetails user={user} />}></Route>
          <Route path="/login" element={<Login user={user} setUser={setUser} />}></Route>
          <Route path="/register" element={<Register />}></Route>
          <Route path="/admin_dashboard/genres" element={<AdminGenreDashboard />}></Route>
          <Route path="/admin_dashboard/actors" element={<AdminActorDashboard />}></Route>
          <Route path="/admin_dashboard/reviews" element={<AdminReviewDashboard />}></Route>
          <Route path="/admin_dashboard/users" element={<AdminUserDashboard user={user} />}></Route>
          <Route path="/:id/reviews" element={<Reviews/>}></Route>
          <Route path="/user/details/" element={<UserDetails user={user} />}></Route>
          <Route path="/user/details/:id" element={<UserDetails user={user} />}></Route>
        </Routes>
        <div className='footer'>
          <div className='copyright'>
            <span><i class="fa-regular fa-copyright"></i></span>
            <span> Fastest Swimers</span>
            <span><i class="fa-regular fa-copyright"></i></span>
          </div>
        </div>
      </div>
    </Router>
  );
}

export default App;
