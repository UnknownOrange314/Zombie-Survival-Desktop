using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;



namespace KTYD.GameState
{
    public abstract class MenuState
    {
        private String name;

        public MenuState(String n)
        {
            name = n;
        }
     
        public String getName()
        {
            return name;
        }
        public Boolean Equals(MenuState b)
        {
            if (this.getName().Equals(b.getName()))
            {
                return true;
            }
            return false;
        }



        public abstract MenuState update(GameTime g);

        public abstract void draw();
        
      
    }
}
