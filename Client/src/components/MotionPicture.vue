<template>

<body>
  <div>

  <div>

<h1><i class="fa-solid fa-film"></i>  Motion Picture Films!  <i class="fa-solid fa-film"></i></h1>

<button v-show="!showForm" v-on:click="showForm = true;">Add Movie <i class="fa-solid fa-circle-plus"></i></button>


  </div>


<div v-show="!showForm">
<table>
<tr>
<th>Name</th>
<th>Description</th>
<th>Release Year</th>
<th>Actions</th>
</tr>

<tr v-for="movie in movies" :key="movie.id">
<td>{{ movie.name }}</td>
<td>{{ movie.description}}</td>
<td>{{ movie.release_year}}</td>
<td>
<button v-on:click="editForm(movie); showForm = true; showDelete = true;" ><i class="fa-solid fa-pen-to-square"></i></button>
<button v-on:click="editForm(movie); addForm()"><i class="fa-solid fa-copy"></i></button>

</td>
</tr>


</table>
</div>

<div id="form" v-show="showForm">
  <form v-on:submit.prevent="submitForm()" >
    <p id="name">Name: </p>
    
<input id="name" type="text" maxlength="50" minlength="1" v-model="newMovie.name" />
<p></p>
<p id="description">Description: </p>

<textarea id="description" rows="5" maxlength="500" v-model="newMovie.description"/>
<p></p>
<p id="release">Release Year: </p>

<input id="release" type="number" v-model="newMovie.release_year"/>
<p></p>

<button type="submit">Save <i class="fa-solid fa-floppy-disk"></i></button>
</form>
<p></p>
<button v-on:click="clearForm(); showForm = false">Cancel <i class="fa-solid fa-ban"></i></button>
<p></p>
<button v-show="showDelete" v-on:click="deleteMovie(newMovie.id)">Delete <i class="fa-solid fa-trash-can"></i></button>
</div>

      
</div>
</body>
</template>

<script>

import movieService from "../services/movieService";

export default {

  data() {
    return {
      movies: [],
      isLoading: true,
      showForm: false,
      showDelete: false,
      newMovie: {
        id: 0,
        name: '',
        description: '',
        release_year: 2022
      }
      
    };
  },
  
  created() {
    movieService.getMovies().then(response => {
      this.movies = response.data;
      this.isLoading = false;
    })
    



    
  },
  methods: {
    editForm(movie) {
      this.newMovie.name = movie.name;
      this.newMovie.id = movie.id;
      this.newMovie.description = movie.description;
      this.newMovie.release_year = movie.release_year;
     
    },

    inputValidation() {
      let bool = true;
      if(this.newMovie.name == '') {
        alert("Name Field Required")
        document.getElementById("name").classList.add("badInput")
        bool = false;
      } else {
        document.getElementById("name").classList.remove("badInput")
      }
      if (this.newMovie.release_year < 1000 || this.newMovie.release_year > 9999) {
        alert("Release Year Must Be Valid Year")
        document.getElementById("release").classList.add("badInput");
        bool = false;
      } else {
        document.getElementById("release").classList.remove("badInput");
      }

      return bool;
    },

    submitForm() {
      if (this.inputValidation()) {
        if(this.newMovie.id == 0) {
           this.addForm()
           this.showForm = false;
           
           } else {
             this.updateMovie()
             this.showForm = false;
             
             }
      }
      
    },

    addForm() {
      
      this.newMovie.release_year = parseInt(this.newMovie.release_year)
      movieService.addMovie(this.newMovie)
      .then(response => {
          if (response.status === 200) {
            movieService.getMovies().then(response => {
      this.movies = response.data;
      alert("Movie Added")
    })}
          this.clearForm();
          
        })
      .catch(error => {
          console.error(error);
        });

    },

    clearForm() {
      this.newMovie.id = 0;
      this.newMovie.name = '';
      this.newMovie.description = '';
      this.newMovie.release_year = 2022;
       document.getElementById("name").classList.remove("badInput");
      document.getElementById("release").classList.remove("badInput");
      this.showDelete = false;
    },

    updateMovie() {
      this.newMovie.release_year = parseInt(this.newMovie.release_year)
      movieService.updateMovie(this.newMovie)
      .then(response => {
          if (response.status === 200) {
            movieService.getMovies().then(response => {
      this.movies = response.data;
      
    })}
          this.clearForm();
          alert("Movie Updated");
        })
      .catch(error => {
          console.error(error);
        });

    },

    deleteMovie(id) {
      if(confirm("Are You Sure You Want To Delete?")){
      movieService.deleteMovie(id).then(() => {
           
            movieService.getMovies().then(response => {
      this.movies = response.data;
      this.showForm = false;
      this.showDelete = false;
      this.clearForm();
    })
          
        }).catch(error => {
          
            console.error(error);
          
        });
    }
    }
  }

}
</script>

<style>

table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}

table {
  table-layout: auto;
  width: 70%;
margin-left: auto;
margin-right: auto;
  background: lightgray;
  

}

body {
  background: lightseagreen;
}

td, th {
  padding: 10px;
}

.badInput {
  text-decoration: underline;
  text-decoration-color: red;
  text-decoration-style: dotted;
  
}

#form {
  text-align: start;
  padding-left: 40%;
  padding-right: 40%;
}

input#name, input#release input#description {
  width: 50%;
  
}

tr:hover {background-color: yellow;}
th {
  background-color: blue;
  color: white;
}
</style>
