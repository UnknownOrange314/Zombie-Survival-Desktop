using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KTYD.View
{
    class View:KTYD.ObserverModel.Observer
    {
        // graphical container
        private SpriteSheet unitSheet;
        private SpriteSheet utilSheet;
        private SpriteSheet bgSheet;
        private SpriteBatch screenBuffer;

        private SpriteFont font;
        private String strTxt;

        // Optimization variables
        private Rectangle tempDrawarea;
        private Vector2 tempCenterDrawnEntity;

        private Rectangle tempStretch;
        private Rectangle tempStretch_Health;
        // Camera variables
        private Vector3 scaleVector;
        private Vector3 centerScreenVector;
        private Vector3 moveCameraVector;
        private Viewport titleSafe;
        
        // Optimization values
        private Vector2 averagePlayerLoc;

        private int width, height;
        private List<textBuffer> displayStrings;
        private textBuffer scoreDisplay;

        private List<BarGraph> healthBars;
        // Model content
        private KTYD.Model.Map gameMap;

        

        /// <summary>
        /// Default Constructor
        /// </summary>
        public View()
        {
            healthBars = new List<BarGraph>();
            displayStrings = new List<textBuffer>();
            scoreDisplay=new textBuffer(new Vector2(300,10),"j");
            displayStrings.Add(scoreDisplay);
   


            tempDrawarea = new Rectangle();
            tempDrawarea.Width = GameConfig.ENTITY_WIDTH;
            tempDrawarea.Height = GameConfig.ENTITY_HEIGHT;
            tempCenterDrawnEntity = new Vector2(GameConfig.CENTER_ENTITY, GameConfig.CENTER_ENTITY);
            tempStretch = new Rectangle();
            tempStretch_Health = new Rectangle();
            scaleVector = new Vector3(1, 1, 0);
            centerScreenVector = new Vector3(400, 300, 0);
            moveCameraVector = new Vector3(0, 0, 0);
            averagePlayerLoc = new Vector2();

        }

        /// <summary>
        /// Set camera width and height
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public void setCameraDimension(int width, int height)
        {
            this.width = width;
            this.height = height;
            centerScreenVector.X = width/2;
            centerScreenVector.Y = height/2;
        }

        /// <summary>
        /// Load screen buffer
        /// </summary>
        /// <param name="buffer">spriteBatch</param>
        public void loadScreenBuffer(SpriteBatch buffer)
        {
            screenBuffer = buffer;
        }

        /// <summary>
        /// Load spritesheet for entitiy
        /// </summary>
        /// <param name="sheet"></param>
        public void loadUnitSpriteSheet(SpriteSheet sheet)
        {
            unitSheet = sheet;
        }

        /// <summary>
        /// Load spritesheet for misc. and utilitiy graphic
        /// </summary>
        /// <param name="sheet"></param>
        public void loadUtilSpriteSheet(SpriteSheet sheet)
        {
            utilSheet = sheet;
        }

        /// <summary>
        /// Load a game map for this view
        /// </summary>
        /// <param name="newMap">Map object</param>
        public void loadMap(KTYD.Model.Map newMap)
        {
            gameMap = newMap;

            //Load barGraphs
            for (int i = 1; i < gameMap.PlayerEntities.Count() + 1; i++)
            {
                BarGraph b1;
                if (i >= 3)
                {
                     b1 = new BarGraph(new Vector2(100 * i - 50, 60), 200, 2);
                }
                else
                {
                     b1 = new BarGraph(new Vector2(100 * i - 50, 10), 200, 2);
                }

                healthBars.Add(b1);
            }

        }

        /// <summary>
        /// Load background of this map
        /// </summary>
        /// <param name="sheet"></param>
        public void loadBackground(SpriteSheet sheet)
        {
            bgSheet = sheet;
        }


        /// <summary>
        /// Load a font for debugging screen
        /// </summary>
        /// <param name="font">spritefont object</param>
        public void loadFont(SpriteFont font)
        {
            this.font = font;
        }

        /// <summary>
        /// Load viewport for titlesafe
        /// </summary>
        /// <param name="port">ViewPort object</param>
        public void loadViewPort(Viewport port)
        {
            this.titleSafe = port;
        }

        /// <summary>
        /// Draw all entities on the screen
        /// </summary>
        public void DrawEntities()
        {
            int player_index = 0;
            gArray allEntities = this.gameMap.allEntities;
            allEntities.startIteration();
            while (allEntities.hasNext())
            {
                List<Entity> localEntities = allEntities.returnNext();

                foreach (Entity e in localEntities)
                {
                    tempDrawarea.X = (int)e.Location.X;
                    tempDrawarea.Y = (int)e.Location.Y;

                    if (e.Type == EntityType.PLAYER)
                    {
                        unitSheet.drawAtIndex(player_index, GameConfig.IMG_SELECT_PLAYER, tempDrawarea, e.Rotate, tempCenterDrawnEntity);
                        ++player_index;
                        //strTxt = strTxt + "\n" + e.Type + ":" + e.Location /*+ ":" + e.GameTime + ": Rotate "+ e.Rotate*/ + " >> T: " + e.Center + ":" + e.CurrentState + " >> " + e.Health;
                    }
                    unitSheet.drawAtIndex(e.graphicState, e.graphicEntityState, tempDrawarea, e.Rotate, tempCenterDrawnEntity);
                }
                //strTxt = strTxt + "\n" + e.Type + ":"+ e.Location /*+ ":" + e.GameTime + ": Rotate "+ e.Rotate*/ + " >> T: " + e.Center + ":" + e.CurrentState + " >> " + e.Health ;
            }
        }

        /// <summary>
        /// Draw all dead entities on the screen
        /// </summary>
        public void DrawDeadEntities()
        {
            /*
            if (gameMap.allDeadEntities.Count > 0)
            {
                foreach (Entity e in this.gameMap.allDeadEntities)
                {
                    tempDrawarea.X = (int)e.Location.X;
                    tempDrawarea.Y = (int)e.Location.Y;
                    unitSheet.drawAtIndex(e.graphicState, e.graphicEntityState, tempDrawarea, e.Rotate, tempCenterDrawnEntity);
                }
            }
             */
        }

        /// <summary>
        /// Draw User interface
        /// </summary>
        private void DrawUserInterface()
        {
        
            
            for(int i=0;i<healthBars.Count();i++)
            {

                healthBars[i].draw(unitSheet,gameMap,tempStretch,tempStretch_Health,this.gameMap.PlayerEntities[i]);
            }
          
        }

      
        public void displayString(String text,Vector2 location)
        {
            strTxt = text;
        }

        private void flushString()
        {
            strTxt = null;
        }

        /// <summary>
        /// Display debugging screen
        /// </summary>
        /// 
        public void DrawDebug()
        {
            foreach (textBuffer b in displayStrings)
            {
                String strTxt = b.getText();
                Vector2 bufferLoc=b.getLocation();
           
                if (strTxt != null && strTxt.Count() > 0)
                {
                    centerScreenVector.X = width / 2;
                    centerScreenVector.Y = height / 2;
                    screenBuffer.DrawString(font, strTxt, bufferLoc, Color.Black);
                }
            }
            
        }
        public void DrawString(String s,Vector2 drawLoc)
        {

          
        }

        /// <summary>
        /// Draw background
        /// </summary>
        private void DrawBackground()
        {
            bgSheet.drawAtCurrentIndex(Vector2.Zero);
        }


        /// <summary>
        /// Return the average locations of the players
        /// </summary>
        /// <returns>vector2</returns>
        private Vector2 avergePlayerLocs()
        {
            float x = 0;
            float y = 0;

            for (int i = 0; i < GameConfig.MAX_PLAYERS; i++)
            {
                x = x + gameMap.PlayerEntities[i].Center.X;
                y = y + gameMap.PlayerEntities[i].Center.Y;
            }
            this.averagePlayerLoc.X = x / GameConfig.MAX_PLAYERS;
            this.averagePlayerLoc.Y = y / GameConfig.MAX_PLAYERS;

            return this.averagePlayerLoc;

        }


        /// <summary>
        /// [HACK HACK HACK]
        /// Return a camera transformation value in term of Matrix
        /// </summary>
        /// <returns>matrix</returns>
        private Matrix cameraTransition()
        {
            // Using View port to find screen bound so that we can display the UI health bar and weapon
            // Fix this and find formula for balancing view between all four players

            int totalLives = 0;
            Entity temp = null;

            Vector2 centerLoc = this.avergePlayerLocs();

            for (int x = 0; x < GameConfig.MAX_PLAYERS; x++)
            {
                if (!gameMap.PlayerEntities[x].isDead())
                {
                    ++totalLives;
                    temp = gameMap.PlayerEntities[x];
                }
                Entity e = this.gameMap.PlayerEntities[x];

                float xDev = (e.Center.X - centerLoc.X);
                float yDev = (e.Center.Y - centerLoc.Y);

                /*
                if (Math.Abs(xDev) > 400 || Math.Abs(yDev) > 300)
                {

                    e.setLocation(centerLoc.X, centerLoc.Y);
                }
                 */

      
                if (xDev > titleSafe.Width/2)
                {
                    e.setLocation(centerLoc.X + 395, e.Center.Y);
                }
                if (xDev < -titleSafe.Width/2)
                {
                    e.setLocation(centerLoc.X - 395, e.Center.Y);

                }

                if (yDev > titleSafe.Height/2)
                {
                   e.setLocation(e.Center.X, centerLoc.Y + 245);

                }
                if (yDev < -titleSafe.Height/2)
                {
                    e.setLocation(e.Center.X, centerLoc.Y - 245);


                }

            }

    
       
            scoreDisplay.setDrawLocation(new Vector2(centerLoc.X - width/2+scoreDisplay.screenLoc().X, centerLoc.Y-height/2 + scoreDisplay.screenLoc().Y));
          
            if (totalLives == 0)
            {
                return Matrix.CreateTranslation(new Vector3(-this.width/2, -this.height/2, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(scaleVector) *
                                         Matrix.CreateTranslation(centerScreenVector);
            } else


            if (totalLives < 2)
            {
                return Matrix.CreateTranslation(new Vector3(-temp.Center.X, -temp.Center.Y, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(scaleVector) *
                                         Matrix.CreateTranslation(centerScreenVector);
            }
            else
            {

                return Matrix.CreateTranslation(new Vector3(-centerLoc.X, -centerLoc.Y, 0)) *
                                             Matrix.CreateRotationZ(0) *
                                             Matrix.CreateScale(scaleVector) *
                                             Matrix.CreateTranslation(centerScreenVector);
            }
            

            /* In case we need it, if not DEPRECATED
            return Matrix.CreateTranslation(new Vector3(-this.gameMap.allEntities[0].Location.X, -this.gameMap.allEntities[0].Location.Y, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(scaleVector) *
                                         Matrix.CreateTranslation(centerScreenVector);
            */
        }

        /// <summary>
        /// Draw
        /// </summary>
        public void Draw()
        {
            screenBuffer.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend, null, null, null, null, cameraTransition());
   
                DrawBackground();
                DrawDeadEntities();
                DrawEntities();
                //DrawUserInterface();
                DrawDebug();
            screenBuffer.End();
            screenBuffer.Begin();
                DrawUserInterface();
            screenBuffer.End();
            flushString();
       
        }
        public void setScore(int i)
        {
            strTxt = i.ToString();
            scoreDisplay.setString("score:"+strTxt);
            //screenBuffer.DrawString(font, "ffff", new Vector2(200, 10), Color.Black);

        }

        /// <summary>
        /// Update
        /// </summary>
        public void update()
        {
     
            //Draw();
        }
    }
}
