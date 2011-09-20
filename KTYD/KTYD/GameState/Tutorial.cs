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
    public class Tutorial:MenuState
    {
        GamePadState[] controllerInput;
        GamePadState[] prevInput;
        KeyboardState state;
        public Tutorial(GamePadState[] c, GamePadState[] p, KeyboardState i, String name)
            : base(name)
        {
            controllerInput = c;
            prevInput = p;
            state = i;

        }
        public override MenuState update(GameTime g)
        {
            return this;
        }

    }
}
