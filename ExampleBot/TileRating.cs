using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetwarApi.objects;

namespace ExampleBot
{
    class TileRating
    {
        public double rating;
        public Tile tile;

        public TileRating(double rating, Tile tile)
        {
            this.rating = rating;
            this.tile = tile;
        }
    }
}
