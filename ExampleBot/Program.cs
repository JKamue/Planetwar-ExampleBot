using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetwarApi;
using PlanetwarApi.objects;

namespace ExampleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            new PlanetwarBot(BotLoop);
        }

        private static void BotLoop(PlanetwarApi.PlanetwarApi api, string username, Round roundInfo, Map map,
            List<Event> events, List<Player> players, List<Sent> sent)
        {

            // Liste eigener Planeten erstellen
            var myPlanets = new List<Tile>();
            foreach (var tile in map.tiles)
            {
                if (tile.owner != null && tile.owner.name == username )
                    myPlanets.Add(tile);
            }

            // Über alle eigenen planeten loopen
            foreach (var myPlanet in myPlanets)
            {
                var ratings = new List<TileRating>();

                foreach (var tile in map.tiles)
                {
                    // Only tiles with planets are relevant
                    if (!tile.hasPlanet)
                        continue;
                    
                    // Prevent the bot form attacking its own tile
                    if (tile.owner.name == username)
                        continue;

                    // Bewertung ausrechnen
                    var distance = myPlanet.location.Distance(tile.location);
                    var score = Math.Pow(distance, 1.5) - tile.planet.production * 1.5;
                    
                    // HinzuFügen zur Liste
                    ratings.Add(new TileRating(score, tile));
                }


                // Order tile list by rating
                ratings = ratings.OrderBy(t => t.rating).ToList();
                
                if (ratings.Count == 0)
                    continue;

                var target = ratings[0].tile;
                api.MoveShips(myPlanet.location, target.location, myPlanet.ships);
                Console.WriteLine($"Ich greife an: {target.location.X} | {target.location.Y}");
            }
        }
    }
}
