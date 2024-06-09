//Author:Noah Segal
//File Name:Teleporter.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A teleporter that teleports the player
using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PASS3
{
    class Teleporter
    {
        //Declare the teleporter animation and position
        Point tpPos;
        Animation teleAnim;

        public Teleporter(Animation teleAnim,Point tpPos)
        {
            //Set the animation and position
            this.teleAnim = teleAnim;
            this.tpPos = tpPos;
        }

        /// <summary>
        /// Updates the teleporter
        /// </summary>
        /// <param name="gameTime">Game time is the games time</param>
        public void UpdateTele(GameTime gameTime)
        {
            //Updates the animation
            teleAnim.Update(gameTime);
        }

        /// <summary>
        /// Draws the teleporter
        /// </summary>
        /// <param name="spriteBatch">Is used to draw the image</param>
        public void DrawTele(SpriteBatch spriteBatch)
        {
            //Draws the teleporter
            teleAnim.Draw(spriteBatch,Color.White);
        }

        /// <summary>
        /// Gets the teleporter rectangle
        /// </summary>
        /// <returns>Returns the teleporter rectangle</returns>
        public Rectangle GetRectangle()
        {
            //Returns the teleporter rectangle
            return teleAnim.GetDestRec();
        }

        /// <summary>
        /// Gets the spawn position for the player
        /// </summary>
        /// <returns>Returns the spawn position</returns>
        public Point GetSpawn()
        {
            //Returns the position
            return tpPos;
        }

    }
}
