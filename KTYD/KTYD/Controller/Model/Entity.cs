
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KTYD
{
    public enum EntityType
    {
        PLAYER,
        BULLET,
        ENEMY,
        GENERIC,
        ITEM
    }

    public enum EntityState
    {
        NORMAL,
        DEAD,
        ATTACK,
        /* Below state for AI only */
        SEARCH,
        GUARD,
        ESCAPE
    }

    /// <summary>
    ///  Entity Class Object 
    ///     The base class for all the game's objects(this description needs to be better)
    /// 
    /// 
    ///     Revision 03/01/11 : Convert to observer-pattern. Making this object observable (subject).
    ///     Revision 11/09/10 : Bounding sphere collision detection and add rotation, center.
    /// 
    ///     Revision 11/6/10 : Added move(float x, float y) method. There may be some compile errors. For some reason, my computer won't let debug.
    ///                        Added getter and setter methods
    ///                        
    ///     
    /// </summary>
    public class Entity : KTYD.Model.Observable
    {
        // Location
        protected Vector2 curLoc;                              // Current location of this entity
        protected Vector2 prevLoc;                            // Previous Location

        // Rotation
        protected Vector2 curCenter;                           // Center location of this entity
        protected float curRotate;                             // Rotational value
        protected int rangeRadius;                              // Range for collision detection


        // Velocity
        protected float curVarSpeed;                           // Variant speed

        // Graphical reference
        protected int state;                                // State (column frame)
        protected int stateEntityOffset;                   // entity state (row frame)
        protected int baseStateEntity;                     // Starting row (Pistol)


        // game contents
        protected int health;                                 // Health 
        protected EntityType type;                            // Type of unit
        protected EntityState curState;                       // Current state
        protected float gameTime;
        protected KTYD.Model.ItemType curItem;

      

        /// <summary>
        /// Consturctor
        /// </summary>
        /// <param name="x">X-Location</param>
        /// <param name="y">Y-Location</param>
        /// <param name="rotate">direction facing in Radian</param>
        public Entity(float x, float y, float rotate, EntityType new_type)
        {
  
            type = new_type;
            curLoc = new Vector2(x, y);
            prevLoc = new Vector2(x, y);
           
            curVarSpeed = 0f;
            curRotate = rotate;
            health = GameConfig.DEFAULT_HEALTH; ;
            rangeRadius = GameConfig.DEFAULT_RANGE_RADIUS;
            gameTime = 0;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rotate"></param>
        /// <param name="new_type"></param>
        /// <param name="new_graphicalEntityState">base graphical entity state (starting Row)</param>
        public Entity(float x, float y, float rotate, EntityType new_type, int new_graphicalEntityState, Weapon startingWeapon)
            : this(x, y, rotate, new_type)
        {
            baseStateEntity = new_graphicalEntityState;

            stateEntityOffset = startingWeapon.getEntityOffset();
            

            stateEntityOffset = baseStateEntity + stateEntityOffset;
        }

        /// <summary>
        /// Type of entity
        /// </summary>
        public EntityType Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Center of the entity
        /// </summary>
        public virtual Vector2 Center  //Fixed a bug here
        {
            get
            {
              
  
                return curLoc;
            }
        }

        /// <summary>
        /// set Location 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setLocation(float x, float y)
        {
            this.curLoc.X = x;
            this.curLoc.Y = y;
        }

        /// <summary>
        /// Column frame represent this entity
        /// </summary>
        public int graphicState
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// Row frame represent this entity
        /// </summary>
        public int graphicEntityState
        {
            get
            {
                return stateEntityOffset;
            }
        }

        /// <summary>
        /// Entity's location
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return curLoc;
            }
        }

        public Vector2 frontLoc
        {
            get
            {
               
                return curLoc;
                
            }
        }


        /// <summary>
        /// Current holding items
        /// </summary>
        public KTYD.Model.ItemType HoldItem
        {
            get
            {
                return curItem;
            }

            set
            {
                this.curItem = value;
            }
        }


        /// <summary>
        /// Health of this entity
        /// </summary>
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Rotational value
        /// </summary>
        public float Rotate
        {
            get
            {
                return curRotate % (2 * MathHelper.Pi);
            }
        }


        /// <summary>
        /// Return the currentState of an entity
        /// </summary>
        public EntityState CurrentState
        {
            get
            {
                return curState;
            }
        }
       
        /// <summary>
        /// Makes sure there is no edge collision 
        /// This is probably a hack that should be refactored.
        /// </summary>
        public bool edgeCollisions(float varSpeed)
        {
            
            Vector2 frontLoc;
            frontLoc.X = this.Location.X + (rangeRadius +varSpeed)* (float)Math.Cos(Rotate);
            frontLoc.Y = this.Location.Y + (rangeRadius+varSpeed) * (float)Math.Sin(Rotate);

            if (frontLoc.X < 0)
            {
               
                return true;
                
            }
            if (frontLoc.Y < 0)
            {
               
                return true;
                
            }

            if (frontLoc.Y > GameConfig.LEVEL_HEIGHT)
            {


             
                return  true;
                

            }
            if (frontLoc.X > GameConfig.LEVEL_WIDTH)
            {
         
                return true;
                

            }
            return false;




        }



      

        /// <summary>
        /// process graphical state based on entity state
        /// </summary>
        protected void processState()
        {
            switch (curState)
            {
                case EntityState.NORMAL:
                    if (state < 1)
                    {
                        ++state;
                    }
                    else
                    {
                        state = GameConfig.IMG_RUN;
                    }
                    break;
                case EntityState.DEAD:
                    state = GameConfig.IMG_FRAME_DEAD;
                    break;
                case EntityState.ATTACK:
                    if (state == 2)
                    {
                        state = 1;
                    }
                    else
                    {
                        state = GameConfig.IMG_SHOOT;
                    }
                    break;

                default:
                    if (state < 1)
                    {
                        ++state;
                    }
                    else
                    {
                        state = GameConfig.IMG_RUN;
                    }
                    break;
            }
        }

       
        /// <summary>
        /// set a new state (column
        /// </summary>
        /// <param name="new_state"></param>
        public void setState(EntityState new_state)
        {
            this.curState = new_state;
        }

        /// <summary>
        /// Set row 
        /// </summary>
        /// <param name="new_state"></param>
        public void setStateEntity(int new_state)
        {
            this.stateEntityOffset = this.baseStateEntity + new_state;
        }

        /// <summary>
        /// Set column
        /// </summary>
        /// <param name="new_state"></param>
        public void setStateCol(int new_state)
        {
            this.state = new_state;
        }

        /// <summary>
        /// Calculating destination location
        /// </summary>
        protected void calculateDestination()
        {
            curLoc.X += (float)(Math.Cos(Rotate) * (curVarSpeed * 2.10+rangeRadius));
            curLoc.Y += (float)(Math.Sin(Rotate) * (curVarSpeed * 2.10+rangeRadius));

        }




        /// <summary>
        /// Restore previous location
        /// </summary>
        public void restorePrevLocation()
        {
            curLoc.X = prevLoc.X;
            curLoc.Y = prevLoc.Y;
        }


        /// <summary>
        /// Finding distance
        /// </summary>
        /// <param name="strX"></param>
        /// <param name="strY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        protected Double findDistance(float strX, float strY, float endX, float endY)
        {
            return Math.Sqrt(Math.Pow((strX - endX), 2) + Math.Pow((strY - endY), 2));
        }

        /// <summary>
        /// Set collision range value 
        /// </summary>
        /// <param name="radius">radius (multiple of 2 recommeded)</param>
        public void setCollisionRange(int radius)
        {
            rangeRadius = radius;
        }

        /// <summary>
        /// is colliding with the target object
        /// </summary>
        /// <param name="e">target</param>
        /// <returns>True if collides</returns>
        public virtual bool isCollide(Entity e)
        {
            Double distance = Math.Sqrt(Math.Pow((e.Location.X - this.curLoc.X), 2) + Math.Pow((e.Location.Y - this.curLoc.Y), 2));

            if (distance <= rangeRadius)
            {
                return true;
            }
            else return false;

        }


        /// <summary>
        /// Is Entity dead?
        /// </summary>
        /// <returns>True if it is, Otherwise, False is returned.</returns>
        public bool isDead()
        {
            if (this.health < 0)
            {
                curState = EntityState.DEAD;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Resurrect this entity
        /// </summary>
        public void resurrect()
        {
            this.health = GameConfig.DEFAULT_HEALTH;
            curState = EntityState.NORMAL;
        }

        /// <summary>
        /// Update game time 
        /// </summary>
        /// <param name="time">Total elpased game time</param>
        public virtual void updateGameTime()
        {
            gameTime += GameConfig.TIME_TICK;
        }


        /// <summary>
        /// GameTime
        /// </summary>
        public float GameTime
        {
            get
            {
                return gameTime;
            }
            set
            {
                gameTime = value;
            }
        }

        /// <summary>
        /// Update method for entity
        /// </summary>
        public virtual void update()
        {
            processState();
        }
        public virtual void updateActions(KTYD.Model.Map g)//This method should not be called
        {
     

        }

        /*
        /// <summary>
        /// Can an entity fire a bullet??
        /// </summary>
        /// <param name="loc">Location of entity</param>
        public void spawnBullet(Vector2 spawnLoc)
        {
            Vector2 frontLoc;
            frontLoc.X = curCenter.X + 10f * (float)Math.Cos(Rotate);
            frontLoc.Y = curCenter.Y + 10f * (float)Math.Cos(Rotate);
            if (canFire(frontLoc) == false)
            {
                return;

            }
            else
            {

            }

        }
        
        /// <summary>
        /// Can an entity fire a bullet??
        /// </summary>
        public Boolean canFire(Vector2 frontLoc)
        {
            
            if (frontLoc.X < 0 || frontLoc.X < 800)
            {
                return false;
            }
            if (frontLoc.Y < 0 || frontLoc.Y < 800)
            {
                return false;

            }
            return true;

        }
         */




    }
}
