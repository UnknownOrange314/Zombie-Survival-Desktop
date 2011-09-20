using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace KTYD
{
    //Class for displaying bar graphs(ie players health)

    
    class BarGraph:GUIComponent
    {
       
        public float length; //length of graph in display
        public float height;

        
        public BarGraph(Vector2 dLoc,float l,float h):base(dLoc)
        {
          
            length = l;
            height = h;

        }
      
        public float getFillFrac(float frac)
        {

            return frac;
        }
        public void draw(SpriteSheet unitSheet,KTYD.Model.Map gameMap,Rectangle tempStretch,Rectangle tempStretch_Health,Entity e)
        {
            tempStretch.Width = 200;
            tempStretch.Height = 20;
            tempStretch.X = (int)(this.getLocation().X);
            tempStretch.Y = (int)(this.getLocation().Y);
            tempStretch_Health.Height = 18;
            tempStretch_Health.X = (int)(this.getLocation().X);
            tempStretch_Health.Y = (int)(this.getLocation().Y);
             tempStretch_Health.Width = ((e.Health) * tempStretch.Width - 2) / 100;

            unitSheet.drawAtIndex(GameConfig.IMG_BAR_BASE, GameConfig.IMG_BAR_BASE_ROW, tempStretch);
            unitSheet.drawAtIndex(GameConfig.IMG_BAR_HEALTH, GameConfig.IMG_BAR_BASE_ROW, tempStretch_Health);
        }
        


    }
     
}
