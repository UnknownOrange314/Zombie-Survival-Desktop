using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KTYD
{
    class textBuffer:GUIComponent
    {
   
        String myText;
        public textBuffer(Vector2 drawLoc,String drawTxt):base(drawLoc)
        {
         
            myText = drawTxt;

        }
     
        public String getText()
        {
            return myText;
        }
        public void setString(String s)
        {
            myText = s;

        }
       
    }
}
