
/* TODO
 * Look through code and find ways to refactor
 *Create a character class that extends entity.cs and have enemy.cs extend it
 *
 * Rewrite enemy class
    *  Keep enemies from getting stuck at an edge
 *  Have some sort of AI director method that helps the AI's coordinate strategy???????
 *  Have heuristic that makes AI chasing you to turn a certain amount of degreees based on how many AI are nearby.
 * Multiple gameTime entities for each entity????
 *  e.CenterLoc is giving weird values.
 *  Quadtrees.
 
*/


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
    public enum gameState
    {
        TITLE,
        TUTORIAL,
        PLAYMODE,
        GAMEOVER,
        CREDIT,
        CONTROL

    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Boolean pauseGame = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        float[] time;
        float storeTime;
        GamePadState[] controllerInput;
        GamePadState[] prevInput;
        Texture2D title, tutorial;
        gameState currentGameState = gameState.PLAYMODE;
        String mState = "playmode";
        GameState.MenuState myMenuState;

        List<String> timeData;


        bool[] pressShoot;
        KeyboardState input;
        Controller.Players gamePlayers;

        Entity[] players;

        KTYD.Model.Item item1, item2;
        View.View gameView;
        Model.Map gameMap;


        Dictionary<PlayerIndex, int> c;

        public Game1()
        {
            c = new Dictionary<PlayerIndex, int>();
            c.Add(PlayerIndex.One, 1);
            c.Add(PlayerIndex.Two, 2);
            c.Add(PlayerIndex.Three, 3);
            c.Add(PlayerIndex.Four, 4);
           

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 700;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            timeData = new List<String>();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            controllerInput = new GamePadState[GameConfig.MAX_PLAYERS];
            prevInput = new GamePadState[GameConfig.MAX_PLAYERS];
            players = new Entity[GameConfig.MAX_PLAYERS];
            time = new float[GameConfig.MAX_PLAYERS];
            pressShoot = new bool[GameConfig.MAX_PLAYERS];


       


            // TODO: Add your initialization logic here
            players[0] = new KTYD.Model.Character(500, 100, 0, EntityType.PLAYER, GameConfig.IMG_BASE_BLUE, Weapon.createPistol());
            players[1] = new KTYD.Model.Character(600,200,0, EntityType.PLAYER, GameConfig.IMG_BASE_RED, Weapon.createPistol());
            //players[2] = new Entity(700,200,0, EntityType.PLAYER, GameConfig.IMG_BASE_ORANGE, KTYD.Model.BulletType.PISTOL);
            //players[3] = new Entity(800,400,0, EntityType.PLAYER, GameConfig.IMG_BASE_GREEN, KTYD.Model.BulletType.PISTOL);


            item1 = new KTYD.Model.Item(100, 100, Model.ItemType.M16_WEAPON);

            item2 = new KTYD.Model.Item(100, 100, Model.ItemType.FLAME_WEAPON);

            /* Load VIEW */
            gameView = new View.View();
            gameView.setCameraDimension(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            GameConfig.SCREEN_RES_X = GraphicsDevice.Viewport.Width;
            GameConfig.SCREEN_RES_Y = GraphicsDevice.Viewport.Height;
            gameView.loadViewPort(GraphicsDevice.Viewport);


            gamePlayers = new Controller.Players();

            gameMap = new Model.Map(gamePlayers);
            gameMap.setDimension(1920, 1600);


            /* Load all players into map and player controller */
            for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
            {
                gameMap.loadEntity(players[i]);

            }

            // Initialize time
            for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
            {
                time[i] = 0;
                pressShoot[i] = false;
            }

            //gameMap.loadEntity(e1); // Load first enemy
            gameMap.loadEntity(item1);  // Load first item
            gameMap.loadEntity(item2);


            // Register observers to objects
            gameMap.register(gameView);
            gameMap.register(gamePlayers);

            // Load the map to the View
            gameView.loadMap(gameMap);

            if (mState.Equals("playmode"))
            {

                 myMenuState = new GameState.Playmode("playmode", gameMap, false, input, pressShoot, storeTime, time, gamePlayers,c, gameView);
             
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            title = Content.Load<Texture2D>("ktyd_01");
            tutorial = Content.Load<Texture2D>("ktyd_02");

            gameView.loadScreenBuffer(spriteBatch);
            gameView.loadUnitSpriteSheet(new SpriteSheet(Content.Load<Texture2D>("KTYD_units"), spriteBatch, 20, 16));
            gameView.loadBackground(new SpriteSheet(Content.Load<Texture2D>("KTYD_Map"), spriteBatch, 1600, 1200));
            font = Content.Load<SpriteFont>("fontdebug");
            gameView.loadFont(font);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        


        protected void keyboardControllerListener()
        {

        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (myMenuState.getName().Equals("playmode"))
            {
                ((GameState.Playmode)myMenuState).updateValues("playmode", gameMap, false, input, pressShoot, storeTime, time, gamePlayers, c);
            }

            Vector2 v = gamePlayers.getPlayerAt(0).Location;

            input = Keyboard.GetState();

            controllerInput[0] = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
            {
                //controllerInput[1] = GamePad.GetState(PlayerIndex.Two);
            }
         
            if (currentGameState == gameState.CONTROL)
            {
                pauseGame = true;


            }
            if (currentGameState == gameState.TITLE)
            {
                if (controllerInput[0].IsButtonDown(Buttons.Start) || input.IsKeyDown(Keys.Space))
                {

                    //gameMap.loadEntity(players[0]);
                    currentGameState = gameState.TUTORIAL;

                }

                //if (controllerInput[1].IsButtonDown(Buttons.Start))
                {

                    //gameMap.loadEntity(players[1]);

                }
         


            }

            else if (currentGameState == gameState.TUTORIAL)
            {
                if (controllerInput[0].IsButtonDown(Buttons.Start) && prevInput[0].IsButtonUp(Buttons.Start)
                    || (input.IsKeyDown(Keys.LeftAlt)))
                {


                    //gameMap.loadEntity(players[0]);
                    currentGameState = gameState.PLAYMODE;
                }
            }

         

            
     
                myMenuState.update(gameTime);



           
           
            // Keep track of previous buttons pressed by all users
            for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
            {
                prevInput[i] = controllerInput[i];
            }
            base.Update(gameTime);





        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (currentGameState == gameState.TITLE)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(title, new Rectangle(0, 0, GameConfig.SCREEN_RES_X, GameConfig.SCREEN_RES_Y), Color.White);
                spriteBatch.End();
            }

            if (currentGameState == gameState.TUTORIAL)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tutorial, new Rectangle(0, 0, GameConfig.SCREEN_RES_X, GameConfig.SCREEN_RES_Y), Color.White);
                spriteBatch.End();
            }

            myMenuState.draw();

          

            base.Draw(gameTime);
        }
    }
}
