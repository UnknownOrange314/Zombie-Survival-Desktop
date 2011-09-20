using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KTYD
{
    /// <summary>
    /// Spritesheet class object
    /// 
    /// This class will act as a wrapper and manager for texture2D. It will convert a spritesheet into 2D arrays of images.
    /// This class makes sure each frame's index will be corresponded to the correct image frame.
    /// </summary>
    public class SpriteSheet
    {
        private Texture2D sheet;                    // Texture2D image
        private SpriteBatch bufferScreen;           // Buffer Screen
        private int dimX, dimY;                     // Dimension of each frame
        private int curCol, curRow;                 // Current Colomn and row

        //private byte curAction;                     // Current action
        protected Vector2[,] location;              // Keep track of all frames' (X,Y) location
        public Vector2 currentFrame;                // Current frame

        public int frameDimX { get { return dimX; } }
        public int frameDimY { get { return dimY; } }

        public Vector2[,] Location { get { return this.location; } }

        public int Col
        {
            get { return (int)currentFrame.X; }
            set { currentFrame.X = (int)value; }
        }

        public int Row
        {
            get { return (int)currentFrame.Y; }
            set { currentFrame.Y = (int)value; }
        }


        /// <summary>
        /// Constructor
        /// This function is created for the sake of Nunit Testing. Do not use!
        /// </summary>
        /// <param name="dimX">Width of each frame</param>
        /// <param name="dimY">Height of each frame</param>
        public SpriteSheet(int dimX, int dimY)
        {
            this.dimX = dimX;
            this.dimY = dimY;
            this.currentFrame = Vector2.Zero;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">image spriteSheet</param>
        /// <param name="buffer">Buffer screen</param>
        /// <param name="dimX"> Width of each frame</param>
        /// <param name="dimY"> Height of each frame</param>
        public SpriteSheet(Texture2D sprite, SpriteBatch buffer, int dimX, int dimY)
        {
            this.sheet = sprite;
            this.bufferScreen = buffer;
            this.dimX = dimX;
            this.dimY = dimY;
            this.currentFrame = Vector2.Zero;
            createStructure((int) this.sheet.Width/dimX, (int) this.sheet.Height/dimY);
            
        }

        /// <summary>
        /// create a reference strucutre for a spritesheet
        /// </summary>
        /// <param name="maxCol">Maximum colomns</param>
        /// <param name="maxRow">Maximum rows</param>
        public void createStructure(int maxCol, int maxRow)
        {
            if (maxRow == 0) maxRow = 1;
            if (maxCol == 0) maxCol = 1;
            this.location = new Vector2[maxCol, maxRow];

            for (int i = 0; i < this.location.GetLength(0); ++i)
            {
                for (int j = 0; j < this.location.GetLength(1); ++j)
                {
                    this.location[i, j].X = i * dimX;
                    this.location[i, j].Y = j * dimY;
                }
            }
        }


        /// <summary>
        /// Draw a particular frame image based on specific action
        /// </summary>
        /// <param name="actionType">Action Type</param>
        /// <param name="drawLoc"> Screen Location (X,Y)</param>
        public void drawByAction(byte actionType, Vector2 drawLoc)
        {
            // TODO: Specify col and row based on action
            switch (actionType)
            {
                case 0:
                    curCol = 0;
                    curRow = 0;
                    break;

                case 1:
                    curCol = 1;
                    curRow = 0;
                    break;
            }
            drawAtIndex(curCol, curRow, drawLoc);
        }

        /// <summary>
        /// Animate images
        /// </summary>
        /// <param name="actionType">Action Type</param>
        /// <param name="drawLoc">Screen Location (X,Y)</param>
        public void animateFrame(byte actionType, Vector2 drawLoc)
        {
            switch (actionType)
            {
                case 0:
                    if (curCol < 2)
                    {
                        ++curCol;
                    }
                    else
                    {
                        curCol = 0;
                    }
                 break;
           
            }
            drawAtIndex(curCol, curRow, drawLoc);
        }

        /// <summary>
        /// Draw at a particular index
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        /// <param name="location">Screen location (X,Y)</param>
        public void drawAtIndex(int col, int row, Vector2 drawLoc)
        {
            this.bufferScreen.Draw(sheet, drawLoc, getFrameLocationOnSheetAt(col,row), Color.White);
        }

        /// <summary>
        /// Draw at a particular index
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        /// <param name="drawArea">Rectangle drawn area on the screen</param>
        public void drawAtIndex(int col, int row, Rectangle drawArea)
        {
            this.bufferScreen.Draw(sheet, drawArea ,getFrameLocationOnSheetAt(col,row), Color.White);
        }

        /// <summary>
        /// Draw at a particular index
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        /// <param name="drawArea">Rectangle drawn area on the screen</param>
        /// <param name="rotate_degree">Rotating value (Degree) </param>
        public void drawAtIndex(int col, int row, Rectangle drawArea, float rotate_degree)
        {
            this.bufferScreen.Draw(sheet, drawArea, getFrameLocationOnSheetAt(col, row), Color.White, MathHelper.ToRadians(rotate_degree), Vector2.Zero, SpriteEffects.None, 0);
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="drawArea"></param>
        /// <param name="rotate_radian">Degree in radian</param>
        /// <param name="center">Center from the image</param>
        public void drawAtIndex(int col, int row, Rectangle drawArea, float rotate_radian, Vector2 center)
        {
            this.bufferScreen.Draw(sheet, drawArea, getFrameLocationOnSheetAt(col, row), Color.White, (rotate_radian), center, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Get frame's (X,Y) location on the spritesheet
        /// </summary>
        /// <param name="col">Coloum index</param>
        /// <param name="row">Row index</param>
        /// <returns>Rectangle area representing an image at the particular frame</returns>
        public Rectangle getFrameLocationOnSheetAt(int col, int row)
        {
            return new Rectangle((int)location[col, row].X, (int)location[col, row].Y, dimX, dimY);
        }

        /// <summary>
        /// Draw at current index frame
        /// </summary>
        /// <param name="drawLoc">Screen Location (X,Y)</param>
        public void drawAtCurrentIndex(Vector2 drawLoc)
        {
            drawAtIndex(Col, Row, drawLoc);
        }

    }
}