﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_pacman
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class AStarProgram
    {
        public bool[,] map;
        private SearchParameters sP;
        

        /// <summary>
        /// Outputs three examples of path finding to the Console.
        /// </summary>
        /// <remarks>The examples have copied from the unit tests!</remarks>
        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            search(pacman.targetpos,Ghost.targetpos);
            PathFinder pathFinder = new PathFinder(sP);
            List<Point> path = pathFinder.FindPath();
            //ShowRoute("The algorithm should find a direct path without obstacles:", path);
            //Console.WriteLine();

            // Now add an obstacle

            //AddWallWithGap();
            //pathFinder = new PathFinder(searchParameters);
            //path = pathFinder.FindPath();
            //ShowRoute("The algorithm should find a route around the obstacle:", path);
            //Console.WriteLine();

            // Finally, create a barrier between the start and end points

            //AddWallWithoutGap();
            //pathFinder = new PathFinder(searchParameters);
            //path = pathFinder.FindPath();
            //ShowRoute("The algorithm should not be able to find a route around the barrier:", path);
            //Console.WriteLine();

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }

        /// <summary>
        /// Displays the map and path as a simple grid to the console
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        //private void ShowRoute(string title, IEnumerable<Point> path)
        //{
        //    Console.WriteLine("{0}\r\n", title);
        //    for (int y = this.map.GetLength(1) - 1; y >= 0 ; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
        //    {
        //        for (int x = 0; x < this.map.GetLength(0); x++)
        //        {
        //            if (this.searchParameters.StartLocation.Equals(new Point(x, y)))
        //                // Show the start position
        //                Console.Write('S');
        //            else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
        //                // Show the end position
        //                Console.Write('F');
        //            else if (this.map[x, y] == false)
        //                // Show any barriers
        //                Console.Write('░');
        //            else if (path.Where(p => p.X == x && p.Y == y).Any())
        //                // Show the path in between
        //                Console.Write('*');
        //            else
        //                // Show nodes that aren't part of the path
        //                Console.Write('·');
        //        }

        //        Console.WriteLine();
        //    }
        //}

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        public void InitializeMap(int i, int j)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[i, j];
            for (int y = 0; y < i; y++)
                for (int x = 0; x < j; x++)
                    map[x, y] = true;

            var startLocation = new Point((int)pacman.targetpos.X, (int)pacman.targetpos.Y);
            var endLocation = new Point((int)Ghost.targetpos.X, (int)Ghost.targetpos.Y);
            this.sP = new SearchParameters(startLocation, endLocation, map);
        }

        public void AddWalls(int i, int j)
        {
            map[i, j] = false;
        }
        public void search(Vector2 playerpos, Vector2 ghostpos)
        {

            var startLocation = new Point((int)ghostpos.X, (int)ghostpos.Y);
            var endLocation = new Point((int)playerpos.X, (int)playerpos.Y);
            sP = new SearchParameters(startLocation, endLocation, map);
        }
    }
}
       
        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
//        private void AddWallWithGap()
//        {
//            //  □ □ □ ■ □ □ □
//            //  □ □ □ ■ □ □ □
//            //  □ S □ ■ □ F □
//            //  □ □ □ ■ ■ □ □
//            //  □ □ □ □ □ □ □

//            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

//            this.map[3, 4] = false;
//            this.map[3, 3] = false;
//            this.map[3, 2] = false;
//            this.map[3, 1] = false;
//            this.map[4, 1] = false;
//        }

//        /// <summary>
//        /// Create a closed barrier between S and F
//        /// </summary>
//        private void AddWallWithoutGap()
//        {
//            //  □ □ □ ■ □ □ □
//            //  □ □ □ ■ □ □ □
//            //  □ S □ ■ □ F □
//            //  □ □ □ ■ □ □ □
//            //  □ □ □ ■ □ □ □

//            // No path

//            this.map[3, 4] = false;
//            this.map[3, 3] = false;
//            this.map[3, 2] = false;
//            this.map[3, 1] = false;
//            this.map[3, 0] = false;
//        }
//    }
//}
