using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Xna.Framework;

namespace KTYD.AI
{
    /// <summary>
    /// Enemy class (Artificial Intelligent)
    /// 
    /// Base: Entity
    /// </summary>
    public class Enemy : KTYD.Model.Character
    {
        private int countSteps;         // current step used
        private int maxSteps;           // max steps
        public bool alreadyShot;       // if already shoot

        private EntityState prevState;
        protected Weapon myWeapon;


        protected Entity currentTarget;
        // Random number generator
        protected Random rand;


        private AI_State myState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rotate"></param>
        /// <param name="graphicRow">based graphical representation of this enemy (row frame)</param>
        /// <param name="weaponType">initial weapon</param>
        public Enemy(float x, float y, float rotate, int graphicRow, Weapon weapon, EntityState startState)
            : base(x, y, rotate, EntityType.ENEMY, graphicRow, weapon)
        {
            curState = startState;
            prevState = curState;
            this.myWeapon = weapon;
            alreadyShot = false;
            this.health = 5;
            maxSteps = 100;
            countSteps = maxSteps;
            rand = new Random();
        }

        /// <summary>
        /// Weapon that this AI holds.
        /// </summary>
        public Weapon Weapon
        {
            get { return myWeapon; }
        }

        /// <summary>
        /// Indicator whether that the bullet has been shot
        /// </summary>
        public bool AlreadyShot
        {
            get { return this.alreadyShot; }
            set { this.alreadyShot = value; }
        }

        public Weapon getWeapon()
        {
            return myWeapon;

        }
        /// <summary>
        /// Checking if within sight
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool isCollide(Entity e)
        {
            return base.isCollide(e);
        }

        /// <summary>
        /// Determine action of target
        /// </summary>
        /// <param name="e">Target</param>
        public void determineActionForTarget(Entity e,KTYD.Model.Map myMap)
        {
            if (e == null)
            {
                
                myState = new GuardState("GUARD",this,myMap);
                
                return;
            }
            if (!e.isDead())
            {
                if ((isPlayerWithinCloseRanged(e)))
                {
                    findPlayer(e);
                    myState = new AttackState("ATTACK", this, myMap);
    
                }
                else if ((isPlayerWithinSight(e)))
                {
                    findPlayer(e);
                    myState = new SearchState("SEARCH",this,myMap);
               
                }
                else
                {
                    myState = new GuardState("GUARD",this,myMap);
               
                }
            }
            else
            {
                if (!isDead())
                {
                    myState = new GuardState("GUARD", this, myMap);
                    //setState(prevState);
                }
            }
            /*
            
            if (e == null)
            {


                setState(EntityState.GUARD);
                return;
            }
            if (!e.isDead())
            {
                if ((isPlayerWithinCloseRanged(e)))
                {
                    findPlayer(e);
                    setState(EntityState.ATTACK);
                }
                else if ((isPlayerWithinSight(e)))
                {
                    findPlayer(e);
                    setState(EntityState.SEARCH);
                }
                else
                {
                    setState(EntityState.GUARD);
                }
            }
            else
            {
                if (!isDead())
                {
                    setState(prevState);
                }
            }
             */

        }

        /// <summary>
        /// Resolve collision conflict with movable entity
        /// </summary>
        /// <param name="e"></param>
        public void resolveCollision(Entity e)
        {
            curRotate = (0.25f + rand.Next() % MathHelper.Pi /*MathHelper.Pi*/) % MathHelper.TwoPi;
            moveBackward();
        }

        private void moveBackward()
        {
            moveBackward(0.75f);
        }

        private void moveForward()
        {
            moveForward(0.75f);
        }

        /// <summary>
        /// Determine if player within sight
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>Bool if within sight</returns>
        private bool isPlayerWithinSight(Entity e)
        {
            return checkWithInRadius(e, GameConfig.DEFAULT_RANGE_RADIUS_ENEMYSIGHT);
        }

        /// <summary>
        /// Helper function to determine if target entity is within radius
        /// </summary>
        /// <param name="e">target</param>
        /// <param name="radius">radius</param>
        /// <returns>True if within radius</returns>
        private bool checkWithInRadius(Entity e, int radius)
        {
            setCollisionRange(radius);
            bool iscollide = base.isCollide(e);
            setCollisionRange(GameConfig.DEFAULT_RANGE_RADIUS);
            return iscollide;
        }

        /// <summary>
        /// Determine if player is really closed to this AI unit
        /// </summary>
        /// <param name="e">Player entity</param>
        /// <returns>True if player is really closed to the point that you can smell him/her.</returns>
        private bool isPlayerWithinCloseRanged(Entity e)
        {
            return checkWithInRadius(e, GameConfig.DEFAULT_RANGE_RADIUS_ENEMYSMELL);
        }

        /// <summary>
        /// Finding where player is located (radius)
        /// </summary>
        /// <param name="e">Player entity</param>
        private void findPlayer(Entity e)
        {
            // find radius where player is located
            this.curRotate = (float)Math.Atan2(e.Location.Y - this.Location.Y, e.Location.X - this.Location.X);


            currentTarget = e;  // set target
        }

        /// <summary>
        /// Find the closest player
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>Return closets player or Null if nothing</returns>
        public Entity findClosestPlayer(List<KTYD.Model.Character> e)
        {
            Entity temp = null;
            Double min_dist = findDistance(this.Location.X, this.Location.Y, e[0].Location.X, e[0].Location.Y);
            foreach (Entity u in e)
            {
                if (isPlayerWithinSight(u))
                {
                    if (min_dist >= findDistance(this.Location.X, this.Location.Y, u.Location.X, u.Location.Y))
                    {
                        min_dist = findDistance(this.Location.X, this.Location.Y, u.Location.X, u.Location.Y);
                        temp = u;
                    }
                }
            }
            return temp;
        }


        /// <summary>
        /// Update game time
        /// </summary>
        public override void updateGameTime()
        {
            base.updateGameTime();


       
                    if (GameTime > myWeapon.getCooldown())
                    {
                        AlreadyShot = false;
                        GameTime = 0;
                    }
            
        }

        


     



        /// <summary>
        /// Update method refactored from Map.cs
        /// </summary>
        public override void updateActions(KTYD.Model.Map gameMap)
        {
            

            gArray containers = gameMap.getContainers();
            List<Entity> trashContainers = gameMap.getTrashContainers();
            List<KTYD.Model.Character> playersList = gameMap.getPlayersList();
            List<Entity> bulletList = gameMap.getBulletList();

            if (!this.isDead())
            {
                
                //Writing a hack here to prevent the enemies from infinitely looking at edge did not work
                if (1 == 2)
                {
                }

              
                else
                {

                    List<Entity> nearbyContainers = containers.getNearbycontainers(this);
                    foreach (Entity u in nearbyContainers)
                    {
                        if (this != u)
                        {
                            if (this.isCollide(u))
                            {
                                if (u.Type == EntityType.BULLET)
                                {
                                    // If bullet is actually hitting entity
                                    if (u.isCollide(this))
                                    {
                                        trashContainers.Add(u);
                                        ((KTYD.Model.Bullet)u).hitTarget(this);
                                        ((KTYD.Model.Bullet)u).setDie();
                                    }
                                }
                                else if (u.Type == EntityType.ENEMY)
                                {
                                    ((KTYD.AI.Enemy)u).resolveCollision(this);
                                }
                                else
                                {
                                    this.restorePrevLocation();
                                }
                            }
                            else
                            {
                                if (u.Type == EntityType.PLAYER)
                                {
                                    ((KTYD.AI.Enemy)this).determineActionForTarget(((KTYD.AI.Enemy)this).findClosestPlayer(playersList),gameMap);
                                }

                            }
                        }
                    }// for each
                }//offedge
            }
            
            

            if (!isDead())
            {
                determineAction(trashContainers,playersList,bulletList,gameMap);
                
                // Runaway

            }
            base.update();

        }
        //Everything that the enemy does should be determined from here.

        public void search()
        {
          

        }
        public void determineAction(List<Entity> trashContainers, List<KTYD.Model.Character> playersList,List<Entity> bulletList,KTYD.Model.Map gameMap)
        {

            Entity e = findClosestPlayer(playersList);
            determineActionForTarget(e,gameMap);




            myState.update();

          
   


                search();
                


           
     

        }
       
         

        
    }
}
