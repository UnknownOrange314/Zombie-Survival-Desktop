using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD
{
    public static class GameConfig
    {

        public static int IMG_BULLET = 0;
        public static int IMG_BULLET_SHOTGUN = 1;
        public static int IMG_BULLET_FLAME = 2;

        public static int IMG_OFFSET_FLAMETHROWER = 0;
        public static int IMG_OFFSET_PISTOL = 1;
        public static int IMG_OFFSET_M16 = 2;
        public static int IMG_OFFSET_SHOTGUN = 3;

        public static int IMG_BASE_BLUE = 0;
        public static int IMG_BASE_RED = 4;
        public static int IMG_BASE_GREEN = 8;
        public static int IMG_BASE_ORANGE = 12;
        public static int IMG_BASE_PURPLE = 16;
        public static int IMG_BASE_GRAY = 20;

        public static int IMG_ITEM_ENTITY = 25;
        public static int IMG_ITEM_HEALTH = 0;
        public static int IMG_ITEM_M16 = 1;
        public static int IMG_ITEM_SHOTGUN = 2;
        public static int IMG_ITEM_FLAMETHROWER = 3;

        public static int IMG_FRAME_DEAD = 3;
        public static int IMG_SHOOT = 2;
        public static int IMG_RUN = 0;

        public static int IMG_BULLET_ENTITY = 24;
        public static int IMG_SELECT_PLAYER = 26;

        public static float CENTER_ENTITY = 10f;

        public static int DEFAULT_RANGE_RADIUS = 8;
        public static int DEFAULT_RANGE_RADIUS_BULLET = 8;
        public static int DEFAULT_RANGE_RADIUS_ITEM = 10;
        public static int DEFAULT_RANGE_RADIUS_ENEMYSIGHT = 300;
        public static int DEFAULT_RANGE_RADIUS_ENEMYSMELL = 100;

        public static int MAX_WEAPON_SLOT = 2;
        public static int WEAPON_SLOT_ONE = 0;
        public static int WEAPON_SLOT_TWO = 1;


        public static int ENTITY_WIDTH = 20;
        public static int ENTITY_HEIGHT = 16;

        public static float TIME_TICK = 60f;

        public static float TEN_MIN_IN_MILLISECONDS = 100f;

        public static int DEFAULT_HEALTH = 100;
        public static int DEFAULT_LIFE_GIVEN = 1;
        public static int IMG_BAR_BASE = 0;
        public static int IMG_BAR_HEALTH = 1;
        public static int IMG_BAR_BASE_ROW = 27;


        public static int SCREEN_RES_X = 800;
        public static int SCREEN_RES_Y = 600;
        public static int MAX_PLAYERS = 2;

        public static int LEVEL_WIDTH = 1600;
        public static int LEVEL_HEIGHT = 1200;
        public static float ENTITY_ROTATE_SPEED = 0.05f;

        public static int GRID_PARTITION_SIZE = 20; //Size of grids in gArray.cs

        public static int MAX_ENEMIES = 1000;
        public static int DIFF_SCALE_FACTOR = 1;  //Lower number=harder game
        public static int MAX_ENEMY_SPAWN = 1000;    //Controls how fast the enemies can spawn
        public static int MIN_SPAWN_DISTANCE = 100; //The minimum distance away from a player that enemies can spawn.

        public static float PLAYER_SPEED=3f;
        public static float PLAYER_TURN_SPEED = 1f;

    }
}
