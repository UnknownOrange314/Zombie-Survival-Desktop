using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Xna.Framework;

namespace KTYD.Controller.Model
{

 

    
    //Obstacle class that blocks stuff off
    //PROBLEM, Obstacle needs to be added to multiple places in grid array.
    public class Obstacle:Entity
    {

        private int topX; //Top coordinates of obstacle
        private int topY;

        private int width;
        private int height;


        public Obstacle(int x,int y,int w, int h):base(x,y, 0f,EntityType.OBSTACLE)
        {
            topX = x;
            topY = y;
            width = w;
            height = h;

        }

        public override bool isCollide(Entity e)
        {

            Vector2 eLoc = e.Location;
          System.Console.WriteLine("Testing obstacle");
          System.Console.WriteLine(eLoc);
          System.Console.WriteLine(topX + ":" + topY + " " + topX + width + ":" + topY + height);
            if (eLoc.X>topX&&eLoc.X<topX+width)
            {
                return true;
                System.Console.WriteLine("hi");
            }
            if (eLoc.Y > topY && eLoc.Y < topY + height)
            {
                System.Console.WriteLine("hi");
                return true;
            }
            return false;
        }
    }
}
