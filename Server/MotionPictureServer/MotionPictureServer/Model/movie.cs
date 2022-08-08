using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionPictureServer.Model
{
    public class Movie
    {

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int release_year { get; set; }

    }
}
