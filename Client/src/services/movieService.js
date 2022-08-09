import axios from 'axios';

const http = axios.create({
  baseURL: "http://localhost:5000"
});

export default {

  

  getMovies() {
      return http.get('/api/movie');
  },

  addMovie(movie) {
    return http.post(`/api/movie`, movie);
  },

  deleteMovie(movieId) {
    return http.delete(`/api/movie/${movieId}`);
  },

  updateMovie(movie) {
    return http.put(`/api/movie`, movie);
  }
}