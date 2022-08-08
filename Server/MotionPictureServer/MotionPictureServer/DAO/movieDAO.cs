 using MotionPictureServer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MotionPictureServer.DAO
{
    public class movieDAO
    {

        public static List<Movie> GetMovies()
        {
            var movieList = new List<Movie>();

            string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=MotionPictures;"
            + "Integrated Security=true";

            string queryString = "SELECT * FROM MotionPictures";



            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {


                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var movie = new Movie();
                        movie.id = (int)reader["ID"];
                        movie.name = reader[1].ToString();
                        movie.description = reader[2].ToString();
                        movie.release_year = (int)reader[3];

                        //  movie.ID = Convert.ToInt32(reader["ID"]);
                        //  movie.Name = Convert.reader["Name"].ToString;




                        movieList.Add(movie);

                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    
                }


            }

            return movieList;
        }

        public static Movie PostMovies(Movie movie)
        {
            

            string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=MotionPictures;"
            + "Integrated Security=true";

            string queryString = "INSERT INTO MotionPictures(name, description, release_year) OUTPUT inserted.id VALUES (@moviename, @moviedescription, @movierelease_year)";



            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@moviename", movie.name);
                command.Parameters.AddWithValue("@moviedescription", movie.description);
                command.Parameters.AddWithValue("@movierelease_year", movie.release_year);
               
                movie.id = Convert.ToInt32(command.ExecuteScalar());


                connection.Close();


                return movie;



            }
            

        }

        public static Movie UpdateMovie(Movie movie)
        {


            string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=MotionPictures;"
            + "Integrated Security=true";

            
            string queryString = "UPDATE MotionPictures SET name = @moviename, description = @moviedescription, release_year = @movierelease_year WHERE id = @movieid";

                

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@moviename", movie.name);
                command.Parameters.AddWithValue("@moviedescription", movie.description);
                command.Parameters.AddWithValue("@movierelease_year", movie.release_year);
                command.Parameters.AddWithValue("@movieid", movie.id);

                command.ExecuteNonQuery();

                connection.Close();


                return movie;



            }


        }
        public static void DeleteMovie(int id)
        {


            string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=MotionPictures;"
            + "Integrated Security=true";

            string queryString = "DELETE FROM MotionPictures WHERE id = @movieid";



            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@movieid", id);
                command.ExecuteNonQuery();
                connection.Close();

            }


        }

    }
}


        

