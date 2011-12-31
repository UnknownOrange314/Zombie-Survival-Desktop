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
    class Title : MenuState
    {

        public Title(gameState s)
            : base(s)
        {

        }
        public override MenuState update(GameTime g)
        {
            return this;
        }
        public override void draw()
        {
          
        }
    }
}
