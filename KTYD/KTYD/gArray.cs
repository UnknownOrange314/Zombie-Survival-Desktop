using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace KTYD
{

    /// <summary>
    /// Splits the playing area up into small squares of n*n pixels with each entity being stored in their corresponding square
    /// This should make collision detection faster


    /// </summary>
    public class gArray
    {



        Dictionary<Entity, intVector> eLocs;

        List<Entity>[,] containers;
        int xGrids = 0;  //Number of grids in the xDirection
        int yGrids = 0; //Number of grids in the yDirection
        int pSize;

        int iterX = 0;
        int iterY = 0;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x-size of playing area</param>
        /// <param name="y">y-size of playing area</param>
    
        public gArray(int xSize, int ySize)
        {
            int p = GameConfig.GRID_PARTITION_SIZE;
            eLocs = new Dictionary<Entity, intVector>();

            xGrids = 12 + xSize / p;
            yGrids = 12 + Math.Abs(ySize) / p;

            pSize = p;
            containers = new List<Entity>[xGrids, yGrids];
            for (int x = 0; x < xGrids; x++)
            {
                for (int y = 0; y < yGrids; y++)
                {

                    List<Entity> add = new List<Entity>();
                    containers[x, y] = add;
                    //containers[x,y].Add(null);

                }


            }
     

        }


        /// <summary>
        /// Returns the grid location of a vector
        /// </summary>
        /// <param name="v">Location vector</param>
        public intVector gridLoc(Vector2 v)
        {

            return new intVector(v.X / pSize, v.Y / pSize);
        }

        /// <summary>
        /// Adds an entity to the gArray
        /// </summary>
        /// <param name="e">Entity to add to map</param>
        public void Add(Entity e)
        {

            intVector gLoc = gridLoc(e.Center);
            
            eLocs.Add(e, gLoc);
            

            if (containers[gLoc.getX(), gLoc.getY()] == null)
            {
                List<Entity> a = new List<Entity>();
                a.Add(e);
                containers[gLoc.getX(), gLoc.getY()] = a;
            }
            else
            {
                containers[gLoc.getX(), gLoc.getY()].Add(e);
            }

        }

        /// <summary>
        /// Removes an entity from the map
        /// </summary>
        /// <param name="e">Entity to be removed</param>
        public void Remove(Entity e)
        {
            intVector eLoc = eLocs[e];
            containers[eLoc.getX(), eLoc.getY()].Remove(e);
            eLocs.Remove(e);
        }

        /// <summary>
        /// Starts the iteration 
        /// </summary>
        public void startIteration()
        {
            iterX = 0;
            iterY = 0;
        }


        /// <summary>
        /// Can the array iterate over more entities
        /// </summary>
        public Boolean hasNext()
        {
            if (iterY >= yGrids)
            {
                return false;

            }
            return true;

        }
        /// <summary>
        /// Moves the iterator to the next grid and returns the entities in it
        /// </summary>
        public List<Entity> returnNext()
        {

            List<Entity> returnList = containers[(int)iterX, (int)iterY];
            iterX = iterX + 1;
            if (iterX == xGrids)
            {
                iterX = 0;
                iterY = iterY + 1;
            }
            return returnList;
        }

        /// <summary>
        /// Adds a list of entities to the array
        /// </summary>
        /// <param name="addList">Entities to be added</param>
        public void AddRange(List<Entity> addList)
        {
            foreach (Entity e in addList)
            {

                Add(e);

            }


        }


        /// <summary>
        /// Gets the xLocation of the current iterator
        /// Im not sure if this is necessary.
        /// </summary>

        public int getIterationX()
        {
            return iterX;
        }

        /// <summary>
        /// Gets the yLocation of the current iterator
        /// Im not sure if this is necessary.
        /// </summary>
        public int getIterationY()
        {
            return iterY;
        }

        /// <summary>
        ///  Does the array contain an entity
        /// </summary>
        /// <param name="e">An entity</param>
        public bool Contains(Entity e)
        {
            return eLocs.ContainsKey(e);


        }

        /// <summary>
        /// Is the entity in the correct grid
        /// </summary>
        /// <param name="e">An entity</param>
        public bool inCorrectPlace(Entity e)
        {
            int x = (int)(e.Center.X) / pSize;
            int y = (int)(e.Center.Y) / pSize;

            if (containers[x, y].Contains(e) == false)
            {
                return false;

            }
            return true;

        }

        /// <summary>
        /// Makes sure that entities are in the correct location
        /// </summary>
 
        public void updateGridLocs()
        {
            startIteration();
            List<Entity> toBeMoved = new List<Entity>();
            while (true)
            {
                if (hasNext() == false)
                {
                    break;
                }
                List<Entity> eList = returnNext();
                foreach (Entity e in eList)
                {
                    if (inCorrectPlace(e) == false)
                    {
                        toBeMoved.Add(e);

                    }

                }
            }
            foreach (Entity e in toBeMoved)
            {
                intVector eLoc = eLocs[e];
                containers[eLoc.getX(), eLoc.getY()].Remove(e);
                eLocs.Remove(e);

                intVector newLoc = gridLoc(e.Center);
     
                eLocs.Add(e, newLoc);

                containers[newLoc.getX(), newLoc.getY()].Add(e);



            }


        }

        /// <summary>
        /// Gets a list of entities close to a particular entitiy
        /// </summary>
        /// <param name="addList">Any entity</param>
        public List<Entity> getNearbycontainers(Entity e)
        {

            intVector gLoc = gridLoc(e.Center);
            int x = gLoc.getX();
            int y = gLoc.getY();

            List<Entity> nearbycontainers = new List<Entity>();

            for (int i = -1; i < 2; ++i)
            {

                if (x + i >= xGrids)
                {

                    break;
                }
                while (x + i < 0)
                {
                    i++;
                }
                for (int j = -1; j < 2; j++)
                {
                    while (y + j < 0)
                    {
                        j++;
                    }
                    if (y + j >= yGrids)
                    {
                        break;
                    }


                    foreach (Entity en in containers[x + i, y + j])
                    {
                        nearbycontainers.Add(en);
                    }


                }

            }
            return nearbycontainers;





        }


    }

}
