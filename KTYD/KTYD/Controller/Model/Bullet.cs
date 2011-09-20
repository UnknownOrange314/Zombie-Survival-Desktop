using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.Model
{

    public enum BulletType
    {
        PISTOL,
        SHOTGUN,
        M16,
        FLAMETHROWER,
        BOMB
    }

    /// <summary>
    /// Bullet object
    /// Base: Entity
    /// 
    /// 
    /// </summary>
    public class Bullet : Entity
    {

        protected Microsoft.Xna.Framework.Vector2 spawnLoc;

        //protected BulletType bulletType;
        protected EntityType ownerOfBulletType;
        protected bool allowFriendFired;
        protected int attackValue;


        protected float speedValue;


        public EntityType Owner
        {
            get
            {
                return ownerOfBulletType;
            }
        }

       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="rotate">Rotational value radian</param>
        public Bullet(float x, float y, float rotate, EntityType owner,int a,float s,int h,int st)
            : base(
            (float)(x + 10f * Math.Cos(rotate)),
            (float)(y + Math.Sin(rotate) * 10f),
            (float)(rotate),
            EntityType.BULLET)
        {


            
            spawnLoc = new Microsoft.Xna.Framework.Vector2(Location.X, Location.Y);
            //bulletType = bullet_type;
            ownerOfBulletType = owner;
            allowFriendFired = false;
            stateEntityOffset = GameConfig.IMG_BULLET_ENTITY;

            attackValue = a;
            speedValue = s;
            health = h;
            state = st;

            calculateDestination();
            rangeRadius = GameConfig.DEFAULT_RANGE_RADIUS_BULLET;
        }
      
        /// <summary>
        /// Predefine value for each type of bullet
        /// </summary>
       

        /// <summary>
        /// Allow friendly fire (Be able to kill their own side)
        /// </summary>
        public void allowFriendlyFire()
        {
            allowFriendFired = true;
        }


        
        public void setOwnerType(EntityType p)
        {
            ownerOfBulletType = p;
        }
        /// <summary>
        /// deduct health from the target entity
        /// </summary>
        /// <param name="obj">target entity</param>
        public void hitTarget(Entity obj)
        {
            health = -1;
            if (!allowFriendFired && obj.Type == ownerOfBulletType)
            {
                return;
            }
            if (obj.Health >= 0)
            {
                obj.Health -= attackValue;
            }
        }

        /// <summary>
        /// Collision detection (override)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool isCollide(Entity e)
        {
            return base.isCollide(e);
        }


        /// <summary>
        /// Set to end of bullet's life
        /// </summary>
        public void setDie()
        {
            health = -1;
        }
        public void reuse(Entity e,Weapon w,EntityType t,List<Entity> bulletList,List<Entity> trashBullets)
        {
            this.setLocation(e.frontLoc.X, e.frontLoc.Y);
            this.setRotate(e.Rotate);
            //this.setBulletType(w);
            
            this.attackValue = w.getAttackValue();
            this.speedValue = w.getSpeedValue();
            this.health = w.getHealth();
            this.state = w.getState();
        
            trashBullets.Remove(this);
            bulletList.Add(this);
        }

        /// <summary>
        /// Update methods
        /// </summary>
        public override void update()
        {

            //if (findDistance(curLoc.X, curLoc.Y, (float)(spawnLoc.X + Math.Cos(this.Rotate)) * 2.5f, (float)(Math.Sin(this.Rotate) + spawnLoc.Y) * 2.5f) > 5)
            if (health >= 0)
            {
               
                moveForward(speedValue);
                --health;
            }
        }


        /// <summary>
        /// Determines what the bullet should do(taken from Map.cs)
        /// </summary>
        public override void updateActions(KTYD.Model.Map gameMap)
        {
            gArray gameEntities = gameMap.allEntities;
            List<Entity> trashContainers = gameMap.getTrashContainers();
            DiffScaler AIDirector = gameMap.diffScaler();
            List<Entity> nearbyEntities=gameEntities.getNearbycontainers(this);
 
                foreach (Entity u in nearbyEntities)
                {
                    if (this != u)
                    {
                        if (this.isCollide(u))
                        {
                            if (u.Type != EntityType.BULLET)
                            {
                                // if bullet is not already in the trash list
                                if (!trashContainers.Contains(this))
                                {
                                    trashContainers.Add(this);
                                    ((KTYD.Model.Bullet)this).hitTarget(u);
                                    ((KTYD.Model.Bullet)this).setDie();
                                    //SoundEffect[6].Play();
                                }

                                if (u.Type == EntityType.ENEMY && ((KTYD.Model.Bullet)this).Owner == EntityType.PLAYER)
                                {
                                    // add point
                                    AIDirector.addPoint();
                                }
                            }
                        }
                    
                }
            }//for each


        }

    }
}
