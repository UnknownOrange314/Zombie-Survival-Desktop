using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KTYD
{
    //Base class for implementing GUI components
    class GUIComponent
    {
        Vector2 myScreenLoc;  //Location of element on screen(defined as top right corner
        Vector2 myDrawLoc;   //Location of element accoring to the game code
        public GUIComponent(Vector2 sLoc)
        {
            myScreenLoc = sLoc;
            myDrawLoc.X = sLoc.X;
            myDrawLoc.Y = sLoc.Y;
           
        }
        public Vector2 getLocation()
        {
            return myDrawLoc;
        }
        public void setDrawLocation(Vector2 v)
        {
            myDrawLoc = v;
        }


        public Vector2 screenLoc()
        {
            return myScreenLoc;
        }
        

    }
}
