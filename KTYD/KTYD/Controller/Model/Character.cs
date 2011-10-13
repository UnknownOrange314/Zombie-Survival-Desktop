
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KTYD.Model
{
    public class Character:Entity
    {
        public Character(float x, float y, float rotate, EntityType new_type):base(x,y,rotate,new_type)
        {
  
         
        }

 
        public Character(float x, float y, float rotate, EntityType new_type, int new_graphicalEntityState, Weapon startingWeapon)
            : base(x,y,rotate,new_type,new_graphicalEntityState,startingWeapon)
        {
           
        }
        public void setRotate(float f)
        {
            curRotate = f;
        }
        /// <summary>
        /// Go to rotate to the opposite direction
        /// </summary>
        public void invertRotation()
        {
            this.curRotate = this.curRotate + MathHelper.Pi;
        }


        /// <summary>
        /// Move forward
        /// </summary>
        public void moveForward(float speed)
        {
            move(speed);
        }
        /// <summary>
        /// Move backward
        /// </summary>
        public void moveBackward(float speed)
        {
            move(-speed);
        }


        /// <summary>
        /// Rotating to the left
        /// </summary>
        public void moveRotateLeft(float value)
        {
            curRotate -= GameConfig.ENTITY_ROTATE_SPEED * value;


            move(0); ////Im assuming that this is for collision detection?
            //move(0.25f);
        }

        /// <summary>
        /// Rotating to the right
        /// </summary>
        public void moveRotateRight(float value)
        {
            curRotate += GameConfig.ENTITY_ROTATE_SPEED * value;


            move(0); ///Im assuming that this is for collision detection?

        }


        /// <summary>
        /// Save previous location
        /// </summary>
        private void savePrevLocation()
        {
            prevLoc.X = curLoc.X;
            prevLoc.Y = curLoc.Y;
        }

        /// <summary>
        /// Generic Move
        /// </summary>
        /// <param name="speed"></param>
        private void move(float speed)
        {

            //Testing hack to see if obstacles work


            //End testing hack
            curVarSpeed = speed;
            savePrevLocation();
            calculateDestination();  //This calculates the destination of the part of the entity corresponding to the direction it is moving in



            if (edgeCollisions(speed))
            {

                restorePrevLocation();
            }
            else
            {

                curLoc.X -= (float)(Math.Cos(Rotate) * (rangeRadius));
                curLoc.Y -= (float)(Math.Sin(Rotate) * (rangeRadius));
            }

            if (curLoc.X < 0 || curLoc.Y < 0)
            {

            }


        }

    }
}
