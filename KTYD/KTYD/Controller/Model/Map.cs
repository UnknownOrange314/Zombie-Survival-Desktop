using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using System.Core.dll; 
namespace KTYD.Model
{
    /// <summary>
    /// Map object (This sounds misleading and probably being put in the wrong place.)
    /// This map will act as a game controller where main game logic takes place.
    /// Base: Observable
    /// 
    /// Observer Pattern
    /// </summary>
    public class Map : Observable
    {

        //private List<Entity> containers;

        private List<Entity> bulletList;

        private List<Entity> trashContainers;

        private List<Entity> deadContainers;

        private Controller.Players playersList;

        private List<Entity> enemiesList;

        private List<Entity> trashBullets;

        public List<KTYD.Controller.Model.Obstacle> obstacles; //List of impassable obstacles in the game, this should probably be refactored

        private float gameTime;

        private Microsoft.Xna.Framework.Audio.SoundEffect[] SoundEffect;

        private int width, height;

        //Grid array for storing entities
        public gArray containers;
      
        // AI director
        DiffScaler AIDirector;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Map(Controller.Players p)
        {

            //temporary test code
            
            KTYD.Controller.Model.Obstacle o = new KTYD.Controller.Model.Obstacle(600, 600, 1000,1000);
            obstacles = new List<KTYD.Controller.Model.Obstacle>();
            //obstacles.Add(o);

            //end test code
            containers = new gArray(GameConfig.LEVEL_WIDTH, GameConfig.LEVEL_HEIGHT);

            trashBullets = new List<Entity>();
            trashContainers = new List<Entity>();
            bulletList = new List<Entity>();
            deadContainers = new List<Entity>();
            playersList = p;
            enemiesList = new List<Entity>();
            AIDirector = new DiffScaler();
            width = GameConfig.LEVEL_HEIGHT;
            height = GameConfig.LEVEL_WIDTH;
            List<KTYD.Controller.Model.Obstacle> e = AIDirector.getObstacles();
            foreach (KTYD.Controller.Model.Obstacle f in e)
            {
               //ystem.Console.WriteLine("hi");
               // f.index(containers);
                //loadEntity((Entity)f);

            }
        }

         public List<KTYD.Controller.Model.Obstacle> getObstacles()
        {
            return obstacles;

        }

        public void loadSoundEffect(Microsoft.Xna.Framework.Audio.SoundEffect[] array)
        {
            this.SoundEffect = array;
        }

        /// <summary>
        /// Specifiy width and height of the map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void setDimension(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Load a list of entities
        /// </summary>
        /// <param name="list"></param>
        public void loadEntities(List<Entity> list)
        {
            foreach (Entity e in list)
            {
                containers.Add(e);
            }

        }

        /// <summary>
        /// Return all entities in the map
        /// </summary>
        public gArray allEntities
        {
            get
            {
                return containers;
            }
        }



        /// <summary>
        /// Return all entities in the map
        /// </summary>
        public gArray getContainers()
        {

            return this.containers;
        }


        /// <summary>
        /// Return list of players in the map
        /// </summary>
        public List<KTYD.Model.Character> getPlayersList()
        {

            return playersList.getPlayers();
        }

        /// <summary>
        /// Return list of players in the map
        /// </summary>
        public List<Entity> getBulletList()
        {
            return this.bulletList;

        }
        /// <summary>
        /// Return all dead entities in the map
        /// </summary>
        public List<Entity> allDeadEntities
        {
            get
            {
                return deadContainers;
            }
        }

        /// <summary>
        /// Return the list of all players
        /// </summary>
        public List<KTYD.Model.Character> PlayerEntities
        {
            get
            {
                return playersList.getPlayers();
            }
        }
        /// <summary>
        /// Return the list of all enemies
        /// </summary>
        public DiffScaler diffScaler()
        {

            return AIDirector;
        }

        /// <summary>
        /// Return the list of all enemies
        /// </summary>
        public List<Entity> EnemiesEntities
        {
            get
            {
                return enemiesList;
            }
        }

        public void addBullet(Entity e,Weapon w,EntityType t)
        {

            w.addBullet(e,  t, trashBullets, bulletList);

           
             

        }

     
        /// <summary>
        /// Load an entity
        /// </summary>
        /// <param name="e"></param>
        public void loadEntity(Entity e)
        {
           

            if (e != null)
            {
                containers.Add(e);
                if (e.Type==EntityType.PLAYER)
                {
                    playersList.Add((KTYD.Model.Character)e); //The list of players is being stored in two places.
                }
                else if (e.Type == EntityType.ENEMY)
                {
                    enemiesList.Add(e);
                }
            }

            
    
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="e"></param>
        public void removeEntity(Entity e)
        {
            if (containers.Contains(e))
            {
                containers.Remove(e);
            }
        }


        /// <summary>
        /// Remove an entity
        /// </summary>
        public List<Entity> getTrashContainers()
        {


            return trashContainers;

        }


        /// <summary>
        /// Clean up
        /// </summary>
        private void cleanUp()
        {
            if (trashContainers.Count() > 0)
            {
                if (gameTime > GameConfig.TEN_MIN_IN_MILLISECONDS)   // deallocate stuff every 10 mins
                {
                    foreach (Entity u in trashContainers)
                    {
                        if (containers.Contains(u))
                        {
                            if (u.Type == EntityType.BULLET)
                            {
                                containers.Remove(u);
                                //trashBullets.Add(u);

                            }

                            if (u.isDead() && (u.Type == EntityType.BULLET || u.Type == EntityType.ITEM))
                            {
                                containers.Remove(u);
                            }
                        }
                    }
                    gameTime = 0;
                }
            }

        }

        /// <summary>
        /// Update gameTime (Probably DEPRECATED)
        /// </summary>
        /// <param name="gameTime">Elapsed game time</param>
        public void updateTime(float gameTime)
        {
            //this.gameTime = gameTime;
            this.gameTime += GameConfig.TIME_TICK;
        }

    
        public int getScore()
        {
            return AIDirector.getScore();

        }

        /// <summary>
        /// Update action for specified entity
        /// </summary>
        /// <param name="e">Entity</param>
        private void updateActionForEntity(Entity e)
        {
            
            // for each type of entity
            switch (e.Type)
            {
                case EntityType.OBSTACLE:
                    foreach (Entity f in this.containers.getNearbycontainers(e))
                    {
                        if (e.isCollide(f))
                        {
                            f.restorePrevLocation();
                        }
                    }
                    break;
                case EntityType.PLAYER: //I think there should be a Character subclass, having the players stored as a list of entities is confusing
                    //Controller.Players eP=((Controller.Players)e);


                    


                    foreach (Entity u in this.containers.getNearbycontainers(e))//This code may need refactoring, there is collision detection code seperate from this.
                    {
                        if (e != u)
                        {
                           //ystem.Console.WriteLine(e.Type);
                          
                            if (e.isCollide(u))
                            {
                                if (u.Type == EntityType.BULLET)
                                {
                                    // If bullet is actually hitting entity
                                    if (u.isCollide(e))
                                    {
                                        trashContainers.Add(u);
                                        ((KTYD.Model.Bullet)u).hitTarget(e);
                                        ((KTYD.Model.Bullet)u).setDie();
                                    }
                                }
                                else if (u.Type == EntityType.ITEM)
                                {
                                    switch (((KTYD.Model.Item)u).ItemType)
                                    {
                                        case KTYD.Model.ItemType.SHOTGUN_WEAPON:
                                            e.HoldItem = KTYD.Model.ItemType.SHOTGUN_WEAPON;
                                            trashContainers.Add(u);
                                            //SoundEffect[4].Play();
                                            break;

                                        case KTYD.Model.ItemType.FLAME_WEAPON:
                                            e.HoldItem = KTYD.Model.ItemType.FLAME_WEAPON;
                                            trashContainers.Add(u);
                                            //SoundEffect[6].Play();
                                            break;

                                        case KTYD.Model.ItemType.M16_WEAPON:
                                            e.HoldItem = KTYD.Model.ItemType.M16_WEAPON;
                                            trashContainers.Add(u);
                                            //SoundEffect[3].Play();
                                            break;

                                        case KTYD.Model.ItemType.HEALTH:
                                            if (e.Health + 25 <= 100)
                                            {
                                                e.Health += 25;
                                            }
                                            else
                                            {
                                                e.Health = 100;
                                            }
                                            //SoundEffect[5].Play();
                                            trashContainers.Add(u);
                                            break;

                                        default:

                                            break;
                                    }
                                }
                                else
                                {
                                    e.restorePrevLocation();
                                }
                            }
                        }
                    }// for each
                    return;
                    /*
                case EntityType.BULLET:
                    e.updateActions(this);
                    //((KTYD.Model.Bullet)e).updateActions(this.containers, this.trashContainers, this.AIDirector);


                    return;

                case EntityType.ENEMY:

                    e.updateActions(this);


                    return;
                */
                default:
                    e.updateActions(this);
                    return;

            }

        }

        /// <summary>
        /// Reuse bullet
        /// </summary>
        /// <returns></returns>
        private KTYD.Model.Bullet reuseBullet()
        {
            int i = 0;
            KTYD.Model.Bullet temp = null;
            while (i < trashContainers.Count)
            {
                if (this.trashContainers[i].Type == EntityType.BULLET)
                {
                    temp = (KTYD.Model.Bullet)this.trashContainers[i];
                    this.trashContainers.Remove(this.trashContainers[i]);
                    return temp;

                }
            }
            return null;

        }


        /// <summary>
        /// Clean up the bullets from the  main list
        /// </summary>
        private void cleanUpBullets()
        {
            // Remove trash from the list
            if (trashContainers.Count > 0)
            {
                foreach (Entity u in trashContainers)
                {
                    if (containers.Contains(u))
                    {
                        containers.Remove(u);
                    }
                }
            }
        }


        /// <summary>
        /// Remove dead bodies from the main list
        /// </summary>
        private void cleanUpDeadBodies()
        {
            // Remove dead body from the list
            if (deadContainers.Count() > 0)
            {
                foreach (Entity u in deadContainers)
                {
                    if (containers.Contains(u))
                    {

                        if (u.Type != EntityType.PLAYER)
                        {
                            containers.Remove(u);
                            enemiesList.Remove(u);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Checking if an entity is a dead bullet
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>True if it is a dead bullet</returns>
        private bool isDeadBullet(Entity e)
        {
            return e.isDead() && e.Type == EntityType.BULLET;
        }


        /// <summary>
        /// Update game
        /// </summary>
        private void updateGame()
        {

            int entityCount = 0;
            cleanUp();
            this.containers.updateGridLocs();
           List<Entity> entities=this.allEntities.allEntities();




           foreach (Entity e in entities)
                {
                    entityCount++;

                    e.updateGameTime();
                    if (isDeadBullet(e))
                    {
                        trashContainers.Add(e);
                    }
                    // other entity that is not a bullet
                    if (e.isDead() && (e.Type != EntityType.BULLET && e.Type != EntityType.PLAYER))
                    {
                        deadContainers.Add(e);
                    }


                    updateActionForEntity(e);

                    e.update();
           
            }//for each enitty

           

            if (enemiesList.Count() < 10 - this.playersList.getPlayers().Count)
            {
                AIDirector.addEnemiesToMap(this);
            }

            // for all bullet spawns
            if (bulletList.Count > 0)
            {
                foreach (Bullet b in bulletList)
                {

                    this.containers.Add(b);
                }
                // this.containers.AddRange(bulletList);   // Add bullet to main list
                bulletList.Clear(); // Clear bullet list
            }

            cleanUpBullets();
            cleanUpDeadBodies();

            this.notify();  // Notify all observers of changes
        }


        /// <summary>
        /// Update game
        /// </summary>
        public void update()
        {
            updateGame();
        }





        
    }
}
