using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;

namespace KTYD
{
    class ControlConfig
    {
        public static Microsoft.Xna.Framework.Input.Keys PAUSE = Keys.P;
        public static Microsoft.Xna.Framework.Input.Keys RESPAWN = Keys.Enter;
        public static Microsoft.Xna.Framework.Input.Keys P1_SHOOT = Keys.Space;
        public static Microsoft.Xna.Framework.Input.Keys P1_FORWARD = Keys.W;
        public static Microsoft.Xna.Framework.Input.Keys P1_BACKWARD = Keys.S;
        public static Microsoft.Xna.Framework.Input.Keys P1_LEFT = Keys.A;
        public static Microsoft.Xna.Framework.Input.Keys P1_RIGHT = Keys.D;


        public static Microsoft.Xna.Framework.Input.Keys P2_SHOOT = Keys.Back;
        public static Microsoft.Xna.Framework.Input.Keys P2_FORWARD = Keys.Up;
        public static Microsoft.Xna.Framework.Input.Keys P2_BACKWARDS = Keys.Down;
        public static Microsoft.Xna.Framework.Input.Keys P2_LEFT = Keys.Left;
        public static Microsoft.Xna.Framework.Input.Keys P2_RIGHT = Keys.Right;
        public static Microsoft.Xna.Framework.Input.Keys RESUME = Keys.R;
        public static Microsoft.Xna.Framework.Input.Keys WEAPON_1 = Keys.F1;
        public static Microsoft.Xna.Framework.Input.Keys WEAPON_2 = Keys.F2;
        public static Microsoft.Xna.Framework.Input.Keys WEAPON_3 = Keys.F3;
        public static Microsoft.Xna.Framework.Input.Keys WEAPON_4 = Keys.F4;
    }
}
