using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionPictureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        
        [HttpGet]
        public List<Model.Movie> Get()
        {
            return DAO.movieDAO.GetMovies();
        }

        [HttpPost]
        public Model.Movie Post(Model.Movie movieToAdd)
        {
            return DAO.movieDAO.PostMovies(movieToAdd);
        }

        [HttpPut]
        public Model.Movie Put(Model.Movie movieToUpdate)
        {
            return DAO.movieDAO.UpdateMovie(movieToUpdate);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DAO.movieDAO.DeleteMovie(id);
        }



    }
}
