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
    class Playmode: MenuState
    {

        float storeTime;
        float[] time;
        KTYD.Model.Map gameMap;
        Controller.Players gamePlayers;
        Boolean pauseGame = false;
        public GameTime gameTime;
        KeyboardState input;
        bool[] pressShoot;
        Dictionary<PlayerIndex, int> c;
        View.View gameView;
        public Playmode(String s, KTYD.Model.Map g, Boolean p, KeyboardState i, bool[] ps, float st, float[] t, Controller.Players gPlayers, Dictionary<PlayerIndex, int> test, View.View gView)
            : base(s)
        {
            c = test;
            gamePlayers = gPlayers;
            gameMap = g;
            pauseGame = p;
            input = i;
            pressShoot = ps;
            storeTime = st;
            time = t;
            gameView = gView;

        }
        public void updateValues(String s, KTYD.Model.Map g, Boolean p, KeyboardState i, bool[] ps, float st, float[] t, Controller.Players gPlayers, Dictionary<PlayerIndex, int> test)
        {
            c = test;
            gamePlayers = gPlayers;
            gameMap = g;
            pauseGame = p;
            input = i;
            pressShoot = ps;
            storeTime = st;
            time = t;
        }

        public override void draw()
        {
                gameView.Draw();
                gameView.setScore(gameMap.getScore());
           
                gameView.DrawString("hi", new Vector2(100, 100));
        }
        public override MenuState update(GameTime gameTime)
        {

            //System.Console.WriteLine("hJHHi");
         
            
                storeTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;  // Main gameTime counter

                // Update time for all players
                for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
                {
                    time[i] += storeTime;
                }


                gameMap.updateTime((float)gameTime.ElapsedGameTime.TotalMilliseconds);

                // Allows the game to exit


                //Receive input from each player's gamepad 
                if (gamePlayers.getPlayerAt(0) != null)
                {
                    //xBoxControllerListener(controllerInput[0], PlayerIndex.One);
                    keyboardControllerListener();
                }

                if (gamePlayers.getPlayerAt(1) != null)
                {
                    //(controllerInput[1], PlayerIndex.Two);
                }

                if (gamePlayers.getPlayerAt(2) != null)
                {
                    //xBoxControllerListener(controllerInput[2], PlayerIndex.Three);
                }
                if (gamePlayers.getPlayerAt(3) != null)
                {
                    //xBoxControllerListener(controllerInput[3], PlayerIndex.Four);
                }


                // Update all game logic
                if (pauseGame == false)
                {

          
                    gameMap.update();
                    gamePlayers.updateCooldowns(pressShoot, time);

                }

                if (input.IsKeyUp(Keys.Space))
                {
                    gamePlayers.getPlayerOne().setState(EntityState.NORMAL);
                }


            
            return this;
        }

      

        protected void keyboardControllerListener()
        {
           List<Entity> players = gamePlayers.getPlayers();
            if (pauseGame == false)
            {

                /*
                if (input.IsKeyDown(ControlConfig.P1_FORWARD))
                {
                  
                    Vector2 center = gamePlayers.getPlayerOne().Center;

                    gamePlayers.getPlayerOne().moveForward(1f);

                }

                if (input.IsKeyDown(ControlConfig.P1_BACKWARD))
                {
                    gamePlayers.getPlayerOne().moveBackward(1f);

                }

                if (input.IsKeyDown(ControlConfig.P1_LEFT))
                {
                    gamePlayers.getPlayerOne().moveRotateLeft(1f);

                }

                if (input.IsKeyDown(ControlConfig.P1_RIGHT))
                {
                    gamePlayers.getPlayerOne().moveRotateRight(1f);

                }
                */

                Dictionary<int, Microsoft.Xna.Framework.Input.Keys> rightMapping = gamePlayers.getRightMapping();
                for (int x = 0; x < 2; x++)
                {
                    Microsoft.Xna.Framework.Input.Keys r = rightMapping[x];
                    if (input.IsKeyDown(r))
                    {

                        Entity p = players[x];
                        p.moveRotateRight(1f);
                    }
                }

                Dictionary<int, Microsoft.Xna.Framework.Input.Keys> forwardMapping = gamePlayers.getForwardMapping();
                for (int x = 0; x < 2; x++)
                {
                    Microsoft.Xna.Framework.Input.Keys r = forwardMapping[x];
                    if (input.IsKeyDown(r))
                    {

                        Entity p = players[x];
                        p.moveForward(1f);
                    }
                }


                Dictionary<int, Microsoft.Xna.Framework.Input.Keys> backwardMapping = gamePlayers.getBackwardMapping();
                for (int x = 0; x < 2; x++)
                {
                    Microsoft.Xna.Framework.Input.Keys r = backwardMapping[x];
                    if (input.IsKeyDown(r))
                    {

                        Entity p = players[x];
                        p.moveBackward(1f);
                    }
                }
                Dictionary<int, Microsoft.Xna.Framework.Input.Keys> leftMapping = gamePlayers.getLeftMapping();
                for (int x = 0; x < 2; x++)
                {
                    Microsoft.Xna.Framework.Input.Keys r = leftMapping[x];
                    if (input.IsKeyDown(r))
                    {

                        Entity p = players[x];
                        p.moveRotateLeft(1f);
                    }
                }

                Dictionary<int, Microsoft.Xna.Framework.Input.Keys> shootMapping = gamePlayers.getShootMapping();

                //Allows players to shoot
                for (int x = 0; x < 2; x++)
                {
                    Microsoft.Xna.Framework.Input.Keys r = shootMapping[x];
                    if (input.IsKeyDown(r))
                    {

                        Entity p = players[x];
                        Weapon w = gamePlayers.getPlayerWeaponTypeAt(0);
                        gameMap.addBullet(p, w, EntityType.PLAYER);
                        pressShoot[x] = true;
                        p.setState(EntityState.ATTACK);

                    }
                }
           
                // for all weapons
                if (input.IsKeyDown(ControlConfig.P1_SHOOT) && !pressShoot[0])
                {
                    gameMap.addBullet(gamePlayers.getPlayerOne(), gamePlayers.getPlayerOneWeaponType(), EntityType.PLAYER);
                    pressShoot[0] = true;
                    gamePlayers.getPlayerOne().setState(EntityState.ATTACK);


                }

                // Only for framethrower
                if (input.IsKeyDown(ControlConfig.P1_SHOOT) && gamePlayers.getPlayerOneWeaponType().getType() == WeaponType.FLAMETHROWER)
                {
                    gameMap.addBullet(gamePlayers.getPlayerOne(), gamePlayers.getPlayerOneWeaponType(), EntityType.PLAYER);
                    gamePlayers.getPlayerOne().setState(EntityState.ATTACK);
                }


                if (input.IsKeyDown(ControlConfig.WEAPON_4))
                {
                    //soundEffects[0].Play();
                    gamePlayers.changeWeaponAtPlayer(0, Weapon.createFlamethrower());
                    gamePlayers.getPlayerOne().setStateEntity(GameConfig.IMG_OFFSET_FLAMETHROWER);
                }

                if (input.IsKeyDown(ControlConfig.WEAPON_3))
                {
                    gamePlayers.changeWeaponAtPlayer(0, Weapon.createShotgun());
                    gamePlayers.getPlayerOne().setStateEntity(GameConfig.IMG_OFFSET_SHOTGUN);
                }

                if (input.IsKeyDown(ControlConfig.WEAPON_2))
                {
                    gamePlayers.changeWeaponAtPlayer(0, Weapon.createM16());
                    gamePlayers.getPlayerOne().setStateEntity(GameConfig.IMG_OFFSET_M16);
                }

                if (input.IsKeyDown(ControlConfig.WEAPON_1))
                {
                    gamePlayers.changeWeaponAtPlayer(0, Weapon.createPistol());
                    gamePlayers.getPlayerOne().setStateEntity(GameConfig.IMG_OFFSET_PISTOL);
                }
                if (input.IsKeyDown(Keys.Enter))
                {
                    gamePlayers.resetPlayerAt(0);
                }
                if (input.IsKeyDown(ControlConfig.PAUSE))
                {
                    pauseGame = true;
                }
            }
            else
            {
                if (input.IsKeyDown(ControlConfig.RESUME))
                {
                    pauseGame = false;
                }
            }

        }
    }
}
