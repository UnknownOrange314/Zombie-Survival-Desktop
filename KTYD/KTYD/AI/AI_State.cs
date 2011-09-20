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



namespace KTYD.AI
{

   
    public abstract class AI_State
    {

        protected String myName;
        protected KTYD.AI.Enemy myEntity;
        protected KTYD.Model.Map myMap;
        public AI_State(String m,KTYD.AI.Enemy e,KTYD.Model.Map mm)
        {
            myName = m;
            myEntity=e;
            myMap = mm;
        }


        public abstract void update();

    }
}
