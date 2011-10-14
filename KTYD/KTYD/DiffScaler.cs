using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace KTYD
{
    /// <summary>
    /// This class will act as a director to create levels
    /// </summary>
   
    public class DiffScaler
    {
        private int myScore;

        int maxEnemies;
        Random r;


        List<KTYD.Controller.Model.Obstacle> myObstacles;
        /// <summary>
        /// Constructor
        /// </summary>
        public DiffScaler()
        {
            maxEnemies = GameConfig.MAX_ENEMIES;
            myScore = 4;
            r = new Random();
            createObstacles();

        }

        public void createObstacles()
        {
            myObstacles=new List<KTYD.Controller.Model.Obstacle>();
            myObstacles.Add(new KTYD.Controller.Model.Obstacle(1,1,400,400));
        }

        public List<KTYD.Controller.Model.Obstacle> getObstacles()
        {
            return myObstacles;

        }
        /// <summary>
        /// Add point
        /// </summary>
        public void addPoint()
        {
            myScore+=1;


        }


        /// <summary>
        /// Gets the score of a player
        /// </summary>
        public int getScore()
        {

            return myScore;
        }



        /// <summary>
        /// Method to determine where enemies should spawn
        /// </summary>
        /// <param name="player">List of players on map</param>

        public Vector2 determineSpawnLocation(List<KTYD.Model.Character> player)
        {
            while (true)
            {

               

                double x = (double)r.Next(10, GameConfig.SCREEN_RES_X - 50) + r.Next() % 100;

                double y = (double)r.Next(10, GameConfig.SCREEN_RES_Y - 50 + r.Next() % 100 /*-500*/);

                Boolean close = false;

                for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
                {
                    double distance = Math.Sqrt((x - player[i].Center.X) * (x - player[i].Center.X) + (y - player[i].Center.Y) * (y - player[i].Center.Y));

                    if (distance < 100)//Do not spawn enemies close to the player.
                    {
                        close = true;
                        //return;
                        break;
                    }
                }

                if (close == false)
                {
                    return new Vector2((float)x, (float)y);
                }
               

            }

        }
        public void addItemsToMap(KTYD.Model.Map gMap)
        {
            KTYD.Model.ItemType itemTyper;
            int tempRand = r.Next() % 4;
     

            // Spawn random items to be picked up
            if (tempRand == 1)
            {

            
                itemTyper = Model.ItemType.M16_WEAPON;
            }
            else if (tempRand == 2)
            {

             
                itemTyper = Model.ItemType.FLAME_WEAPON;
                //itemTyper = Model.ItemType.HEALTH;
            }
            else if (tempRand == 3)
            {

              
                itemTyper = Model.ItemType.SHOTGUN_WEAPON;
            }
            else
            {
               
                itemTyper = Model.ItemType.HEALTH;
            }
            gMap.loadEntity(new KTYD.Model.Item(50 + r.Next() % (GameConfig.LEVEL_WIDTH - 100), 50 + r.Next() % (GameConfig.LEVEL_HEIGHT - 100), itemTyper));
        }
        /// <summary>
        /// Monitor and Add enemies to the map
        /// </summary>
        /// <param name="gMap">Map</param>
        public void addEnemiesToMap(KTYD.Model.Map gMap)
        {
           
            int eCount = gMap.EnemiesEntities.Count;
            List<KTYD.Model.Character> player = gMap.PlayerEntities;

            int difficultyScore = eCount * GameConfig.DIFF_SCALE_FACTOR;

            int spawnCount = 0;
            
            Vector2 spawnPos;
            spawnPos.X = 2;
            spawnPos.Y = 2;
            int max_runner = 20; // need this just in case while loop is indefinite (if it ever happened)
            int max_run = 10;   // need this just in case while loop is indefinite
            while (max_run !=0)
            {

               
              
                eCount++;

                spawnCount++;
                if(spawnCount>GameConfig.MAX_ENEMY_SPAWN)
                {
                    return;
                    }
                if (eCount > maxEnemies)
                {
                  
                    break;

                }
                if (difficultyScore > myScore)
                {
              

                    break;
                    
                }
               

                 spawnPos = determineSpawnLocation(gMap.getPlayersList());
                

               

                int tempRand =  r.Next() % 4;
             

                Weapon eWeapon;
                // Spawn random items to be picked up
                if (tempRand == 1)
                {

                    eWeapon = Weapon.createPistol();
             
                }
                else if (tempRand == 2)
                {

                    eWeapon = Weapon.createShotgun();
            
                    //itemTyper = Model.ItemType.HEALTH;
                }
                else if (tempRand == 3)
                {

                    eWeapon = Weapon.createPistol();
                    
                }
                else
                {
                    eWeapon = Weapon.createShotgun();
                  
                }
                difficultyScore = difficultyScore + GameConfig.DIFF_SCALE_FACTOR;

                Random r2 = new Random();
                Random r3 = new Random();
                double rot = (r2.Next()+r3.Next()+r.Next())/ (2 * Math.PI);
                KTYD.AI.Enemy e = new KTYD.AI.Enemy(spawnPos.X,spawnPos.Y,(float)(rot), GameConfig.IMG_BASE_GRAY, eWeapon, EntityState.SEARCH);
               
                gMap.loadEntity(e);
                --max_runner;
                
                
            }//while

           

            return;
            

        }
       
    }
}