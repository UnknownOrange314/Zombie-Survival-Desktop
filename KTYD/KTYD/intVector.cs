using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD
{
    public class intVector
    {
        int myX;
        int myY;
        public intVector(int x, int y)
        {
            myX = x;
            myY = y;
        }

        //converts float locations into integer locations
        public intVector(float x, float y)
        {
            myX = (int)x;
            myY = (int)y;
        }
        public int getX()
        {
            return myX;

        }
        public int getY()
        {
            return myY;

        }

    }
}

